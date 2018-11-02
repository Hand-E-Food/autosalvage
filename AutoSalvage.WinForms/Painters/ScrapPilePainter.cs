using AutoSalvage.Entities;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class ScrapPilePainter : IPainter
    {
        public void Paint(FloorPlanViewInfo info, object obj)
        {
            var scrap = (ScrapPile)obj;
            var bounds = scrap.Bounds.ToRectangleF();
            bounds.Inflate(-0.25f, -0.25f);
            bounds = info.Transform(bounds);
            info.Graphics.FillEllipse(Brushes.Gold, bounds);
            info.Graphics.DrawEllipse(Pens.Goldenrod, bounds);
        }
    }
}
