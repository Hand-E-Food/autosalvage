using System.Drawing;
using System.Linq;

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
        /// Transforms this <see cref="PointF"/> with the specified translation and scale.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to transform.</param>
        /// <param name="translation">The lateral translation to apply to the <paramref name="point"/>.</param>
        /// <param name="scale">The scale to magnify the <paramref name="point"/>.</param>
        /// <returns>The transformed <see cref="PointF"/>.</returns>
        public static PointF Transform(this PointF point, SizeF translation, float scale) =>
            new PointF(point.X * scale + translation.Width, point.Y * scale + translation.Height);

        /// <summary>
        /// Transforms this array of <see cref="PointF"/> structures with the specified translation and scale.
        /// </summary>
        /// <param name="points">The array of <see cref="PointF"/> structures to transform.</param>
        /// <param name="translation">The lateral translation to apply to the <paramref name="points"/>.</param>
        /// <param name="scale">The scale to magnify the <paramref name="points"/>.</param>
        /// <returns>The transformed array of <see cref="PointF"/> structures.</returns>
        public static PointF[] Transform(this PointF[] points, SizeF translation, float scale) =>
            points.Select(point => point.Transform(translation, scale)).ToArray();

        /// <summary>
        /// Transforms this <see cref="RectangleF"/> with the specified translation and scale.
        /// </summary>
        /// <param name="rect">The <see cref="RectangleF"/> to transform.</param>
        /// <param name="translation">The lateral translation to apply to the <paramref name="rect"/>.</param>
        /// <param name="scale">The scale to magnify the <paramref name="rect"/>.</param>
        /// <returns>The transformed <see cref="RectangleF"/>.</returns>
        public static RectangleF Transform(this RectangleF rect, SizeF translation, float scale) =>
            new RectangleF(rect.Location.Transform(translation, scale), rect.Size.Transform(scale));

        /// <summary>
        /// Transforms this <see cref="SizeF"/> with the specified scale.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to transform.</param>
        /// <param name="scale">The scale to magnify the <paramref name="size"/>.</param>
        /// <returns>The transformed <see cref="SizeF"/>.</returns>
        public static SizeF Transform(this SizeF size, float scale) =>
            new SizeF(size.Width * scale, size.Height * scale);
    }
}
