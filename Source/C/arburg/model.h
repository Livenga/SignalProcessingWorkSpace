#ifndef _MODEL_H
#define _MODEL_H

#include <stdint.h>
#include "model_type.h"


/**
 * サンプルモデルの作成
 */
extern model_t *model_create_sample(
        double  sec,
        int32_t size,
        int8_t  contains_noise);

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


/**
 */
extern power_spectrum_t *power_spectrum_calc(
        const arburg_result_t *p_result,
        double max_freq,
        double step_size,
        double dt);


/**
 */
extern int8_t power_spectrum_to_csv(
        const power_spectrum_t *p_self,
        const char *path,
        const int8_t has_header);


/**
 */
extern void power_spectrum_free(power_spectrum_t *p);
#endif
