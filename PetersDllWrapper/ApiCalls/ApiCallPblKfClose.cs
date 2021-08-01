using System;

namespace PetersDllWrapper
{
    internal abstract class ApiCallPblKfClose : ApiCall
    {
        internal abstract int PblKfClose(IntPtr pblKeyFile);


        protected ApiCallPblKfClose(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        protected sealed override string NativeMethodName => "pblKfClose";
    }
}