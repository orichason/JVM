using JVMLibrary.Attribute_Infos;
using JVMLibrary.CP_Infos;
using JVMLibrary.Method_Infos;
using JVMLibrary.Tools;

using Microsoft.VisualBasic;

namespace JVM
{
    public class ClassFile
    {
        public byte[] ByteCode { get;  set; }
        public uint Magic { get; internal set; }
        public ushort MinorVersion { get; internal set; }
        public ushort MajorVersion { get; internal set; }
        public ushort ConstantPoolCount { get; internal set; }
        public CP_Info[] ConstantPool { get; internal set; }
        public ushort AccessFlags { get; internal set; }
        public ushort ThisClass { get; internal set; }
        public ushort SuperClass { get; internal set; }
        public ushort InterfacesCount { get; internal set; }
        public ushort[] Interfaces { get; internal set; }
        public ushort FieldsCount { get; internal set; }
        public ushort MethodsCount { get; internal set; }

        public ushort AttributesCount { get; internal set; }

        public Attribute_Info[] Attributes { get; internal set; }

        public Method_Info[] Methods { get; internal set; }

        public void RunFile()
        {
            ClassFileParser.AttemptParse("C:\\Users\\orich\\OneDrive\\Desktop\\ISA\\JavaTest\\Program.class");
        }
    }
}
