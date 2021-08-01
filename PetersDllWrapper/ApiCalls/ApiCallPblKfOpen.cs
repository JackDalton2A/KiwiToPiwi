using System;

namespace PetersDllWrapper
{
    internal abstract class ApiCallPblKfOpen : ApiCall
    {
        internal abstract IntPtr PblKfOpen(string path, int update, IntPtr fileSetTag);


        protected ApiCallPblKfOpen(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        protected sealed override string NativeMethodName => "pblKfOpen";
    }
}