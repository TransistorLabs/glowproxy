using System;
using System.Collections.Generic;
using System.Linq;

using GlowProxyAPI.Net;

namespace ApiTest.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var handle = GlowProxy.Open();
            var state = new LedState();

            var white = new Color(0xff, 0xff, 0xff);
            var green = new Color(0x00, 0xff, 0x00);
            state.Leds[0] = white;
            state.Leds[1] = white;
            state.Leds[2] = white;
            state.Leds[3] = white;
            state.Leds[4] = green;
            state.Leds[5] = green;
            state.Leds[6] = green;
            state.Leds[7] = green;
            GlowProxy.SetColor(handle, state);
            GlowProxy.Close(handle);

        }
    }
}
