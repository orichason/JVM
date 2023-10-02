using JVMLibrary.Extensions;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_Integer_Info : CP_Info
    {
        uint Bytes;

        public CONSTANT_Integer_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
            => Parse(ref byteCode);

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => Bytes = byteCode.SliceU4();
    }
}
