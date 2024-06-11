#include <stdio.h>
#include <math.h>
#include <string.h>
#include <stdlib.h>
#include <time.h>
#include <sys/stat.h>
#include <errno.h>

#include "model.h"

extern void model_write(
        const char *path,
        const model_t *p_model);

extern void model_write_csv(
        const char *path,
        const model_t *p_model);

/**
 */
static void calc_power_spectrum(
        const arburg_result_t *p_result,
        double max_freq,
        char *destination_path);

static char *create_datetime_directory(
        char   *path,
        size_t n);

static int eprintf(
        FILE *stream,
        const char *message,
        const char *fn);

static char *path_combine(
        char *p1,
        char *p2);

static int q_compare(const void *p1, const void *p2);

/**
 */
int main(
        int argc,
        char *argv[])
{
    srand(time(NULL));

    char path[1024];
    memset((void *)path, 0, sizeof(path));

    model_t *p_model = model_create_sample(1.f, 1000);
    //model_t *p_model = model_create_sin(1.f, 50.f, 512);

    // 出力先ディレクトリ作成
    create_datetime_directory(path, 1024);
    char *signal_path = path_combine(path, "signal.csv");
    //model_write_csv(signal_path, p_model);

    free(signal_path);

#if 0
    for(int32_t i = 0; i < p_model->size; ++i)
    {
        model_item_t *p_item = (p_model->items + i);
        fprintf(stdout, "%.12f\t%.12f\n",
                p_item->time,
                p_item->value);
    }
#endif

    int order_count = 500;
    arburg_result_t *p_result = model_ar_model(p_model, order_count);

    //
    qsort(p_result, order_count - 1, sizeof(arburg_result_t), q_compare);

#if 0
    double min_q = p_result->Q;
    int min_index = 0;
    for(int i = 1; i < (order_count - 1); ++i)
    {
        double q = (p_result + i)->Q;
        fprintf(stderr, "\t[%4d]\t%f\n", i, q);

        if(min_q > q)
        {
            min_q     = q;
            min_index = i;
        }
    }
    arburg_result_t *p_a = (p_result + min_index);
    fprintf(stderr, "最小Q[%d] = %f\n", min_index, p_a->Q);
#endif

    // 上位 16 の結果の出力を行う.
    for(int i = 0; i < 16; ++i)
    {
        calc_power_spectrum(p_result + i, 200.f, path);
    }


#if 0
    for(int i = 0; i < (order_count - 1); ++i)
    {
        calc_power_spectrum(p_result + i, 100.f, path);
    }
#endif
    free(p_result);

    return 0;
}


/**
 */
static void calc_power_spectrum(
        const arburg_result_t *p_result,
        double max_freq,
        char *destination_path)
{
    FILE *fp = NULL;
    char path[2048];

    memset((void *)path, 0, sizeof(path));
    snprintf(path, 2048, "%s/%06ld.csv",
            destination_path,
            p_result->m_count);

    fp = fopen(path, "w");
    if(fp == NULL)
    {
        eprintf(stderr, "", "fopen(3)");
        return;
    }


    double dt = .001f;
    for(double f = .0f; f < max_freq; f += .1f)
    {
        double omega = 2. * M_PI * f;

        double sum_sin = .0f,
               sum_cos = 1.f;
        for(int i = 0; i < p_result->m_count; ++i)
        {
            sum_cos += *(p_result->a + i) * cos(omega * (i + 1) * dt);
            sum_sin += *(p_result->a + i) * sin(omega * (i + 1) * dt);
        }

        double s;
#if 1
        s = (p_result->Pm * dt) / (sum_cos * sum_cos + sum_sin * sum_sin);
#else
        s = 1.f / (sum_cos * sum_cos + sum_sin * sum_sin);
        s = 10.f * log10(s);
#endif
        //fprintf(stdout, "%f\t%f\n", f, s);
        fprintf(fp, "%f\t%f\n", (double)f, s);
    }

    fclose(fp);
}


static char *create_datetime_directory(
        char   *path,
        size_t n)
{
    time_t t = time(NULL);
    struct tm *p_now = localtime(&t);

    snprintf(path, n, "csvs/%04d%02d%02d_%02d%02d%02d",
            (1900 + p_now->tm_year), (1 + p_now->tm_mon), p_now->tm_mday,
            p_now->tm_hour,          p_now->tm_min,       p_now->tm_sec);

    struct stat statbuf;
    memset((void *)&statbuf, 0, sizeof(struct stat));
    int ret = stat(path, &statbuf);
    if(ret == 0)
    {
        return S_ISDIR(statbuf.st_mode)
            ? path
            : NULL;
    }

    // ディレクトリの作成
    ret = mkdir(path, 0755);
    if(ret != 0)
        return NULL;
    fprintf(stderr, "%s: ディレクトリ作成.\n", path);

    return path;
}


static int eprintf(
        FILE *stream,
        const char *message,
        const char *fn)
{
    char *err= strerror(errno);

    return (fn != NULL)
        ? fprintf(stream, "%s: %s\t(%s)\n", fn, err, message)
        : fprintf(stream, "%s\t(%s)\n", err, message);
}


static char *path_combine(
        char *p1,
        char *p2)
{
    size_t p1_len = strlen(p1),
           p2_len = strlen(p2);

    size_t len = p1_len + p2_len + 1;

    char *p = (char *)calloc(1 + len, sizeof(char));

    strncat(p, p1, len);
    *(p + p1_len) = '/';
    strncat(p, p2, len);

    return p;
}


static int q_compare(const void *p1, const void *p2)
{
    if(p1 == NULL || p2 == NULL)
        return -1;

    const arburg_result_t *_p1 = (const arburg_result_t *)p1,
                    *_p2 = (const arburg_result_t *)p2;

    return _p1->Q > _p2->Q;
}
