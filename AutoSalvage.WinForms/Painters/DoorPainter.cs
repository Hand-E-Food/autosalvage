using AutoSalvage.World;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class DoorPainter : IPainter
    {
        public void Paint(FloorPlanViewInfo info, object obj)
        {
            var door = (Door)obj;
            var bounds = info.Transform(door.Bounds.ToRectangleF());
            info.Graphics.FillRectangle(Brushes.Blue, bounds);
            info.Graphics.DrawRectangle(Pens.SkyBlue, bounds);
        }
    }
}
