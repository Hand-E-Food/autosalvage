using System.Drawing;

namespace AutoSalvage
{
    /// <summary>
    /// Provides convenience methods for working with <see cref="System.Drawing"/> classes.
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// Converts a <see cref="Rectangle"/> to a <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="rect">The <see cref="Rectangle"/> to convert.</param>
        /// <returns>A <see cref="RectangleF"/> with the same dimensions as the <see cref="Rectangle"/>.</returns>
        public static RectangleF ToRectangleF(this Rectangle rect) =>
            new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

        /// <summary>
        /// Transforms the <see cref="RectangleF"/> with the specified translation and scale.
        /// </summary>
        /// <param name="rect">The <see cref="RectangleF"/> to transform.</param>
        /// <param name="translation">The lateral translation to apply to the <paramref name="rect"/>.</param>
        /// <param name="scale">The scale to magnify the <paramref name="rect"/>.</param>
        /// <returns>The transformed <see cref="RectangleF"/>.</returns>
        public static RectangleF Transform(this RectangleF rect, SizeF translation, float scale)
        {
            return new RectangleF(
                rect.X * scale + translation.Width,
                rect.Y * scale + translation.Height,
                rect.Width * scale,
                rect.Height * scale
            );
        }
    }
}
