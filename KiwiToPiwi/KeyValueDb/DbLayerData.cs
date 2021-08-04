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
using System.Text;

namespace KiwiToPiwi.KeyValueDb
{
    internal class DbLayerData : DbElement
    {
        private List<string> _dbKeyFileRefs;

        public DbLayerData(byte[] dbBytes, AStringData aStringData, UStringData uStringData) : base(dbBytes, aStringData, uStringData)
        {
        }

        protected override void Parse(BinaryReader br)
        {
            
            _dbKeyFileRefs = new List<string>();
            uint id = 0;
            try
            {
                while ((id = br.ReadUInt32()) != 0x100)
            {
                if (id == 0)
                    continue;
                if (id == 0x105 )
                {
                    _dbKeyFileRefs.Add("-------------------");
                    br.ReadBytes(24);
                    continue;
                }

                if (IsInlineText(id))
                {
                    var textSize = id & 0x7FFF_FFFF;

                    _dbKeyFileRefs.Add(Encoding.ASCII.GetString(br.ReadBytes((int) textSize)));
                }
                else
                {
                    _dbKeyFileRefs.Add(AStringData[id]);
                }
            }
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine(e);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }

        public override void WriteDeserializeDataToStream(StreamWriter writer)
        {
            foreach (var keyFileRef in _dbKeyFileRefs)
            {
                writer.WriteLine(keyFileRef);
            }

        }
    }
}