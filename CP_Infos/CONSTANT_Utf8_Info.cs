using JVMLibrary.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_Utf8_Info : CP_Info
    {
        ushort Length;
        public byte[] Bytes;
        public CONSTANT_Utf8_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
        {
            Length = byteCode.SliceU2();
            Bytes = new byte[Length];
            Parse(ref byteCode);
        }

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
        {
            for (int i = 0; i < Length; i++)
            {
                Bytes[i] = byteCode.SliceU1();
            }
        }
    }
}
