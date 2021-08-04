#region License

// /*
// MIT License
// 
// Copyright (c) 2021 JackDalton2A
// XYZ
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// */

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KiwiToPiwi.KeyValueDb;
using Microsoft.Extensions.Logging.Abstractions;
using PetersDllWrapper;

namespace KiwiToPiwi
{
    internal abstract class KeyValueDbRawData
    {
        private readonly string _projectPath;
        private readonly string _destinationPath;
        private readonly string _odxPartMnemonic;
        private readonly string _fileNameWithoutExtention;
        public Dictionary<byte[], byte[]> RefToSerializedDbItemDic { get; }
        public List<DbElement> DeserializedDbItems { get;}

        public void WriteRefToSerializedDbItemDictionaryToFile()
        {
            var fullPathName = _destinationPath + _fileNameWithoutExtention + "\\";
            Directory.CreateDirectory(Path.GetDirectoryName(fullPathName) ?? "ShitHappens");

            foreach (var pair in RefToSerializedDbItemDic)
            {
                string vTableId = BitConverter.ToUInt16(pair.Value, 0).ToString("X4");

                string path;
                if (pair.Key.Length > 4)
                {
                    //Probably the ShortName or ID as key
                    path = fullPathName + "vT_" + vTableId + "__" + Encoding.ASCII.GetString(pair.Key) + ".bin";
                }
                else
                {
                    //synthetic key
                    path = fullPathName + "vT_" + vTableId + "__" + BitConverter.ToString(pair.Key) + ".bin";
                }
                File.WriteAllBytes(path, pair.Value);
            }
        }

        public void WriteDeserializeDbItemsToFile()
        {
            var fullPathName = _destinationPath + _fileNameWithoutExtention + "\\";
            Directory.CreateDirectory(Path.GetDirectoryName(fullPathName) ?? "ShitHappens");

            foreach (var item in DeserializedDbItems)
            {
                // File name  
                string fileName =  fullPathName + "vT_" + ((ushort)(item.DbElementType)).ToString("X4") + "__" + item.DbElementType + ".txt";
                FileStream stream = null;
                try
                {
                    // Create a FileStream with mode CreateNew  
                    stream = new FileStream(fileName, FileMode.Create);
                    // Create a StreamWriter from FileStream  
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        item.WriteDeserializeDataToStream(writer);
                    }
                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }
            }
        }

        public void DeserializeDbItems(AStringData aStringData, UStringData uStringData)
        {
            
            foreach (var pair in RefToSerializedDbItemDic)
            {
                var item = DbElementSimpleFactory.CreatFromByteArray(pair.Value, aStringData, uStringData);
                if (item != null)
                {
                    DeserializedDbItems.Add(item);
                }
            }
        }


        protected KeyValueDbRawData(FileInfo kvStoreToOpen, string destinationPath)
        {
            _destinationPath = destinationPath;
            _projectPath = kvStoreToOpen.DirectoryName + "\\";
            _fileNameWithoutExtention = kvStoreToOpen.Name.Remove(kvStoreToOpen.Name.Length - kvStoreToOpen.Extension.Length);  //in this case Extension is .key or .db
            _odxPartMnemonic = _fileNameWithoutExtention.Remove(0,_fileNameWithoutExtention.Length-2); //fixed size eg. sd or pr

            RefToSerializedDbItemDic = new Dictionary<byte[], byte[]>();
            DeserializedDbItems = new List<DbElement>();
            ExtractSerializedDataRaw();
        }

       // protected abstract string OdxPartMnemonic { get; }

        protected void ExtractSerializedDataRaw()
        {
            var keyToAddressDic = new Dictionary<byte[], byte[]>();
        
            var nativeLibraryPath = @"H:\DirtyDevFolder\Converter\pbl103d.dll";
            //key file
            var fullPathName = _projectPath + _fileNameWithoutExtention + ".key";
            if (File.Exists(fullPathName))
                using (var dll = new Pbl103d(nativeLibraryPath, NullLogger.Instance))
                {
                    var keyFileHandle = dll.PblKfOpen(fullPathName, 0, IntPtr.Zero);

                    byte[] buffer = new byte[2048];
                    uint lengthOfKey = (uint)buffer.Length; //caution is used as the IN and OUT param

                    int lengthOfData = dll.First(keyFileHandle, ref buffer, ref lengthOfKey);

                    do
                    {
                        var key = new byte[lengthOfKey];
                        for (uint i = 0; i < lengthOfKey; i++)
                        {
                            key[i] = buffer[i];
                        }

                        var data = new byte[lengthOfData];

                        lengthOfData = dll.PblKfRead(keyFileHandle, ref data);
                        if (data.Length != lengthOfData)
                        {
                            throw new InvalidOperationException(nameof(lengthOfData) + " != " + nameof(data.Length) );
                        }

                        keyToAddressDic.Add(key,data);

                        lengthOfKey = (uint)buffer.Length; //set again
                        lengthOfData = dll.Next(keyFileHandle, ref buffer, ref lengthOfKey);

                    } while (!(lengthOfData < 0));

                    var b = dll.PblKfClose(keyFileHandle);
                };

            //db file
            fullPathName = _projectPath + _fileNameWithoutExtention + ".db";
            if (File.Exists(fullPathName))
                using (var compressedStream = new BinaryReader(File.Open(fullPathName, FileMode.Open)))
                {
                    foreach (var pair in keyToAddressDic)
                    {
                        //get Address
                        if (pair.Value.Length >= 4)
                        {
                            uint address = pair.Value[3];
                            address <<= 8;
                            address |= pair.Value[2];
                            address <<= 8;
                            address |= pair.Value[1];
                            address <<= 8;
                            address |= pair.Value[0];
                            //The other bytes in the value are the packed, unpacked data size.
                            //But that doesn't matter to us.
                            //Another problem is that the number of bytes (for the size) changes depending on the size
                            //we let the decompress a algorithm decide how many bytes it takes

                            //take a compressed Pice
                            compressedStream.BaseStream.Position = address;
                            byte[] dumpFromPositionToEnd = new byte[compressedStream.BaseStream.Length - compressedStream.BaseStream.Position];
                            for (var i = 0; i < dumpFromPositionToEnd.Length; i++)
                            {
                                dumpFromPositionToEnd[i] = (byte)compressedStream.BaseStream.ReadByte();
                            }

                            //Decompress
                            byte[] serializedDataRaw;
                            using (var partOfCompressedStream = new MemoryStream(dumpFromPositionToEnd))
                            using (var zLipStream = new Ionic.Zlib.ZlibStream(partOfCompressedStream, Ionic.Zlib.CompressionMode.Decompress))
                            using (var resultStream = new MemoryStream())
                            {
                                zLipStream.CopyTo(resultStream);
                                serializedDataRaw = resultStream.ToArray();
                            }

                            //Dic with Key from key file an Object data from db file
                            RefToSerializedDbItemDic.Add(pair.Key, serializedDataRaw);
                        }
                        else
                        {
                            throw new InvalidOperationException("That should never happen (Address < 4)");
                        }
                    }
                }
        }
    }

    internal class SharedDataAsRawData : KeyValueDbRawData
    {
        public SharedDataAsRawData(FileInfo kvStoreToOpen, string destinationPath) : base(kvStoreToOpen, destinationPath)
        {
        }

       // protected sealed override string OdxPartMnemonic => "sd";
    }
}