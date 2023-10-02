using JVMLibrary.Extensions;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_MethodRef_Info : CP_Info
    {
        ushort ClassIndex;
        public ushort NameAndTypeIndex;

        public CONSTANT_MethodRef_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
            => Parse(ref byteCode);
        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => (ClassIndex, NameAndTypeIndex) = (byteCode.SliceU2(), byteCode.SliceU2());
        
    }
}
