#ifndef _IO_H
#define _IO_H

#include "model.h"
#include "io_type.h"

extern void model_write(
        const char *path,
        const model_t *p_model);

extern void model_write_csv(
        const char *path,
        const model_t *p_model);

/**
 * 最新のデータディレクトリのシンボリックリンクを作成もしくは更新する.
 */
extern int create_or_update_data_link(
        const char *src,
        const char *dst);

#endif
