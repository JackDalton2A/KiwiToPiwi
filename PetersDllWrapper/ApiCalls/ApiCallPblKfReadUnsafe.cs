using System;

namespace PetersDllWrapper
{
    internal class ApiCallPblKfReadUnsafe : ApiCallPblKfRead
    {
        internal override unsafe int PblKfRead(IntPtr pblKeyFile, ref byte[] bufferToFill)
        {
            
            void* pOutPutDataBufferOnStack = stackalloc byte[bufferToFill.Length];
            
            var dataLenResult = pblKfRead((PblKeyFile_t*) pblKeyFile, pOutPutDataBufferOnStack, bufferToFill.Length);
            for (uint i = 0; i < dataLenResult; i++)
            {
                bufferToFill[i] = ((byte*) pOutPutDataBufferOnStack)[i];
            }

            return dataLenResult;
        }

        internal ApiCallPblKfReadUnsafe(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        // should look like the C function as much as possible.
        // ReSharper disable InconsistentNaming
        private unsafe int pblKfRead(
            PblKeyFile_t* k,  /*key file to read from  */
            void* data,   /* data to insert */
            int datalen  /* length of the data  */
        )
        {
            return ((delegate* unmanaged[Stdcall]<PblKeyFile_t*, void*, int, int>)AddressOfNativeMethod)(k, data, datalen);
        }
        // ReSharper restore InconsistentNaming


    }
}