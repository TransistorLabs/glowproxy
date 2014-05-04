using System;
using System.Collections.Generic;
using System.Globalization;
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

        /// <summary>
        /// Assumes a color input format of "#ffffff"
        /// </summary>
        public Color(string color)
        {
            Red = Blue = Green = 0;
            color = color.Replace("#", "");

            if (color.Length == 6)
            {
                Red = ParseColor(color.Substring(0,2));
                Green = ParseColor(color.Substring(2,2));
                Blue = ParseColor(color.Substring(4,2));
            }
        }

        public override string ToString()
        {
            return string.Format("#{0:x2}{1:x2}{2:x2}", Red, Green, Blue);
        }

        public byte Red;
        public byte Green;
        public byte Blue;

        private static byte ParseColor(string input)
        {
            byte output;
            byte.TryParse(input, NumberStyles.HexNumber, null ,out output);
            return output;
        }
    }
}
