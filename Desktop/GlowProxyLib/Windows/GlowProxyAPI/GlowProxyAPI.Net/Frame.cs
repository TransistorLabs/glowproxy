using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GlowProxyAPI.Net
{
    [StructLayout(LayoutKind.Sequential, Size = 24, CharSet = CharSet.Ansi)]
    public unsafe struct Frame
    {
        public fixed byte Red[8];
        public fixed byte Green[8];
        public fixed byte Blue[8];
    }
}
