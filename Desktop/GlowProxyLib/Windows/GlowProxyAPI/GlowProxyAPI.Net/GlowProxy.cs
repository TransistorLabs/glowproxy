using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GlowProxyAPI.Net
{
    public static class GlowProxy
    {
        [DllImport("GlowProxyAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Init")]
        internal static extern int _init();

        [DllImport("GlowProxyAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Open")]
        internal static extern IntPtr _open();

        [DllImport("GlowProxyAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetColor")]
        internal static extern void _setColor(IntPtr handle, Frame frame);

        [DllImport("GlowProxyAPI.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Close")]
        internal static extern void _close(IntPtr handle);

        public unsafe static Frame GetFrameFromLeds(LedState state)
        {
            var frame = new Frame();
            var index = 0;
            foreach (var led in state.Leds)
            {
                frame.Red[index] = led.Red;
                frame.Green[index] = led.Green;
                frame.Blue[index] = led.Blue;
                ++index;
            }
            return frame;
        }

        public static IntPtr Open()
        {
            _init();
            var handle = _open();
            return handle;
        }

        public static void SetColor(IntPtr handle, LedState state)
        {
            var frame = GetFrameFromLeds(state);
            _setColor(handle, frame);
        }

        public static void Close(IntPtr handle)
        {
            _close(handle);
        }

    }
}
