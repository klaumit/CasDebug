using System.Collections.Generic;

namespace SimCore
{
    public static class CollTool
    {
        public static void AddRange<T>(this ICollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
                coll.Add(item);
        }
    }
}