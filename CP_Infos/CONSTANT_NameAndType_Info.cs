using JVMLibrary.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_NameAndType_Info : CP_Info
    {
        ushort NameIndex;
        ushort DescriptorIndex;
        public CONSTANT_NameAndType_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
            => Parse(ref byteCode);

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => (NameIndex, DescriptorIndex) = (byteCode.SliceU2(), byteCode.SliceU2());
    }
}
