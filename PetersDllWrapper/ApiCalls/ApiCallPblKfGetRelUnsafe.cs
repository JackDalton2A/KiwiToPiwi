using System;

namespace PetersDllWrapper
{
    internal class ApiCallPblKfGetRelUnsafe : ApiCallPblKfGetRel
    {
        internal override unsafe int PblKfGetRel(IntPtr pblKeyFile, long relIndex, ref byte[] resultKey,
            ref uint resultKeyLen)
        {
            uint keyLen;
            void* pOutPutKeyBufferOnStack = stackalloc byte[resultKey.Length];
            
            var dataLen = pblKfGetRel((PblKeyFile_t*) pblKeyFile, relIndex, pOutPutKeyBufferOnStack, (UIntPtr*) (&keyLen));
            for (uint i = 0; i < keyLen; i++)
            {
                resultKey[i] = ((byte*) pOutPutKeyBufferOnStack)[i];
            }

            resultKeyLen = keyLen;
            return dataLen;
        }

        internal ApiCallPblKfGetRelUnsafe(IntPtr handleToLoadedNativeLibrary) : base(handleToLoadedNativeLibrary)
        {
        }

        // should look like the C function as much as possible.
        // ReSharper disable InconsistentNaming
        private unsafe int pblKfGetRel(
            PblKeyFile_t* k, /* key file to position in                */
            long Relindex,   /* index of record to positon to          */
            void* okey,      /* buffer for result key                  */
            UIntPtr* okeylen /* length of the result key after return  */
        )
        {
            return ((delegate* unmanaged[Stdcall]<PblKeyFile_t*, long, void*, void*, int>)AddressOfNativeMethod)(k, Relindex, okey, okeylen);
        }
        // ReSharper restore InconsistentNaming


    }
}