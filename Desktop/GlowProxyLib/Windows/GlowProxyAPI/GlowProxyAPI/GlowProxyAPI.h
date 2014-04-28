#ifndef GLOWPROXYAPI_H__
#define GLOWPROXYAPI_H__

#include "hidapi.h"

#ifdef __cplusplus
extern "C" {
#endif

    typedef struct hid_device_ glowproxy_device;

    typedef struct
    {
        unsigned char Red;
        unsigned char Green;
        unsigned char Blue;
    } Color;

    typedef struct
    {
        unsigned char Red[8];
        unsigned char Green[8];
        unsigned char Blue[8];
    } Frame;

    int Init(void);

    glowproxy_device* Open(void);

    void Close(glowproxy_device* handle);

    void SetColor(glowproxy_device* handle, Frame frameData);

#ifdef __cplusplus
}
#endif

#endif