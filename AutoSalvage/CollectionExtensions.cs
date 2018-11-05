using System.Collections.Generic;

namespace AutoSalvage
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds multiple copies of <paramref name="item"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The collection to add to.</param>
        /// <param name="count">The number of copies of <paramref name="item"/> to add.</param>
        /// <param name="item">The item to add.</param>
        public static void Add<T>(this ICollection<T> collection, int count, T item)
        {
            while (count-- > 0)
                collection.Add(item);
        }
    }
}
