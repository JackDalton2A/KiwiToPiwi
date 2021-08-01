using System;

namespace PetersDllWrapper
{
    internal abstract class ApiCallPblKfGetRel : ApiCall
    {
        internal abstract int PblKfGetRel(IntPtr pblKeyFile, long relIndex, ref byte[] resultKey, ref uint resultKeyLen);


        protected ApiCallPblKfGetRel(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        protected sealed override string NativeMethodName => "pblKfGetRel";
    }
}