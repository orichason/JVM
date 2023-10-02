namespace JVMLibrary.Tools
{
    public class StackFrame
    {
        public Stack<uint> stack;
        public uint[] localVariables;

        public StackFrame(uint maxLocals)
        {
            localVariables = new uint[maxLocals];
            stack = new Stack<uint>();
        }
    }
}
