using AutoSalvage.Entities;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class DronePainter : IPainter
    {
        public void Paint(FloorPlanViewInfo info, object obj)
        {
            var drone = (Drone)obj;
            var bounds = info.Transform(drone.Bounds.ToRectangleF());

            Brush brush;
            Pen pen;

            if (drone.MaximumHealth <= 0)
            {
                brush = Brushes.DarkRed;
                pen = Pens.Red;
            }
            else if (drone.Health <= 0)
            {
                brush = Brushes.Olive;
                pen = Pens.Yellow;
            }
            else
            {
                brush = Brushes.DarkGreen;
                pen = Pens.LightGreen;
            }

            info.Graphics.FillEllipse(brush, bounds);
            info.Graphics.DrawEllipse(pen, bounds);
        }
    }
}
