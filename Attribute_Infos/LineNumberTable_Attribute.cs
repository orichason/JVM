using JVMLibrary.Extensions;

namespace JVMLibrary.Attribute_Infos
{
    public class LineNumberTable_Attribute : Attribute_Info
    {
        uint AttributeLength;
        ushort LineNumberTableLength;

        struct Table
        {
            public ushort start_pc;
            public ushort line_number;
        }

        Table[] LineNumberTable;
        public LineNumberTable_Attribute(ushort AttributeNameIndex, ref ReadOnlySpan<byte> byteCode) : base(AttributeNameIndex, ref byteCode)
            => Parse(ref byteCode);

        public override void Parse(ref ReadOnlySpan<byte> byteCode)
        {
            (AttributeLength, LineNumberTableLength) = (byteCode.SliceU4(), byteCode.SliceU2());

            LineNumberTable = new Table[LineNumberTableLength];

            for (int i = 0; i < LineNumberTable.Length; i++)
            {
                LineNumberTable[i].start_pc = byteCode.SliceU2();
                LineNumberTable[i].line_number = byteCode.SliceU2();
            }
        }

    }
}
