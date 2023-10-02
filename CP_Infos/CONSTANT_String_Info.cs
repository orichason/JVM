using JVMLibrary.Extensions;

using System;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_String_Info : CP_Info
    {
        ushort StringIndex;
        public CONSTANT_String_Info(byte tag, ref ReadOnlySpan<byte> byteCode ) : base(tag, ref byteCode)
            =>Parse(ref byteCode);
        

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => StringIndex = byteCode.SliceU2();
    }
}
