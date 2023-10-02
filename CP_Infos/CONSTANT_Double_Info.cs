using JVMLibrary.Extensions;

namespace JVMLibrary.CP_Infos
{
    public class CONSTANT_Double_Info : CP_Info
    {
        uint HighBytes;
        uint LowBytes;
        public CONSTANT_Double_Info(byte tag, ref ReadOnlySpan<byte> byteCode) : base(tag, ref byteCode)
            => Parse(ref byteCode);
        public override void Parse(ref ReadOnlySpan<byte> byteCode)
            => (HighBytes, LowBytes) = (byteCode.SliceU4(), byteCode.SliceU4());
    }
}
