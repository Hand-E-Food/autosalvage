using System.Drawing;

namespace AutoSalvage.WinForms
{
    internal class TransformedPaintingInfo
    {
        private readonly float scale;
        private readonly SizeF translation;

        /// <summary>
        /// The <see cref="Graphics"/> object to paint to.
        /// </summary>
        public Graphics Graphics { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="TransformedPaintingInfo"/> class.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> object to paint to.</param>
        /// <param name="translation">The lateral translation to apply.</param>
        /// <param name="scale">The scale transform to apply.</param>
        public TransformedPaintingInfo(Graphics graphics, SizeF translation, float scale)
        {
            Graphics = graphics;
            this.translation = translation;
            this.scale = scale;
        }

        /// <summary>
        /// Transforms this <see cref="PointF"/> with the specified translation and scale.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to transform.</param>
        /// <returns>The transformed <see cref="PointF"/>.</returns>
        public PointF Transform(PointF point) => point.Transform(translation, scale);

        /// <summary>
        /// Transforms this array of <see cref="PointF"/> structures with the specified translation and scale.
        /// </summary>
        /// <param name="points">The array of <see cref="PointF"/> structures to transform.</param>
        /// <returns>The transformed array of <see cref="PointF"/> structures.</returns>
        public PointF[] Transform(PointF[] points) => points.Transform(translation, scale);

        /// <summary>
        /// Transforms this <see cref="RectangleF"/> with the specified translation and scale.
        /// </summary>
        /// <param name="rect">The <see cref="RectangleF"/> to transform.</param>
        /// <returns>The transformed <see cref="RectangleF"/>.</returns>
        public RectangleF Transform(RectangleF rect) => rect.Transform(translation, scale);

        /// <summary>
        /// Transforms this <see cref="SizeF"/> with the specified scale.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to transform.</param>
        /// <returns>The transformed <see cref="SizeF"/>.</returns>
        public SizeF Transform(SizeF size) => size.Transform(scale);
    }
}
