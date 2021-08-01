using System;

namespace PetersDllWrapper
{
    internal abstract class ApiCallPblKfRead : ApiCall
    {
        internal abstract int PblKfRead(IntPtr pblDataFile, ref byte[] bufferToFill);


        protected ApiCallPblKfRead(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        protected sealed override string NativeMethodName => "pblKfRead";
    }
}