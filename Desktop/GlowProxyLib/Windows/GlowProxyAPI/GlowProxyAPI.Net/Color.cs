using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GlowProxyAPI.Net
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack=1)]
    public struct Color
    {
        public Color(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public byte Red;
        public byte Green;
        public byte Blue;
    }
}
