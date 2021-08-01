using System;
using System.Runtime.InteropServices;

namespace PetersDllWrapper
{
    internal class ApiCallPblKfGetAbsUnsafe : ApiCallPblKfGetAbs
    {
        internal override unsafe int PblKfGetAbs(IntPtr pblKeyFile, int relindex, ref byte[] resultKey,
            ref uint resultKeyLen)
        {
            uint keyLen;
            void* pOutPutKeyBufferOnStack = stackalloc byte[resultKey.Length];
            
            var dataLen = pblKfGetAbs((PblKeyFile_t*) pblKeyFile, relindex, pOutPutKeyBufferOnStack, (UIntPtr*) (&keyLen));
            for (uint i = 0; i < keyLen; i++)
            {
                resultKey[i] = ((byte*) pOutPutKeyBufferOnStack)[i];
            }

            resultKeyLen = keyLen;
            return dataLen;
        }

        internal ApiCallPblKfGetAbsUnsafe(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        // should look like the C function as much as possible.
        // ReSharper disable InconsistentNaming
        private unsafe int pblKfGetAbs(
            PblKeyFile_t* k, /* key file to position in                */
            int relindex,   /* index of record to positon to          */
            void* okey,      /* buffer for result key                  */
            UIntPtr* okeylen /* length of the result key after return  */
        )
        {
            return ((delegate* unmanaged[Stdcall]<PblKeyFile_t*, int, void*, UIntPtr*, int>)AddressOfNativeMethod)(k, relindex, okey, okeylen);
        }
        // ReSharper restore InconsistentNaming


    }
}