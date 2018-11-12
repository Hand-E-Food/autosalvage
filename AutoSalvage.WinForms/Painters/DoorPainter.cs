using AutoSalvage.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class DoorPainter : IPainter
    {
        public IEnumerable<Type> SupportedTypes { get; } = new[] { typeof(Door) };

        public void Paint(TransformedPaintingInfo info, object obj)
        {
            var door = (Door)obj;
            var bounds = info.Transform(door.Bounds.ToRectangleF());
            info.Graphics.FillRectangle(Brushes.Blue, bounds);
            info.Graphics.DrawRectangle(Pens.SkyBlue, bounds);
        }
    }
}
