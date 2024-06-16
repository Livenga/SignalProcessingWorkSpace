#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <math.h>
#include <unistd.h>

#include "model_type.h"


/**
 */
power_spectrum_t *power_spectrum_calc(
        const arburg_result_t *p_result,
        double max_freq,
        double step_size,
        double dt)
{
    int cursor = 0;

    size_t size = (size_t)((max_freq / step_size) + 1.f);

    power_spectrum_t *p_ret = (power_spectrum_t *)calloc(1, sizeof(power_spectrum_t));
    power_spectrum_item_t *p_items = (power_spectrum_item_t *)calloc(size, sizeof(power_spectrum_item_t));

    p_ret->am_count = p_result->m_count;
    p_ret->item_count = size;
    p_ret->items = p_items;

    for(double f = .0; f <= max_freq; f += step_size)
    {
        double omega = 2. * M_PI * f;

        double sum_sin = .0f,
               sum_cos = 1.f;
        for(int i = 1; i < p_result->m_count; ++i)
        {
            sum_cos += *(p_result->a + i) * cos(omega * i * dt);
            sum_sin += *(p_result->a + i) * sin(omega * i * dt);
        }

        double s;
        s = (p_result->Pm * dt) / (sum_cos * sum_cos + sum_sin * sum_sin);

        power_spectrum_item_t *p_item = (p_items + cursor);
        // (f, s)
        p_item->f = f;
        p_item->s = s;

        ++cursor;
    }

    return p_ret;
}


/**
 */
int8_t power_spectrum_to_csv(
        const power_spectrum_t *p_self,
        const char *path,
        const int8_t has_header)
{
    if(path == NULL)
    {
        return -1;
    }

    FILE *fp = fopen(path, "wb");
    if(fp == NULL)
    {
        // TODO Error Message
        return -1;
    }


    size_t line_length = 0;
    char buffer[1024];
    memset((void *)buffer, 0, sizeof(buffer));

    if(has_header == 1)
    {
        line_length = snprintf(buffer, 1024, "f\tpower\tdB\n");
        fwrite(buffer, sizeof(char), line_length, fp);
    }

    for(int i = 0; i < p_self->item_count; ++i)
    {
        power_spectrum_item_t *p_item = (p_self->items + i);
        line_length = snprintf(buffer, 1024, "%f\t%f\t%f\n",
                p_item->f,
                p_item->s,
                10.f * log10(p_item->s));

        fwrite(buffer, sizeof(char), line_length, fp);
    }
    fflush(fp);
    fclose(fp);

    return 0;
}


/**
 */
void power_spectrum_free(power_spectrum_t *p)
{
    free(p->items);
    memset((void *)p, 0, sizeof(power_spectrum_t));
    p->items = NULL;

    free(p);
}
