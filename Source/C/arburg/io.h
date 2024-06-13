#ifndef _IO_H
#define _IO_H

#include "model.h"
#include "io_type.h"


/**
 */
extern void arburg_result_dump(const arburg_result_t *p_result);

extern void model_write(
        const char *path,
        const model_t *p_model);

extern void model_write_csv(
        const char *path,
        const model_t *p_model);

/**
 */
extern model_t *model_read_csv(
        const char *path,
        const char separator,
        double     dt);

/**
 * 最新のデータディレクトリのシンボリックリンクを作成もしくは更新する.
 */
extern int create_or_update_data_link(
        const char *src,
        const char *dst);

#endif
