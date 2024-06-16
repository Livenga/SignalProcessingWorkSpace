#include <stdio.h>
#include <math.h>
#include <string.h>
#include <stdlib.h>
#include <time.h>
#include <sys/stat.h>
#include <errno.h>

#include "io.h"
#include "model.h"


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
static char *get_target_name(const char *path);

static void create_gnuplot_script(power_spectrum_t **p_psds, size_t size, const char *root_path);

extern void db_insert_orders(
        const arburg_result_t *p_result,
        int order_count);

/**
 */
int main(
        int argc,
        char *argv[])
{
    srand(time(NULL));

    char path[1024];
    memset((void *)path, 0, sizeof(path));

    model_t *p_model = NULL;

    if(argc > 1)
    {
        p_model = model_read_csv(*(argv + 1), '\t', .001);
    }

    if(p_model == NULL)
    {
        p_model = model_create_sample(1.f, 1000, 0);
        //p_model = model_create_sin(1.f, 50.f, 512);
    }

    // 出力先ディレクトリ作成
    create_datetime_directory(path, 1024);
    char *signal_path = path_combine(path, "signal.csv");
    model_write_csv(signal_path, p_model);

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

    int order_count = 256;
    arburg_result_t *p_result = model_ar_model(p_model, order_count);
    //db_insert_orders(p_result, order_count);

    qsort(p_result, order_count - 1, sizeof(arburg_result_t), q_compare);


    size_t psd_size = 16;
    power_spectrum_t **p_psds = (power_spectrum_t **)calloc(psd_size, sizeof(power_spectrum_t *));
    // 上位 16 の結果のパワースペクトラムの出力.
    for(int i = 0; i < psd_size; ++i)
    {
        power_spectrum_t *p_ps = power_spectrum_calc(p_result + i, 300, .1f, .001f);
        *(p_psds + i) = p_ps;

        fprintf(stderr, "[%ld]\tQm = %f\n", (p_result + i)->m_count, (p_result + i)->Q);

        // CSV ファイル名の割り当て
        char ps_filename[256];
        memset((void *)ps_filename, 0, sizeof(ps_filename));
        snprintf(ps_filename, 256, "%03d_m%06ld.csv", i, (p_result + i)->m_count);

        char *p_csv = path_combine(path, ps_filename);

        // csv ファイルへの出力
        power_spectrum_to_csv(p_ps, p_csv, 1);

        // 各種解放
        free(p_csv);
        //power_spectrum_free(p_ps);
    }

    // gnuplot スクリプトファイルの作成
    create_gnuplot_script(p_psds, psd_size, path);

    // PSD 解放
    for(int i = 0; i < psd_size; ++i)
    {
        power_spectrum_free(*(p_psds + i));
        *(p_psds + i) = NULL;
    }
    free(p_psds); p_psds = NULL;


    // 最新のデータディレクトリのシンボリックリンクを作成
    char *p_target_name = get_target_name(path);
    create_or_update_data_link(p_target_name, "csvs/latest");

    free(p_target_name); p_target_name = NULL;


    for(int i = 0; i < order_count - 1; ++i)
    {
        arburg_result_dump((p_result + i));
    }

    free(p_result);

    return 0;
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


/**
 * qsort 順序判定関数
 */
static int q_compare(
        const void *p1,
        const void *p2)
{
    if(p1 == NULL || p2 == NULL)
        return -1;

    const arburg_result_t *_p1 = (const arburg_result_t *)p1,
                    *_p2 = (const arburg_result_t *)p2;

    return _p1->Q > _p2->Q;
}


/**
 */
static char *get_target_name(const char *path)
{
    size_t len = strlen(path);
    char *tmp = (char *)calloc(len + 1, sizeof(char));
    memcpy(tmp, path, len * sizeof(char));

    // XXX 末尾に '/' が複数連続した場合が考慮されていない.
    if('/' == *(tmp + (len - 1)))
        *(tmp + (len - 1)) = '\0';

    char *p_split = strchr(tmp, '/');
    if(p_split == NULL)
        return tmp;

    char *p_prev = p_split;
    do
    {
        p_split = strchr(p_prev + 1, '/');
        if(p_split != NULL)
            p_prev = p_split;
    }
    while(p_split != NULL);

    ++p_prev;
    len = strlen(p_prev);
    char *p_ret = (char *)calloc(len + 1, sizeof(char));
    memcpy(p_ret, p_prev, len);

    free(tmp); tmp = NULL;

    return p_ret;
}


/**
 */
static void create_gnuplot_script(
        power_spectrum_t **p_psds,
        size_t size,
        const char *root_path)
{
    FILE *fp = NULL;

    char script_name[256];
    memset(script_name, 0, sizeof(script_name));
    snprintf(script_name, 256, "4x4_multiplot.p");

    char *script_path = path_combine((char *)root_path, script_name);
#if 1
    fprintf(stderr, "gunplot script filepath = %s\n", script_path);
#endif

    fp = fopen(script_path, "wb");
    free(script_path); script_path = NULL;
    if(fp == NULL)
        return;

    fprintf(fp, "# %s\n\n", script_name);
    fprintf(fp, "set grid xtics mxtics linewidth 1, linewidth .8\n");
    fprintf(fp, "set grid ytics mytics linewidth 1, linewidth .8\n\n");
    fprintf(fp, "set terminal png\n");
    fprintf(fp, "set terminal pngcairo size 4096, 4096 font \", 8\"\n");
    fprintf(fp, "set output \"4_4.png\"\n");
    fprintf(fp, "set multiplot layout 4,4\n\n");
    for(int col = 2; col < 4; ++col)
    {
        for(int i = 0; i < size; ++i)
        {
            power_spectrum_t *p_psd = *(p_psds + i);

            if(col == 2)
                fprintf(fp, "# ");

            fprintf(fp, "plot \"%03d_m%06ld.csv\" every::1 using 1:%d title \"am[%ld]\" with lines\n",
                    i,
                    p_psd->am_count,
                    col,
                    p_psd->am_count);
        }

        if(col != 3)
            fprintf(fp, "\n");
    }

    fclose(fp);
}
