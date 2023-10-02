namespace JVMLibrary.Attribute_Infos
{
    public abstract class Attribute_Info
    {
        public ushort AttributeNameIndex { get; set; }
        public uint AttributeLength { get; internal set; }

        public byte[] Info;

        public Attribute_Info(ushort AttributeNameIndex, ref ReadOnlySpan<byte> byteCode)
        {
            this.AttributeNameIndex = AttributeNameIndex;
        }

        public abstract void Parse(ref ReadOnlySpan<byte> byteCode);
    }
}
