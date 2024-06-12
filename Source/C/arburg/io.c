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
 * 最新のデータディレクトリのシンボリックリンクを作成もしくは更新する.
 */
int create_or_update_data_link(
        const char *src,
        const char *dst)
{
    int ret = EOF;

    // 既にシンボリックリンクが存在する場合は削除する.
    if(access(dst, F_OK))
    {
        struct stat statbuf;
        memset(&statbuf, 0, sizeof(struct stat));

        ret = stat(dst, &statbuf);

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
