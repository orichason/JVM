using JVMLibrary.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_Long_Info : CP_Info
    {
        uint HighBytes;
        uint LowBytes;
        public CONSTANT_Long_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
            => Parse(ref byteCode);

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => (HighBytes, LowBytes) = (byteCode.SliceU4(), byteCode.SliceU4());
    }
}
