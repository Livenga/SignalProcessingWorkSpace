#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <unistd.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <errno.h>

#include "model.h"
#include "io_type.h"


/**
 */
void arburg_result_dump(const arburg_result_t *p_result)
{
    int32_t n = (p_result->m_count - 1);
    char path[1024];
    memset((void *)path, 0, sizeof(path));
    snprintf(path, 1024, "a/m_%06d.csv", n);

    FILE *fp = fopen(path, "wb");
    if(fp == NULL)
        return;

    fprintf(fp, "Pm = %.8f\n",   p_result->Pm);
    fprintf(fp, "Q  = %.8f\n\n", p_result->Q);

    for(int i = 0; i < p_result->m_count; ++i)
    {
        double a = *(p_result->a + i);
        fprintf(fp, "%d\t%.8f\n", i, a);
    }

    fclose(fp);
}


void model_write(
        const char *path,
        const model_t *p_model)
{
    char *header = "arburg";

    FILE *fp = NULL;
    fp = fopen(path, "wb");
    if(fp == NULL)
        return;

    fwrite(header, sizeof(char), strlen(header), fp);

    int16_t mode = (int16_t)SIGNAL;
    int32_t data_count = p_model->size;

    fwrite((void *)&mode, sizeof(int16_t), 1, fp);
    fwrite((void *)&data_count, sizeof(int32_t), 1, fp);

    fflush(fp);

    for(int i = 0; i < p_model->size; ++i)
    {
        model_item_t *p_item = (p_model->items + i);
        fwrite((void *)p_item, sizeof(model_item_t), 1, fp);
    }

    fflush(fp);
    fclose(fp);
}


void model_write_csv(
        const char *path,
        const model_t *p_model)
{
    FILE *fp = NULL;
    fp = fopen(path, "wb");
    if(fp == NULL)
        return;

    for(int i = 0; i < p_model->size; ++i)
    {
        model_item_t *pi = (p_model->items + i);
        fprintf(fp, "%f\t%f\n", pi->time, pi->value);
    }

    fflush(fp);
    fclose(fp);
}


/**
 */
model_t *model_read_csv(
        const char *path,
        const char separator,
        double     dt)
{
    FILE *fp = fopen(path, "rb");
    if(fp == NULL)
        return NULL;

    size_t bufsize = 4096;
    char buffer[bufsize];

    int item_count = 0;
    while(fgets(buffer, bufsize, fp))
    {
        ++item_count;
    }

    // 先頭位置に移動
    fseek(fp, 0L, SEEK_SET);

    model_t *p_ret = (model_t *)calloc(1, sizeof(model_t));
    p_ret->size = item_count;
    p_ret->items = (model_item_t *)calloc(item_count, sizeof(model_item_t));

    for(int i = 0; i < item_count; ++i)
    {
        char *p_str = fgets(buffer, bufsize, fp);

        model_item_t *p_item = (p_ret->items + i);
        p_item->time = dt * i;

        char *p_sep = strchr(p_str, separator);
        if(p_sep != NULL)
        {
            *p_sep = '\0';

            p_item->time  = strtod(p_str, NULL);
            p_item->value = strtod(p_sep + 1, NULL);

            *p_sep = separator;
        }
        else
        {
            p_item->value = strtod(p_str, NULL);
        }
    }

    return p_ret;
}


/**
 * 最新のデータディレクトリのシンボリックリンクを作成もしくは更新する.
 */
int create_or_update_data_link(
        const char *src,
        const char *dst)
{
    int ret = EOF;

    // 既にシンボリックリンクが存在する場合は削除する.
    if(access(dst, F_OK) == 0)
    {
        struct stat statbuf;
        memset(&statbuf, 0, sizeof(struct stat));

        ret = lstat(dst, &statbuf);

        if(ret == 0 && ! S_ISLNK(statbuf.st_mode))
        {
            // 使用想定している宛先がシンボリックリンク以外としてすでに存在している
            return -2;
        }

        // シンボリックリンクの削除
        unlink(dst);
    }

    fprintf(stderr, "%s -> %s\n", src, dst);

    ret = symlink(src, dst);

    return (ret == 0) ? 0 : errno;
}
