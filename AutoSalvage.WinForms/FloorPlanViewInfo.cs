using System.Drawing;

namespace AutoSalvage.WinForms
{
    internal class FloorPlanViewInfo
    {
        private readonly float scale;
        private readonly SizeF translation;

        public Graphics Graphics { get; }

        public FloorPlanViewInfo(Graphics graphics, SizeF translation, float scale)
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
