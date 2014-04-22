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
    
    frame.Leds[0].Red =     0xff;
    frame.Leds[0].Green =   0xff;
    frame.Leds[0].Blue =    0xff;
    
    frame.Leds[1].Red =     0xff;
    frame.Leds[1].Green =   0xff;
    frame.Leds[1].Blue =    0xff;

    frame.Leds[2].Red =     0xff;
    frame.Leds[2].Green =   0xff;
    frame.Leds[2].Blue =    0xff;

    frame.Leds[3].Red =     0xff;
    frame.Leds[3].Green =   0xff;
    frame.Leds[3].Blue =    0xff;

    frame.Leds[4].Red =     0x00;
    frame.Leds[4].Green =   0xff;
    frame.Leds[4].Blue =    0x00;

    frame.Leds[5].Red =     0x00;
    frame.Leds[5].Green =   0xff;
    frame.Leds[5].Blue =    0x00;

    frame.Leds[6].Red =     0x00;
    frame.Leds[6].Green =   0xff;
    frame.Leds[6].Blue =    0x00;

    frame.Leds[7].Red =     0x00;
    frame.Leds[7].Green =   0xff;
    frame.Leds[7].Blue =    0x00;

    SetColor(handle, frame);
    Close(handle);
	return 0;
}

