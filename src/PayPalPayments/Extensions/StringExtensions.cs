
namespace System
{
    public static class StringExtensions
    {
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static double ToDouble(this string value)
        {
            return Convert.ToDouble(value);
        }

        public static DateTime ToDateTime(this string value)
        {
            return Convert.ToDateTime(value);
        }
    }
}
