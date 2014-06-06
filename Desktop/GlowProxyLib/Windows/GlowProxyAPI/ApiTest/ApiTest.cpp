// ApiTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../GlowProxyAPI/GlowProxyAPI.h"

int _tmain(int argc, _TCHAR* argv[])
{
    Init();
    glowproxy_device* handle;
    handle = Open();
    Frame frame;
    
    frame.Red[0] =     0xff;
    frame.Green[0] = 0xff;
    frame.Blue[0] = 0xff;
    
    frame.Red[1] = 0xff;
    frame.Green[1] = 0x00;
    frame.Blue[1] = 0xff;

    frame.Red[2] = 0xff;
    frame.Green[2] = 0x00;
    frame.Blue[2] = 0xff;

    frame.Red[3] = 0xff;
    frame.Green[3] = 0xff;
    frame.Blue[3] = 0xff;

    frame.Red[4] = 0x00;
    frame.Green[4] = 0xff;
    frame.Blue[4] = 0x00;

    frame.Red[5] = 0x00;
    frame.Green[5] = 0xff;
    frame.Blue[5] = 0x00;

    frame.Red[6] = 0x00;
    frame.Green[6] = 0xff;
    frame.Blue[6] = 0x00;

    frame.Red[7] = 0x00;
    frame.Green[7] = 0xff;
    frame.Blue[7] = 0x00;

    SetColor(handle, frame);
    Close(handle);
	return 0;
}

