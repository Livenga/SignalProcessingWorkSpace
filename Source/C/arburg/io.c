#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <unistd.h>

#include "model.h"


enum _file_type_t
{
    SIGNAL = 0,
    RESULT
} file_type_t;


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
