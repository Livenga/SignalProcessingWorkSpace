#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <math.h>
#include <time.h>

#include "model.h"


/**
 */
static arburg_result_t *test_ar_model_read(
        const char *path,
        size_t bufsize);

static const char *get_exec_name(const char *p);

int main(
        int argc,
        char *argv[])
{
    if(argc == 1)
        return EOF;

    // model_t *p_model = model_create_sample(1.f, 1000, 0);
    arburg_result_t *p_arburg = test_ar_model_read(*(argv + 1), 2048L);

    if(p_arburg == NULL)
    {
        // model_free(p_model);
        return EOF;
    }


    char result_path[1024];
    memset((void *)result_path, 0, sizeof(result_path));

    time_t t_now = time(NULL);
    struct tm *tm_now = localtime(&t_now);

    snprintf(result_path, 1024, "%04d%02d%02d_%02d%02d%02d-%s.csv",
            (1900 + tm_now->tm_year), 1 + tm_now->tm_mon, tm_now->tm_mday,
            tm_now->tm_hour, tm_now->tm_min, tm_now->tm_sec,
            get_exec_name(*argv));
    FILE *fp_result = NULL;
    fp_result = fopen(result_path, "w");

    double step_size = .1f;
    power_spectrum_t *p_ps = power_spectrum_calc(p_arburg, 300.f, step_size, .001);

    fprintf(stderr, "[Power Spectrum]\n");
    for(int i = 0; i < p_ps->item_count; ++i)
    {
        power_spectrum_item_t *p_item = (p_ps->items + i);
        double _s = 10.f * log10(p_item->s);

        fprintf(stderr, "\t%.3f\t%f\t%f\n", p_item->f, p_item->s, _s);
        if(fp_result != NULL)
        {
            fprintf(fp_result, "%f\t%f\t%f\n", p_item->f, p_item->s, _s);
        }
    }

    power_spectrum_free(p_ps);

    arburg_result_free(p_arburg);
    // model_free(p_model);

    fclose(fp_result);
    return 0;
}


//
static arburg_result_t *test_ar_model_read(
        const char *path,
        size_t bufsize)
{
    FILE *fp = fopen(path, "rb");
    if(fp == NULL)
        return NULL;

    char line[bufsize];
    size_t count = 0;
    while(fgets(line, bufsize, fp) != NULL)
    {
        ++count;
    }

    fprintf(stderr, "モデル次数 m: %ld\n", count);
    if(count < 1)
    {
        fclose(fp);
        return NULL;
    }

    arburg_result_t *p_ret = (arburg_result_t *)calloc(1, sizeof(arburg_result_t));
    p_ret->Pm = .1f;
    p_ret->m_count = count;
    p_ret->a = (double *)calloc(count, sizeof(double));

    fseek(fp, 0L, SEEK_SET);
    int cursor = 0;
    while(fgets(line, bufsize, fp) != NULL)
    {
        // XXX 必要であればここで異常検出処理
        double a = strtod(line, NULL);

        *(p_ret->a + cursor) = a;

        ++cursor;
    }

    fclose(fp);
    return p_ret;
}


static const char *get_exec_name(const char *p)
{
    const char *p_sep = p;
    const char *p_prev = NULL;
    do
    {
        p_sep = strchr(p_sep + 1, '/');
        if(p_sep != NULL)
            p_prev = p_sep;
    } while(p_sep != NULL);

    return p_prev != NULL ? (p_prev + 1) : p;
}
