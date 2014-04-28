// GlowProxyAPI.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "GlowProxyAPI.h"

#define VENDOR_ID	0X03EB
#define	PRODUCT_ID 	0x204f

int Init(void)
{
    return hid_init();
}

glowproxy_device* Open(void)
{
    glowproxy_device* handle;
    handle = hid_open(VENDOR_ID, PRODUCT_ID, NULL);
    if (!handle) {
        //printf("unable to open device\n");
        return NULL;
    }
    return handle;
}

void Close(glowproxy_device* handle)
{
    hid_close(handle);
    hid_exit();
}

void SetColor(glowproxy_device* handle, Frame frameData)
{
    int res;
    unsigned char buf[25];
    memset(buf, 0, sizeof(buf));

    buf[0] = 0x00; // Report 0
    int led = 0, 
        bufferIndex = 0;

    for (led = 0; led < 8; led++)
    {
        buf[++bufferIndex] = frameData.Red[led];
        buf[++bufferIndex] = frameData.Green[led];
        buf[++bufferIndex] = frameData.Blue[led];
    }

    
    res = hid_write(handle, buf, 25);
}
