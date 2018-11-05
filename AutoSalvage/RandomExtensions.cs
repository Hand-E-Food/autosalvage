using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage
{
    public static class RandomExtensions
    {

        /// <summary>
        /// Returns a random item from the collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="random">The random number generator.</param>
        /// <param name="items">The items from which to choose a random item.</param>
        /// <returns>The randomly chosen item.</returns>
        public static T Get<T>(this Random random, ICollection<T> items)
        {
            using (var enumerator = items.GetEnumerator())
            {
                var i = random.Next(items.Count);
                while (i-- >= 0)
                    enumerator.MoveNext();
                return enumerator.Current;
            }
        }

        /// <summary>
        /// Returns a rectangle of size <paramref name="size"/> located randomly within the specified <paramref name="bounds"/>.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        /// <param name="bounds">The bounds within which to randomly locate the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        /// <returns>A rectangle of size <paramref name="size"/> located randomly within the specified <paramref name="bounds"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="bounds"/> or <paramref name="size"/> has negative dimensions; or</para>
        /// <para><paramref name="size"/> does not fit inside <paramref name="bounds"/>.</para>
        /// </exception>
        public static Rectangle Next(this Random random, Rectangle bounds, Size size)
        {
            if (bounds.Width < 0 || bounds.Height < 0)
                throw new ArgumentException($"{nameof(bounds)} has negative dimensions.", nameof(bounds));
            if (size.Width < 0 || size.Height < 0)
                throw new ArgumentException($"{nameof(size)} has negative dimensions.", nameof(size));
            if (size.Width > bounds.Width || size.Height > bounds.Height)
                throw new ArgumentException($"{nameof(size)} does not fit inside {nameof(bounds)}.", nameof(size));

            return new Rectangle(
                bounds.X + random.Next(bounds.Width - size.Width + 1),
                bounds.Y + random.Next(bounds.Height - size.Height + 1),
                size.Width,
                size.Height
            );
        }
    }
}
