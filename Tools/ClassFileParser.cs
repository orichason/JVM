using JVM;

using JVMLibrary.Attribute_Infos;
//using JVM.CP_Info;

using JVMLibrary.CP_Infos;
using JVMLibrary.Extensions;
using JVMLibrary.Method_Infos;

using System.Security.Cryptography;
using System.Text;

namespace JVMLibrary.Tools
{
    public static class ClassFileParser
    {
        public static bool AttemptParse(string classFilePath)
        {
            classFile.ByteCode = File.ReadAllBytes("C:\\Users\\orich\\OneDrive\\Desktop\\ISA\\JavaTest\\Program.class");

            ReadOnlySpan<byte> byteCode = classFile.ByteCode.AsSpan();

            classFile.Magic = byteCode.SliceU4();

            if (!VerifyMagic(classFile.Magic))
            {
                return false;
            }

            classFile.MinorVersion = byteCode.SliceU2();
            classFile.MajorVersion = byteCode.SliceU2();

            ConstantPoolParser(classFile, ref byteCode);

            classFile.AccessFlags = byteCode.SliceU2();
            classFile.ThisClass = byteCode.SliceU2();
            classFile.SuperClass = byteCode.SliceU2();
            classFile.InterfacesCount = byteCode.SliceU2();

            LoadInterfaces(classFile, ref byteCode);

            classFile.FieldsCount = byteCode.SliceU2();

            if (classFile.FieldsCount != 0)
            {
                FieldInfoParser(classFile, ref byteCode);
            }

            classFile.MethodsCount = byteCode.SliceU2();
            MethodsParser(classFile, ref byteCode);

            classFile.AttributesCount = byteCode.SliceU2();
            classFile.Attributes = new Attribute_Info[classFile.AttributesCount];

            ParseAttributesArray(classFile, ref byteCode);

            Method_Info main = FindMain(classFile);

            Emulate(main);

            return true;
        }

        private static void Emulate(Method_Info method)
        {
            Emulator emulate = new();

            emulate.Emulate(method);
        }

        private static void MethodsParser(ClassFile classFile, ref ReadOnlySpan<byte> byteCode)
        {
            classFile.Methods = new Method_Info[classFile.MethodsCount];
            for (int i = 0; i < classFile.MethodsCount; i++)
            {
                Method_Info method_Info = new Method_Info(ref byteCode);

                method_Info.Attributes = new Attribute_Info[method_Info.AttributesCount];

                for (int j = 0; j < method_Info.Attributes.Length; j++)
                {
                    method_Info.Attributes[j] = AttributeParser(classFile, ref byteCode);
                }

                classFile.Methods[i] = method_Info;
            }
        }

        private static void ParseAttributesArray(ClassFile classFile, ref ReadOnlySpan<byte> byteCode)
        {
            for (int i = 0; i < classFile.Attributes.Length; i++)
            {
                classFile.Attributes[i] = AttributeParser(classFile, ref byteCode);
            }
        }

        public static Attribute_Info AttributeParser(ClassFile classFile, ref ReadOnlySpan<byte> byteCode)
        {
            ushort attributeNameIndex = byteCode.SliceU2();

            string value = Encoding.UTF8.GetString(((CONSTANT_Utf8_Info)classFile.ConstantPool[attributeNameIndex - 1]).Bytes);
            switch (value)
            {
                case "Code":
                    Code_Attribute code_Attribute = new Code_Attribute(attributeNameIndex, ref byteCode);
                    return code_Attribute;
                case "LineNumberTable":
                    LineNumberTable_Attribute lineNumberTable_Attribute = new LineNumberTable_Attribute(attributeNameIndex, ref byteCode);
                    return lineNumberTable_Attribute;
                case "SourceFile":
                    SourceFile_Attribute sourceFile_Attribute = new SourceFile_Attribute(attributeNameIndex, ref byteCode);
                    return sourceFile_Attribute;
            };

            return null;
        }

        private static void ConstantPoolParser(ClassFile classFile, ref ReadOnlySpan<byte> byteCode)
        {
            classFile.ConstantPoolCount = byteCode.SliceU2();

            classFile.ConstantPool = new CP_Info[classFile.ConstantPoolCount - 1];

            for (int i = 0; i < classFile.ConstantPool.Length; i++)
            {
                byte tag = byteCode.SliceU1();
                switch (tag)
                {
                    case 0x07:
                        classFile.ConstantPool[i] = new CONSTANT_Class_Info(tag, ref byteCode);
                        break;
                    case 0x09:
                        classFile.ConstantPool[i] = new CONSTANT_Fieldref_Info(tag, ref byteCode);
                        break;
                    case 0x0a:
                        classFile.ConstantPool[i] = new CONSTANT_MethodRef_Info(tag, ref byteCode);
                        break;
                    case 0x0b:
                        classFile.ConstantPool[i] = new CONSTANT_InterfaceMethodref_Info(tag, ref byteCode);
                        break;
                    case 0x08:
                        classFile.ConstantPool[i] = new CONSTANT_String_Info(tag, ref byteCode);
                        break;
                    case 0x03:
                        classFile.ConstantPool[i] = new CONSTANT_Integer_Info(tag, ref byteCode);
                        break;
                    case 0x04:
                        classFile.ConstantPool[i] = new CONSTANT_Float_Info(tag, ref byteCode);
                        break;
                    case 0x05:
                        classFile.ConstantPool[i] = new CONSTANT_Long_Info(tag, ref byteCode);
                        break;
                    case 0x06:
                        classFile.ConstantPool[i] = new CONSTANT_Double_Info(tag, ref byteCode);
                        break;
                    case 0x0c:
                        classFile.ConstantPool[i] = new CONSTANT_NameAndType_Info(tag, ref byteCode);
                        break;
                    case 0x01:
                        classFile.ConstantPool[i] = new CONSTANT_Utf8_Info(tag, ref byteCode);
                        break;
                    case 0x0f:
                        classFile.ConstantPool[i] = new CONSTANT_MethodHandle_Info(tag, ref byteCode);
                        break;
                    case 0x10:
                        classFile.ConstantPool[i] = new CONSTANT_MethodType_Info(tag, ref byteCode);
                        break;
                }

            }
        }

        private static void LoadInterfaces(ClassFile classFile, ref ReadOnlySpan<byte> byteCode)
        {
            classFile.Interfaces = new ushort[classFile.InterfacesCount];

            for (int i = 0; i < classFile.Interfaces.Length; i++)
            {
                classFile.Interfaces[i] = byteCode.SliceU2();
            }
        }

        private static void FieldInfoParser(ClassFile classFile, ref ReadOnlySpan<byte> byteCode)
        {
            switch (byteCode.SliceU2())
            {
                default:
                    throw new Exception();
                    break;
            }
        }
        private static bool VerifyMagic(uint magic)
            => magic == 0xCAFEBABE;

        private static Method_Info FindMain(ClassFile classFile)
        {
            for (int i = 0; i < classFile.Methods.Length; i++)
            {
                string methodName = "";
                string parameters = "";

                if (classFile.Methods[i].AccessFlag != 0x09)
                {
                    continue;
                }

                for (int j = 0; j < ((CONSTANT_Utf8_Info)classFile.ConstantPool[classFile.Methods[i].NameIndex]).Bytes.Length; j++)
                {
                    methodName += ((char)((CONSTANT_Utf8_Info)classFile.ConstantPool[classFile.Methods[i].NameIndex]).Bytes[j]).ToString();
                }

                for (int k = 0; k < ((CONSTANT_Utf8_Info)classFile.ConstantPool[classFile.Methods[i].DescriptorIndex]).Bytes.Length; k++)
                {
                    parameters += ((char)((CONSTANT_Utf8_Info)classFile.ConstantPool[classFile.Methods[i].DescriptorIndex]).Bytes[k]).ToString();
                }

                if (parameters != "([Ljava/lang/String;)V" || methodName != "main")
                {
                    continue;
                }


                return classFile.Methods[i];

            }

            throw new Exception("Main not found");
        }
    }
}
