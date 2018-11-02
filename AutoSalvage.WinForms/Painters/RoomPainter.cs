using AutoSalvage.World;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage.WinForms.Painters
{
    internal class RoomPainter : IPainter
    {
        public IEnumerable<Type> SupportedTypes { get; } = new[] { typeof(Room) };

        public void Paint(FloorPlanViewInfo info, object obj)
        {
            var room = (Room)obj;
            var bounds = info.Transform(room.Bounds.ToRectangleF());
            info.Graphics.FillRectangle(Brushes.DarkGray, bounds);
            info.Graphics.DrawRectangle(Pens.LightGray, bounds);
        }
    }
}
