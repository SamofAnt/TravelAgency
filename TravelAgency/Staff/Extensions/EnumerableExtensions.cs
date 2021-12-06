
namespace Staff.Extensions
{
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> collection, string separator = "\n") =>
            string.Join(separator, collection);
    }
}
