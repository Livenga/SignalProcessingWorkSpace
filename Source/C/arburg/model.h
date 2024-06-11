#ifndef _MODEL_H
#define _MODEL_H

#include <stdint.h>


typedef struct _model_item_t
{
    double time;
    double value;
} model_item_t;

typedef struct _model_t
{
    size_t size;
    model_item_t *items;
    //double *values;
} model_t;


typedef struct _arburg_result_t
{
    double Pm;
    double Q;
    double *a;
    size_t m_count;
} arburg_result_t;


/**
 * サンプルモデルの作成
 */
extern model_t *model_create_sample(
        double sec,
        int32_t size);

/**
 */
extern model_t *model_create_sin(
        double sec,
        double freq,
        int    sampling_rate);

/**
 */
extern model_t *model_create_serial_data(int32_t size);

/**
 */
extern void model_free(model_t *p);

/**
 */
extern arburg_result_t *model_ar_model(
        const model_t *model,
        size_t order);

/**
 */
extern void arburg_result_free(arburg_result_t *p);

#endif
