using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace PetersDllWrapper
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct PblKeyFile_t
    {
        public char* magic;
    }

}