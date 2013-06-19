using System.Collections;

namespace Blog.Core
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}