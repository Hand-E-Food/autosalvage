using System.Drawing;

namespace AutoSalvage.WinForms
{
    /// <summary>
    /// Provides convenience methods for working with <see cref="System.Drawing"/> classes.
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// Draws a rectangle specified by a <see cref="RectangleF"/> structure.
        /// </summary>
        /// <param name="graphics">The graphics object drawing the rectangle.</param>
        /// <param name="pen">A <see cref="Pen"/> that determines the color, width, and style of the rectangle.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle to draw.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="pen"/> is null.</exception>
        public static void DrawRectangle(this Graphics graphics, Pen pen, RectangleF rect)
        {
            graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
