namespace AutoSalvage.WinForms.Painters
{
    /// <summary>
    /// Paints an object.
    /// </summary>
    internal interface IPainter
    {
        /// <summary>
        /// Paints <paramref name="obj"/> using graphics information in <paramref name="info"/>.
        /// </summary>
        /// <param name="info">The graphics information used for painting.</param>
        /// <param name="obj">The objecte to paint.</param>
        void Paint(FloorPlanViewInfo info, object obj);
    }
}
