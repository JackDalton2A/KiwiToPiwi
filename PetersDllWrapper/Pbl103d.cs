using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace PetersDllWrapper
{
    public class Pbl103d : IDisposable
    {
        private readonly ILogger _logger;
        private IntPtr _handleNativeLibrary;

        private readonly ApiCallPblKfOpen _pblKfOpen;
        private readonly ApiCallPblKfClose _pblKfClose;
        private readonly ApiCallPblKfGetAbs _pblKfGetAbs;
        private readonly ApiCallPblKfGetRel _pblKfGetRel;
        private readonly ApiCallPblKfRead _pblKfRead;

        public Pbl103d(string libraryPath, ILogger logger)
        {
            _logger = logger;
            _logger.LogInformation($"NativeLibraryPath: {libraryPath}");

            _handleNativeLibrary = NativeLibrary.Load(libraryPath);
            if (_handleNativeLibrary != IntPtr.Zero)
            {
                _pblKfOpen = new ApiCallPblKfOpenUnsafe(_handleNativeLibrary);
                _pblKfClose = new ApiCallPblKfCloseUnsafe(_handleNativeLibrary);
                _pblKfGetAbs = new ApiCallPblKfGetAbsUnsafe(_handleNativeLibrary);
                _pblKfGetRel = new ApiCallPblKfGetRelUnsafe(_handleNativeLibrary);
                _pblKfRead = new ApiCallPblKfReadUnsafe(_handleNativeLibrary);
                
            }
        }

        public IntPtr PblKfOpen(string path, int update, IntPtr fileSetTag)
        {
            return _pblKfOpen.PblKfOpen(path, update, fileSetTag);
        }

        public int PblKfClose(IntPtr pblKeyFile)
        {
            return _pblKfClose.PblKfClose(pblKeyFile);
        }

        public int PblKfGetAbs(IntPtr pblKeyFile, int absIndex, ref byte[] resultKey, ref uint resultKeyLen)
        {
            return _pblKfGetAbs.PblKfGetAbs(pblKeyFile,absIndex,ref resultKey,ref resultKeyLen);
        }

        public int First(IntPtr pblKeyFile, ref byte[] resultKey, ref uint resultKeyLen)
        {
            return _pblKfGetAbs.PblKfGetAbs(pblKeyFile, 0, ref resultKey, ref resultKeyLen);
        }

        public int Last(IntPtr pblKeyFile, ref byte[] resultKey, ref uint resultKeyLen)
        {
            return _pblKfGetAbs.PblKfGetAbs(pblKeyFile, -1, ref resultKey, ref resultKeyLen);
        }


        public int PblKfGetRel(IntPtr pblKeyFile, long relIndex, ref byte[] resultKey, ref uint resultKeyLen)
        {
            return _pblKfGetRel.PblKfGetRel(pblKeyFile, relIndex, ref resultKey, ref resultKeyLen);
        }

        public int Next(IntPtr pblKeyFile, ref byte[] resultKey, ref uint resultKeyLen)
        {
            return _pblKfGetRel.PblKfGetRel(pblKeyFile, 1L, ref resultKey, ref resultKeyLen);
        }
        
        public int PblKfRead(IntPtr pblKeyFile, ref byte[] bufferToFill)
        {
            return _pblKfRead.PblKfRead(pblKeyFile, ref bufferToFill);
        }

        private void ReleaseUnmanagedResources()
        {
            // Dispose unmanaged resources
            if (_handleNativeLibrary != IntPtr.Zero)
            {
                NativeLibrary.Free(_handleNativeLibrary);
                _handleNativeLibrary = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~Pbl103d()
        {
            ReleaseUnmanagedResources();
        }


    }
}



//Name Address	Ordinal
//    getPblErrNo	0000000180001000	1
//pblHtCreate	00000001800015B0	2
//pblHtCurrent	0000000180001A70	3
//pblHtDelete	0000000180001C90	4
//pblHtFirst	0000000180001A00	5
//pblHtInsert	00000001800016A0	6
//pblHtLookup	00000001800018B0	7
//pblHtNext	0000000180001A30	8
//pblHtRemove	0000000180001A90	9
//pblIsamClose	00000001800028D0	10
//pblIsamCommit	00000001800022D0	11
//pblIsamDelete	00000001800038B0	12
//pblIsamFind	0000000180004720	13
//pblIsamFlush	0000000180002A50	14
//pblIsamGet	0000000180004AC0	15
//pblIsamInsert	0000000180002D90	16
//pblIsamOpen	00000001800023D0	17
//pblIsamReadData	00000001800050C0	18
//pblIsamReadDatalen	0000000180004F50	19
//pblIsamReadKey	0000000180004F30	20
//pblIsamStartTransaction	0000000180002150	21
//pblIsamUpdateData	0000000180005250	22
//pblIsamUpdateKey	00000001800059A0	23
//pblKfClose	000000018000A9B0	24
//pblKfCommit	000000018000AB30	25
//pblKfCreate	000000018000A640	26
//pblKfDelete	000000018000C570	27
//pblKfFind	000000018000CF60	28
//pblKfFlush	000000018000AA20	29
//pblKfGetAbs	000000018000D6A0	30
//pblKfGetRel	000000018000D290	31
//pblKfInit	000000018000A460	32
//pblKfInsert	000000018000B780	33
//pblKfOpen	000000018000A890	34
//pblKfRead	000000018000D170	35
//pblKfRestorePosition	000000018000AE10	36
//pblKfSavePosition	000000018000ADF0	37
//pblKfSetCompareFunction	000000018000AA10	38
//pblKfStartTransaction	000000018000ADC0	39
//pblKfUpdate	000000018000C6E0	40
//pbl_BufToLong	0000000180001360	41
//pbl_BufToShort	0000000180001330	42
//pbl_LongSize	00000001800014E0	43
//pbl_LongToBuf	0000000180001340	44
//pbl_LongToVarBuf	0000000180001380	45
//pbl_ShortToBuf	0000000180001320	46
//pbl_VarBufSize	0000000180001520	47
//pbl_VarBufToLong	0000000180001420	48
//pbl_malloc	0000000180001020	49
//pbl_malloc0	0000000180001080	50
//pbl_mem2dup	0000000180001170	51
//pbl_memcmp	00000001800012C0	52
//pbl_memcmplen	0000000180001280	53
//pbl_memdup	00000001800010F0	54
//pbl_memlcpy	0000000180001250	55
//setPblErrNo	0000000180001010	56
//DllEntryPoint	000000018000E17C	[main entry]
