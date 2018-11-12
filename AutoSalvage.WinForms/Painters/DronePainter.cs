using AutoSalvage.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class DronePainter : IPainter
    {
        private const float TextSizeRatio = 0.70f;
        private const float PixelToPointRatio = 0.75f;

        private static readonly StringFormat stringFormat = new StringFormat {
            Alignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.NoWrap,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.Character,
        };

        public IEnumerable<Type> SupportedTypes { get; } = new[] { typeof(Drone) };

        public void Paint(TransformedPaintingInfo info, object obj)
        {
            var drone = (Drone)obj;
            var bounds = info.Transform(drone.Bounds.ToRectangleF());

            Brush brush;
            Pen pen;
            Brush textBrush = null;

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
                textBrush = Brushes.LightGreen;
            }

            info.Graphics.FillEllipse(brush, bounds);
            info.Graphics.DrawEllipse(pen, bounds);
            if (textBrush != null)
            {
                using (var font = new Font(FontFamily.GenericSansSerif, PixelToPointRatio * TextSizeRatio * Math.Min(bounds.Width, bounds.Height)))
                {
                    info.Graphics.DrawString(drone.Name.Remove(1), font, textBrush, bounds, stringFormat);
                }
            }
        }
    }
}
