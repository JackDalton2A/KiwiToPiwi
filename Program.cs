using System;
using System.Formats.Asn1;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.Logging.Abstractions;
using PetersDllWrapper;

namespace KiwiToPiwi
{
    class Program
    {
        static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            // new Ionic.Zlib.ZlibStream(ms, Ionic.Zlib.CompressionMode.Decompress, Ionic.Zlib.CompressionLevel.Default, true)
            using (var zipStream = new Ionic.Zlib.ZlibStream(compressedStream, Ionic.Zlib.CompressionMode.Decompress ))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
        static void Main(string[] args)
        {
            var nativeLibraryPath = @"D:\PiwisApps\PIDT\PIDT_MIG\DLS3_64\Converter\pbl103d_conv_vc120_64.dll";
            Console.WriteLine("Hello World!");
            var dll = new Pbl103d(nativeLibraryPath, NullLogger.Instance);
            var baseName = @"0.0.0@Getriebesteuerung_PDK.bv";
            var file = @"D:\PiwisApps\PIDT\PIDT_MIG\Projects-V\9x7\" + baseName + ".key";
            var fileDb = @"D:\PiwisApps\PIDT\PIDT_MIG\Projects-V\9x7\" + baseName + ".db" ;




            var keyFileHandle = dll.PblKfOpen(file, 0, IntPtr.Zero);

            byte[] Buffer = new byte[2048];
            uint LenBuffer = (uint)Buffer.Length;

            int dataLen = dll.First(keyFileHandle, ref Buffer, ref LenBuffer);
            do
            {            
                
                byte[] key= new byte[LenBuffer];
                for (uint i = 0; i < LenBuffer; i++)
                {
                    key[i] = Buffer[i];
                }
                Console.Write(BitConverter.ToString(key) +  " dataSize: "+ dataLen + " -> " );
                LenBuffer = (uint)Buffer.Length;
              
                dataLen = dll.PblKfRead(keyFileHandle, ref Buffer, dataLen);
                byte[] data = new byte[dataLen];
                for (uint i = 0; i < dataLen; i++)
                {
                    data[i] = Buffer[i];
                }

                Console.Write(BitConverter.ToString(data));

                if (data.Length >= 4)
                {
                    uint index = data[3];
                    index <<= 8;
                    index |= data[2];
                    index <<= 8;
                    index |= data[1];
                    index <<= 8;
                    index |= data[0];
                    
                    
                    var dump = GetDump(fileDb, index);
                    var decData = Decompress(dump);
                    File.WriteAllBytes(baseName + "_"+ BitConverter.ToString(key) +"___" + BitConverter.ToString(data) + ".bin", decData);
                }
                
                
                
                
                Console.WriteLine();
                LenBuffer = (uint)Buffer.Length;
                dataLen = dll.Next(keyFileHandle, ref Buffer, ref LenBuffer);
                
            } while (!(dataLen < 0));

            var b = dll.PblKfClose(keyFileHandle);
            dll.Dispose();
        }

        private static byte[] GetDump(string fileDb, uint index)
        {
            byte[] dump = new byte[] { };
            if (File.Exists(fileDb))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileDb, FileMode.Open)))
                {
                    var a = reader.BaseStream;
                    a.Position = index;
                    dump = new byte[a.Length - a.Position];
                    for (var i = 0; i < dump.Length; i++) dump[i] = (byte) a.ReadByte();
                }
            }

            return dump;
        }
    }
}
