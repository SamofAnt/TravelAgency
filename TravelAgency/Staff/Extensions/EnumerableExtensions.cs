using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff.Extensions
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> collection, string separator = "\n") =>
            string.Join(separator, collection);
    }
}
