using System.Collections.Generic;
using System.Linq;

namespace BingRestServices.Extensions
{
    public static class KeyTypeArrayExtensions
    {
        public static string ToCSVString(this IEnumerable<KeyType> array)
        {
            return string.Join(",", array.Select(p => p.Key).ToArray());
        }
    }
}