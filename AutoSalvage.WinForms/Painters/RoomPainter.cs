using AutoSalvage.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class RoomPainter : IPainter
    {
        private static readonly Brush darkBrush = new SolidBrush(Color.FromArgb(0xff, 0x11, 0x11, 0x11));

        public IEnumerable<Type> SupportedTypes { get; } = new[] { typeof(Room) };

        public void Paint(TransformedPaintingInfo info, object obj)
        {
            var room = (Room)obj;
            var bounds = info.Transform(room.Bounds.ToRectangleF());
            info.Graphics.FillRectangle(darkBrush, bounds);
            info.Graphics.DrawRectangle(Pens.LightGray, bounds);
        }
    }
}
