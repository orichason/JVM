
using JVMLibrary.Attribute_Infos;
using JVMLibrary.CP_Infos;
using JVMLibrary.Method_Infos;

using System.Reflection.Metadata;


namespace JVMLibrary.Tools
{
    public class Emulator
    {
        public void Emulate(Method_Info method)
        {
            Stack<StackFrame> stack = new();
            //stack2.Push(new StackFrame(3));
            //uint[] localVariables = new uint[3];
            //Stack<uint> stack = new();

            for (int i = 0; i < method.Attributes.Length; i++)
            {
                if (method.Attributes[i].AttributeNameIndex == 0x7)
                {
                    ParseAttribute(method.Attributes[i], ref stack);
                }
            }
        }

        private void ParseAttribute(Attribute_Info attribute, ref Stack<StackFrame> stack)
        {
            stack.Push(new StackFrame(((Code_Attribute)attribute).MaxLocals));
            int byteCount = 0;
            for (int i = 0; i < ((Code_Attribute)attribute).Code.Length; i++)
            {
                switch (((Code_Attribute)attribute).Code[byteCount])
                {
                    case 0x03:
                        stack.Peek().stack.Push(0);
                        byteCount++;
                        break;
                    case 0x04:
                        stack.Peek().stack.Push(1);
                        byteCount++;
                        break;
                    case 0x05:
                        stack.Peek().stack.Push(2);
                        byteCount++;
                        break;
                    case 0x06:
                        stack.Peek().stack.Push(3);
                        byteCount++;
                        break;
                    case 0x07:
                        stack.Peek().stack.Push(4);
                        byteCount++;
                        break;
                    case 0x08:
                        stack.Peek().stack.Push(5);
                        byteCount++;
                        break;
                    case 0x10:
                        stack.Peek().stack.Push(((Code_Attribute)attribute).Code[byteCount + 1]);
                        byteCount += 2;
                        break;
                    case 0x3c:
                        stack.Peek().localVariables[0] = stack.Peek().stack.Pop();
                        byteCount++;
                        break;
                    case 0x3d:
                        stack.Peek().localVariables[1] = stack.Peek().stack.Pop();
                        byteCount++;
                        break;
                    case 0x1b:
                        stack.Peek().stack.Push(stack.Peek().localVariables[0]);
                        byteCount++;
                        break;
                    case 0x1c:
                        stack.Peek().stack.Push(stack.Peek().localVariables[1]);
                        byteCount++;
                        break;
                    case 0x60:
                        uint val1 = stack.Peek().stack.Pop();
                        uint val2 = stack.Peek().stack.Pop();
                        stack.Peek().stack.Push(val1 + val2);
                        byteCount++;
                        break;
                    case 0x3e:
                        stack.Peek().localVariables[2] = stack.Peek().stack.Pop();
                        byteCount++;
                        break;
                    case 0xb8:
                        byte indexbyte1 = ((Code_Attribute)attribute).Code[byteCount + 1];
                        byte indexbyte2 = ((Code_Attribute)attribute).Code[byteCount + 2];
                        for (int j = 0; j < classFile.MethodsCount; j++)
                        {
                            //Find methodRef with name and type index
                            
                        }
                        //Emulate(((CONSTANT_MethodRef_Info)classFile.ConstantPool[((indexbyte1 << 8) | indexbyte2) - 1]).);
                        //stack.Peek().stack.Push(classFile.ConstantPool[(indexbyte1 << 8) | indexbyte2].);
                        break;
                    case 0xb1:
                        break;
                    default:
                        throw new Exception("Missing instruction" + ((Code_Attribute)attribute).Code[byteCount]);
                }
            }
        }
    }
}

