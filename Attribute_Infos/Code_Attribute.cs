using JVMLibrary.Extensions;
using JVMLibrary.Tools;

namespace JVMLibrary.Attribute_Infos
{
    public class Code_Attribute : Attribute_Info
    {
        uint AttributeLength;
        public ushort MaxStack;
        public ushort MaxLocals;
        public uint CodeLength;
        public byte[] Code;
        ushort ExceptionTableLength;

        public class PC
        {
            public ushort StartPC;
            public ushort EndPC;
            public ushort HandlerPC;
            public ushort CatchType;
        }
        PC[] ExceptionTable;
        ushort AttributesCount;
        Attribute_Info[] Attributes;

        public Code_Attribute(ushort AttributeNameIndex, ref ReadOnlySpan<byte> byteCode) : base(AttributeNameIndex, ref byteCode)
            => Parse(ref byteCode);

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
        {
            AttributeLength = byteCode.SliceU4();
            MaxStack = byteCode.SliceU2();
            MaxLocals = byteCode.SliceU2();
            CodeLength = byteCode.SliceU4();

            Code = new byte[CodeLength];
            for (int i = 0; i < Code.Length; i++)
            {
                Code[i] = byteCode.SliceU1();
            }

            ExceptionTableLength = byteCode.SliceU2();

            if(ExceptionTableLength != 0)
            {
                throw new Exception("Make table");
            }

            ExceptionTable = new PC[ExceptionTableLength];

            for (int i = 0; i < ExceptionTable.Length; i++)
            {
               //finish this
            }

            AttributesCount = byteCode.SliceU2();

            Attributes = new Attribute_Info[AttributesCount];
            for (int i = 0; i < Attributes.Length; i++)
            {
                Attributes[i] = ClassFileParser.AttributeParser(classFile, ref byteCode);
            }
            
        }
    }
}
