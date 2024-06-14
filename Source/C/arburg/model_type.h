#ifndef _MODEL_TYPE_H
#define _MODEL_TYPE_H

#include <stdio.h>
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


typedef struct _power_spectrum_item_t
{
    double f;
    double s;
} power_spectrum_item_t;


typedef struct _power_spectrum_t
{
    size_t item_count;
    power_spectrum_item_t *items;
} power_spectrum_t;

#endif
