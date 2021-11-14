namespace Staff
{
    public static class StringExtensions
    {
        public static bool isNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        public static string TrimOrNull(this string value)
        {
            var trimmed = value?.Trim();
            return trimmed.isNullOrEmpty()
                ? null
                : trimmed;
        }
    }
}