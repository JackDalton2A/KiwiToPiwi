using System;

namespace PetersDllWrapper
{
    internal abstract class ApiCallPblKfGetAbs : ApiCall
    {
        internal abstract int PblKfGetAbs(IntPtr pblKeyFile, int relindex, ref byte[] resultKey, ref uint resultKeyLen);


        protected ApiCallPblKfGetAbs(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        protected sealed override string NativeMethodName => "pblKfGetAbs";
    }
}