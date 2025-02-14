public static class FloatExtensions
{
    public static string ToStringFixed(this float value, int decimalPlaces)
    {
        return value.ToString($"F{decimalPlaces}");
    }
}