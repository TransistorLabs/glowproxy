using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GlowProxyAPI.Net
{
    [StructLayout(LayoutKind.Sequential, Size = 25, CharSet = CharSet.Ansi)]
    public unsafe struct InputReport
    {
        public byte ButtonMask;
        public byte CurrentCount;
        public fixed byte Reserved [23];

        public bool Equals(InputReport r)
        {
            // Return true if the fields match:
            return (this.ButtonMask == r.ButtonMask)
                && (this.CurrentCount == r.CurrentCount);
        }
    }
}
