using System;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace PetersDllWrapper
{
    internal class ApiCallPblKfCloseUnsafe : ApiCallPblKfClose
    {
        internal override unsafe int PblKfClose(IntPtr pblKeyFile)
        {
            var result = pblKfClose((PblKeyFile_t*)pblKeyFile);
            return result;
        }

        internal ApiCallPblKfCloseUnsafe(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        // should look like the C function as much as possible.
        // ReSharper disable InconsistentNaming
        private unsafe int pblKfClose(PblKeyFile_t* k)
        {
            return ((delegate* unmanaged[Stdcall]<PblKeyFile_t*, int>)AddressOfNativeMethod)(k);
        }
        // ReSharper restore InconsistentNaming


    }
}