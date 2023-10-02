using System.Formats.Asn1;

namespace JVMLibrary.CP_Infos
{
    public abstract class CP_Info
    {
        public Tags Tag { get; }
        
        public CP_Info(byte tag, ref ReadOnlySpan<byte> byteCode)
        {
            Tag = (Tags)tag;
        }
        public enum Tags
        {
            CONSTANT_Class = 7,
            CONSTANT_Fieldref = 9,
            CONSTANT_Methodref = 10,
            CONSTANT_InterfaceMethodref = 11,
            CONSTANT_String = 8,
            CONSTANT_Integer = 3,
            CONSTANT_Float = 4,
            CONSTANT_Long = 5,
            CONSTANT_Double = 6,
            CONSTANT_NameAndType = 12,
            CONSTANT_Utf8 = 1,
            CONSTANT_MethodHandle = 15,
            CONSTANT_MethodType = 16,
            CONSTANT_InvokeDynamic = 18
        }
        public abstract void Parse(ref ReadOnlySpan<byte> byteCode);
    }
}
