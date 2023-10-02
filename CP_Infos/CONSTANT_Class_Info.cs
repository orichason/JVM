using JVMLibrary.Extensions;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_Class_Info : CP_Info
    {
        ushort NameIndex;

        public CONSTANT_Class_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
            => Parse(ref byteCode);
        

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => NameIndex = byteCode.SliceU2();
    }
}
