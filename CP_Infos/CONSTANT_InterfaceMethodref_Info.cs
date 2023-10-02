using JVMLibrary.Extensions;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_InterfaceMethodref_Info : CP_Info
    {
        ushort ClassIndex;

        ushort NameAndTypeIndex;

        public CONSTANT_InterfaceMethodref_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
            => Parse(ref byteCode);

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => (ClassIndex, NameAndTypeIndex) = (byteCode.SliceU2(), byteCode.SliceU2());

    }
}
