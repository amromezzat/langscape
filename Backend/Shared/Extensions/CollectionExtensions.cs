using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> array)
        {
            return array == null || array.Count() == 0;
        }
    }
}