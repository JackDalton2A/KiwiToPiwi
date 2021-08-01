using System;
using System.Runtime.InteropServices;

namespace PetersDllWrapper
{
    internal class ApiCallPblKfOpenUnsafe : ApiCallPblKfOpen
    {
        internal override unsafe IntPtr PblKfOpen(string path, int update, IntPtr fileSetTag)
        {
            var ptrPath = Marshal.StringToHGlobalAnsi(path);
            var result = pblKfOpen((char*) ptrPath.ToPointer(), update, fileSetTag.ToPointer());
            Marshal.FreeHGlobal(ptrPath);
            return (IntPtr) result;
        }

        internal ApiCallPblKfOpenUnsafe(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        // should look like the C function as much as possible.
        // ReSharper disable InconsistentNaming
        private unsafe PblKeyFile_t* pblKfOpen(char* path, int update, void* filesettag)
        {
            return ((delegate* unmanaged[Stdcall]<char*, int, void*, PblKeyFile_t*>)AddressOfNativeMethod)(path, update, filesettag);
        }
        // ReSharper restore InconsistentNaming


    }
}