using System.Collections.Generic;

namespace SharpDrill
{
    public static class CollectionExtensions
    {
        public static void AddRange<TElement>(this ICollection<TElement> collection, IEnumerable<TElement> elementsToAdd)
        {
            foreach(var element in elementsToAdd)
            {
                collection.Add(element);
            }
        }
    }
}
