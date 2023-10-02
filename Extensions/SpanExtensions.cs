namespace JVMLibrary.Extensions
{
    public static class SpanExtensions
    {
        public static byte SliceU1(ref this ReadOnlySpan<byte> byteCode)
        {
            byte returnByte = byteCode[0];
            byteCode = byteCode.Slice(1);

            return returnByte;
        }

        public static ushort SliceU2(ref this ReadOnlySpan<byte> byteCode)
            => (ushort)(((ushort)byteCode.SliceU1() << 8) | byteCode.SliceU1());


        public static uint SliceU4(ref this ReadOnlySpan<byte> byteCode)
          => (uint)byteCode.SliceU2() << 16 | byteCode.SliceU2();

    }
}
