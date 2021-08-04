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
    abstract class DbElement
    {
        protected readonly AStringData AStringData;
        protected readonly UStringData UStringData;
        protected abstract void Parse(BinaryReader br);

        public VTableIds DbElementType { get; private set; }

        protected DbElement(byte[] dbBytes, AStringData aStringData, UStringData uStringData)
        {
            AStringData = aStringData;
            UStringData = uStringData;
            var lth = dbBytes.Length;
            if ((dbBytes[lth - 1] == 0x00) && (dbBytes[lth - 2] == 0x3E) && (dbBytes[lth - 3] == 0x23))
            {
                using (BinaryReader reader =
                    new BinaryReader(new MemoryStream(dbBytes, 0, dbBytes.Length - 3, false, true)))
                {
                    DbElementType = (VTableIds) reader.ReadUInt16();
                    Parse(reader);
                }
            }
            else
            {
                throw new InvalidOperationException(nameof(dbBytes) + "EndMarker is not 0x233E00");
            }
        }

        public abstract void WriteDeserializeDataToStream(StreamWriter writer);
        

        protected bool IsInlineText(uint id)
        {
            return (id & 0x8000_0000) == 0x8000_0000;
        }

    }

    internal class DbKeyVector : DbElement
    {
        private List<string> _dbKeyFileRefs;

        public DbKeyVector(byte[] dbBytes, AStringData aStringData, UStringData uStringData) : base(dbBytes, aStringData, uStringData)
        {
        }

        protected override void Parse(BinaryReader br)
        {
            var items = br.ReadUInt16();
            _dbKeyFileRefs = new List<string>(items);
            
            for (int i = 0; i < items; i++)
            {
                var id = br.ReadUInt32();
                if (IsInlineText(id))
                {
                    var textSize = id & 0x7FFF_FFFF;
                    
                    _dbKeyFileRefs.Add(Encoding.ASCII.GetString(br.ReadBytes((int)textSize)));
                }
                else
                {
                    _dbKeyFileRefs.Add(AStringData[id]);
                }
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