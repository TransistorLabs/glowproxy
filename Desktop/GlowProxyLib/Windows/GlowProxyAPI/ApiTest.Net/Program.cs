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

            try
            {
                var state = new LedState();

                var white = new Color(0xff, 0xff, 0xff);
                var blue = new Color(0x00, 0x00, 0xff);
                var green = new Color(0x00, 0xff, 0x00);
                state.Leds[0] = blue;
                state.Leds[1] = blue;
                state.Leds[2] = blue;
                state.Leds[3] = blue;
                state.Leds[4] = green;
                state.Leds[5] = green;
                state.Leds[6] = green;
                state.Leds[7] = green;
                GlowProxy.SetColor(handle, state);

                var report = new InputReport();
                while (true)
                {
                    var lastReport = report;
                    var r = GlowProxy.ReadInput(handle, ref report);
                    if (!lastReport.Equals(report))
                    {
                        Console.WriteLine(report.ButtonMask + ":" + report.CurrentCount);
                        if (report.ButtonMask > 0)
                        {
                            var lastState = state;
                            for (var i = 0; i < 8; i++)
                            {
                                state.Leds[i] = white;
                            }
                            //GlowProxy.SetColor(handle, state);

                            state = lastState;
                            GlowProxy.SetColor(handle, lastState);

                        }
                        else
                        {
                            var red = report.CurrentCount * 2;
                            var color1 = new Color((byte)red, 0x00, 0xff);
                            var color2 = new Color((byte)red, 0xff, 0x00);
                            state.Leds[0] = color1;
                            state.Leds[1] = color1;
                            state.Leds[2] = color1;
                            state.Leds[3] = color1;
                            state.Leds[4] = color2;
                            state.Leds[5] = color2;
                            state.Leds[6] = color2;
                            state.Leds[7] = color2;
                            GlowProxy.SetColor(handle, state);
                        }
                        
                        
                        
                    }
                }
            }
            catch (Exception)
            {
                {
                }
                throw;
            }
            finally
            {
                GlowProxy.Close(handle);
            }
        }
    }
}
