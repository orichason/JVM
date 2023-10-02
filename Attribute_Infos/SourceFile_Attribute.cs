using JVMLibrary.Extensions;

namespace JVMLibrary.Attribute_Infos
{
    public class SourceFile_Attribute : Attribute_Info
    {
        uint AttributeLength;
        ushort SourceFileIndex;
        public SourceFile_Attribute(ushort AttributeNameIndex, ref ReadOnlySpan<byte> byteCode) : base(AttributeNameIndex, ref byteCode)
            => Parse(ref byteCode);

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
        {
            (AttributeLength, SourceFileIndex) = (byteCode.SliceU4(), (ushort)(byteCode.SliceU2() - 1));
        }
    }
}
