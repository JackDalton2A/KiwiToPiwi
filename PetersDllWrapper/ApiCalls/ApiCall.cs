using System;
using System.Runtime.InteropServices;

namespace PetersDllWrapper
{
    internal abstract class ApiCall
    {
        protected readonly IntPtr AddressOfNativeMethod;
        protected ApiCall(IntPtr handleToLoadedNativeLibrary)
        {
            AddressOfNativeMethod = NativeLibrary.GetExport(handleToLoadedNativeLibrary, NativeMethodName);
        }
        protected abstract string NativeMethodName { get; }

    }
}

