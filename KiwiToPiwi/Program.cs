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
using System.Data;
using System.Formats.Asn1;
using System.IO;
using System.IO.Compression;
using KiwiToPiwi.KeyValueDb;
using PetersDllWrapper;

namespace KiwiToPiwi
{
    //FYI inspired by....
    //https://stackoverflow.com/questions/9050260/what-does-a-zlib-header-look-like
    //https://github.com/peterGraf/pbl
    //https://github.com/gennad/spamprobe/tree/b957379a659f6d8195f9248baf0e44b22334b306
    //http://zlib.net/
    //https://aluigi.altervista.org/mytoolz.htm#offzip
    //http://aluigi.org/mytoolz/offzip.zip

    class Program
    {
        static void Main(string[] args)
        {
            var outcomePath = @"H:\DirtyDevFolder\DataNoTextDb\";
            var targetPath = @"H:\DirtyDevFolder\dummyProjects\rtDataNoTextDb\";

            //var outcomePath = @"H:\DirtyDevFolder\DataWithTextDb\";
            //var targetPath = @"H:\DirtyDevFolder\dummyProjects\rtDataWithTextDb\";

            Console.WriteLine("Hello P-Car lovers!");

            //Attention... FYI that happens in the original target folder
            Gzip.DecompressAllGZipFilesDirectory(targetPath);

            var ascData = new AStringData(targetPath, outcomePath);
            var unicodeData = new UStringData(targetPath, outcomePath);
            ascData.WriteDictionaryToFile();
            unicodeData.WriteDictionaryToFile();

            DirectoryInfo directorySelected = new DirectoryInfo(targetPath);

            foreach (FileInfo dbFileToOpen in directorySelected.GetFiles("*.key"))
            {
                var t = new SharedDataAsRawData(dbFileToOpen, outcomePath);
                t.WriteRefToSerializedDbItemDictionaryToFile();
                t.DeserializeDbItems(ascData,unicodeData);
                t.WriteDeserializeDbItemsToFile();

            }

        }
    }
}
