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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KiwiToPiwi
{
    internal abstract class StringData
    {
        private readonly string _projectPath;
        private readonly string _destinationPath;
        public Dictionary<uint, string> TextDic { get; }

        public void WriteDictionaryToFile()
        {
            if (TextDic.Count > 0)
            {
                var fullPathName = _destinationPath + NameOfFilePair + ".txt";
                Directory.CreateDirectory(Path.GetDirectoryName(fullPathName) ?? "ShitHappens");
                File.WriteAllLines(fullPathName, TextDic.Select(x => "0x" + x.Key.ToString("X8") + "\t" + Regex.Replace(x.Value, @"\r\n?|\n", "( \\n )") ));
            }
        }

        public string this[uint index] // indexer declaration
        {
            get => TextDic[index];
            //set => TextDic[index] = value;
        }



        protected StringData(string projectPath, string destinationPath)
        {
            _destinationPath = destinationPath;
            _projectPath = projectPath;
            TextDic = new Dictionary<uint, string>();
            BuildTextDictionary();
        }

        protected abstract string NameOfFilePair { get; }

        protected abstract string TextEncoding(byte[] textBytes);
        protected abstract int RealTextByteSize(uint size);

        protected void BuildTextDictionary()
        {
            var keyToAddressDic = new Dictionary<uint, uint>();


            //idx file to Dictionary with Id/Address pair
            var fullPathName = _projectPath + NameOfFilePair + ".idx";
            if (File.Exists(fullPathName))
                using (var reader = new BinaryReader(File.Open(fullPathName, FileMode.Open)))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                        try
                        {
                            keyToAddressDic.Add(
                                reader.ReadUInt32(),//Id
                                reader.ReadUInt32() //Address in data file 
                            );
                        }
                        catch (EndOfStreamException e)
                        {
                            if (reader.BaseStream.Position == reader.BaseStream.Length)
                            {
                                Debug.WriteLine("This index file has a key at the end without an address assignment!");
                                Debug.WriteLine("Maybe a bug or I didn't understand something.");
                            }
                            else
                            {
                                throw;
                            }
                        }
                }

            //data file
            fullPathName = _projectPath + NameOfFilePair + ".data";
            if (File.Exists(fullPathName))
                using (var reader = new BinaryReader(File.Open(fullPathName, FileMode.Open)))
                {
                    foreach (var pair in keyToAddressDic)
                    {
                        //get Text from data file
                        reader.BaseStream.Position = pair.Value;
                        var size = reader.ReadUInt32();
                        var textAsBytes = reader.ReadBytes(RealTextByteSize(size));

                        //Dic with Id from idx file an text from data file
                        TextDic.Add(pair.Key, TextEncoding(textAsBytes));
                    }
                }
        }


    }

    internal class AStringData : StringData
    {
        public AStringData(string projectPath, string destinationPath) : base(projectPath, destinationPath)
        {
        }

        protected sealed override string NameOfFilePair => "AStringData";
        protected override string TextEncoding(byte[] textBytes)
        {
            return Encoding.ASCII.GetString(textBytes);
        }

        protected override int RealTextByteSize(uint size)
        {
            return (int) size;
        }
    }

    internal class UStringData : StringData
    {
        public UStringData(string projectPath, string destinationPath) : base(projectPath, destinationPath)
        {
        }

        protected sealed override string NameOfFilePair => "UStringData";
        protected override string TextEncoding(byte[] textBytes)
        {
            return Encoding.Unicode.GetString(textBytes);
        }

        protected override int RealTextByteSize(uint size)
        {
            return (int) size * 2;
        }
    }
}
