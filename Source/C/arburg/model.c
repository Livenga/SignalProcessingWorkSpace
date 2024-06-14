//#define __DEBUG__
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdint.h>
#include <time.h>
#include <math.h>

#include "model.h"


/**
 */
model_t *model_create_sample(
        double  sec,
        int32_t size,
        int8_t  contains_noise)
{
    model_t *p = (model_t *)calloc(1, sizeof(model_t));
    p->size   = size;
    p->items = (model_item_t *)calloc(p->size, sizeof(model_item_t));

    double step_size = sec / p->size;
    double rad1 = 2. * M_PI * 28.,
           rad2 = 2. * M_PI * 31.5,
           rad3 = 2. * M_PI * 32.5;

    if(contains_noise)
        srand(time(NULL));

    int cnt = 0;
    for(double dt = 0.; dt <= sec; dt += step_size)
    {
        double signal  = sin(rad1 * dt) + sin(rad2 * dt) + sin(rad3 * dt);
        if(contains_noise)
        {
            double noise = ((double)rand() / RAND_MAX) * 2. - 1.;
            signal += noise;
        }

        model_item_t *p_item = (p->items + cnt);

        p_item->time  = dt;
        p_item->value = signal;

        ++cnt;
    }

    return p;
}


/**
 */
model_t *model_create_sin(
        double sec,
        double freq,
        int    sampling_rate)
{
    double dt = sec / (double)sampling_rate;

    model_t *p = (model_t *)calloc(1, sizeof(model_t));

    p->size = sampling_rate;
    p->items = (model_item_t *)calloc(sampling_rate, sizeof(model_item_t));

    double omega = 2.f * M_PI * freq;
    for(int i = 0; i < sampling_rate; ++i)
    {
        model_item_t *p_item = (p->items + i);

        p_item->time  = dt * i;
        p_item->value = sin(omega * i * dt);
    }

    return p;
}

/**
 */
model_t *model_create_serial_data(int32_t size)
{
    model_t *p = (model_t *)calloc(1, sizeof(model_t));

    p->size = size;
    p->items = (model_item_t *)calloc(size, sizeof(model_item_t));

    for(int i = 0; i < size; ++i)
    {
        model_item_t *p_item = (p->items + i);

        p_item->time  = 1 + i;
        p_item->value = 1 + i;
    }

    return p;
}


/**
 */
void model_free(model_t *p)
{
    if(p->items != NULL)
    {
        free((void *)p->items);
        p->items = NULL;
    }

    free(p);
}


/**
 */
arburg_result_t *model_ar_model(
        const model_t *model,
        size_t order_count)
{
    int32_t N = model->size;

    double *x = (double *)calloc(N, sizeof(double));
    double sum_x = .0f;
    for(int i = 0; i < N; ++i) {
        *(x + i) = (model->items + i)->value;
        sum_x += *(x + i);
    }

    double ave_x = sum_x / N;
#if 0
    sum_x = .0f;
    for(int i = 0; i < N; ++i)
    {
        double z = *(x + i) - ave_x;
        *(x + i) = z;
        sum_x += z * z;
    }
    double pm = sum_x / N;
#endif

    double *bx  = (double *)calloc(N, sizeof(double));
    double *bdx = (double *)calloc(N, sizeof(double));

    double *P = (double *)calloc(order_count, sizeof(double));

    // Pm0 は x のモデルの分散
    sum_x = .0f;
    for(int i = 0; i < N; ++i)
    {
        double xx_ = *(x + i) - ave_x;
        *(bx + i) = xx_;
        sum_x += xx_ * xx_;
    }
    *(P + 0) = sum_x / (double)N;
    fprintf(stderr, "x 分散 = %f\n", *P);

    double *a      = (double *)calloc(order_count, sizeof(double));
    *(a + 0) = 1.f;

    double *a_prev = (double *)calloc(order_count, sizeof(double));

    // b'1i
    memcpy(bdx, bx + 1, (N - 1) * sizeof(double));


    arburg_result_t *p_ret = (arburg_result_t *)calloc(order_count - 1, sizeof(arburg_result_t));

    for(int m = 1; m < order_count; ++m)
    {
        arburg_result_t *p_ar = p_ret + (m - 1);
        p_ar->Pm      = .0f;
        p_ar->a       = (double *)calloc(m + 1, sizeof(double));
        p_ar->m_count = (m + 1);

        int max_count = N - m;

        double sum_n = .0f, sum_m = .0f;
        for(int i = 1; i < max_count; ++i)
        {
            double bmi  = *(bx  + i),
                   bdmi = *(bdx + i);

            sum_n += bmi * bdmi;
            sum_m += (bmi * bmi) + (bdmi * bdmi);
        }

        double amm = (-2.f * sum_n) / sum_m;

        *(a + m) = amm;
        *(P + m) = *(P + (m - 1)) * (1.f - amm * amm);
        p_ar->Pm = *(P + m);

        if(m > 1)
        {
            for(int i = 1; i < m; ++i)
            {
                *(a + i) = *(a_prev + i) + amm * *(a_prev + (m - i));
            }
        }
        memcpy(a_prev, a, (m + 1) * sizeof(double));
        memcpy(p_ar->a, a, (m + 1) * sizeof(double));

        // Em^2 の計算
        double Em_2 = .0f;
        for(int k = m + 1; k < N; ++k)
        {
            double sum_ax = .0f;
            for(int i = 1; i <= m; ++i)
            {
                sum_ax += *(a + i) + *(x + (k - i));
            }

            Em_2 += pow(*(x + k) - sum_ax, 2.f);
        }

        p_ar->Q = Em_2 * ((1.f + ((double)m + 1.f ) / N)
                / ((1.f - ((double)m + 1.f) / N)));

        // bmi, dbmi 更新
        for(int i = 1; i < N - m; ++i)
        {
            *(bx  + i) += amm * *(bdx + i);
            *(bdx + i) = *(bdx + (i + 1)) + amm * *(bx + (i + 1));
        }
    }

    free((void *)a_prev); a_prev = NULL;
    free((void *)a); a = NULL;

    free((void *)P); P = NULL;

    free((void *)bx);  bx  = NULL;
    free((void *)bdx); bdx = NULL;
    free((void *)x);   x   = NULL;

    return p_ret;
}


/**
 */
void arburg_result_free(arburg_result_t *p)
{
    free(p->a);
    memset(p, 0, sizeof(arburg_result_t));
    free(p);
}
