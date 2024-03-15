namespace WinterCrestal.Extensions
{
    public static class NumbersExtension
    {
        public static bool IsBitSet(this byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static byte SetBit(this byte b, int pos, bool value)
        {
            byte mask = (byte)(1 << pos);
            return value ? (byte)(b | mask) : (byte)(b & ~mask);
        }
    }
}
