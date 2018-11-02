using AutoSalvage.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class ObstructionPainter : IPainter
    {
        public IEnumerable<Type> SupportedTypes { get; } = new[] { typeof(Obstruction) };

        public void Paint(FloorPlanViewInfo info, object obj)
        {
            var obstruction = (Obstruction)obj;
            var bounds = obstruction.Bounds.ToRectangleF();
            bounds.Inflate(-0.1f, -0.1f);
            bounds = info.Transform(bounds);
            info.Graphics.FillRectangle(Brushes.SaddleBrown, bounds);
            info.Graphics.DrawRectangle(Pens.SandyBrown, bounds);
            info.Graphics.DrawLine(Pens.SandyBrown, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
            info.Graphics.DrawLine(Pens.SandyBrown, bounds.Right, bounds.Top, bounds.Left, bounds.Bottom);
        }
    }
}
