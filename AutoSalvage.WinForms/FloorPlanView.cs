using AutoSalvage.Entities;
using AutoSalvage.WinForms.Painters;
using AutoSalvage.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoSalvage.WinForms
{

    public partial class FloorPlanView : UserControl
    {
        private readonly Dictionary<Type, IPainter> painters;

        private Point? mouseStart = null;
        private SizeF translation;
        private PointF? viewCenterStart = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FloorPlan FloorPlan
        {
            get => floorPlan;
            set
            {
                if (floorPlan == value)
                    return;

                floorPlan = value;
                ViewCenter = new Point(0, 0);
                Invalidate();
                OnFloorPlanChanged();
            }
        }
        private FloorPlan floorPlan = null;
        public event EventHandler FloorPlanChanged;
        protected void OnFloorPlanChanged() => FloorPlanChanged?.Invoke(this, EventArgs.Empty);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PointF ViewCenter
        {
            get => viewCenter;
            set
            {
                if (viewCenter == value)
                    return;

                viewCenter = value;
                CalculateTranslation();
                Invalidate();
            }
        }
        private PointF viewCenter;
        public event EventHandler ViewCenterChanged;
        protected void OnViewCenterChanged() => ViewCenterChanged?.Invoke(this, EventArgs.Empty);

        [DefaultValue(DefaultZoom)]
        public float Zoom
        {
            get => zoom;
            set
            {
                if (zoom == value)
                    return;

                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Zoom), $"{nameof(Zoom)} must be a positive value.");

                zoom = value;
                Invalidate();
            }
        }
        private float zoom = DefaultZoom;
        private const float DefaultZoom = 20;
        public event EventHandler ZoomChanged;
        protected void OnZoomChanged() => ZoomChanged?.Invoke(this, EventArgs.Empty);

        public FloorPlanView()
        {
            InitializeComponent();

            painters = typeof(IPainter).Assembly
                .GetTypes()
                .Where(type => !type.IsAbstract && typeof(IPainter).IsAssignableFrom(type))
                .Select(type => (IPainter)Activator.CreateInstance(type))
                .SelectMany(painter => painter.SupportedTypes
                    .Select(type => new { type, painter })
                )
                .ToDictionary(
                    pair => pair.type,
                    pair => pair.painter
                );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var info = new FloorPlanViewInfo(e.Graphics, translation, zoom);
            PaintObjects(info, floorPlan.Rooms.Values);
            PaintObjects(info, floorPlan.Doors.Values);
            PaintObjects(info, floorPlan.Entities);
        }

        private void PaintObjects(FloorPlanViewInfo info, IEnumerable objs)
        {
            foreach (object obj in objs)
                if (painters.TryGetValue(obj.GetType(), out var painter))
                    painter.Paint(info, obj);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalculateTranslation();
            Invalidate();
        }

        private void CalculateTranslation()
        {
            translation = new SizeF(Width / 2f + ViewCenter.X, Height / 2f + ViewCenter.Y);
        }

        private void FloorPlanView_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    StartPanning(e.Location);
                    break;

                case MouseButtons.Right:
                    CancelPanning();
                    break;
            }
        }

        private void FloorPlanView_MouseMove(object sender, MouseEventArgs e)
        {
            ContinuePanning(e);
        }

        private void FloorPlanView_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    EndPanning();
                    break;
            }
        }

        private void FloorPlanView_MouseEnter(object sender, EventArgs e)
        {
            if ((MouseButtons & MouseButtons.Left) == 0)
                EndPanning();
        }

        private void StartPanning(Point pixelLocation)
        {
            mouseStart = pixelLocation;
            viewCenterStart = ViewCenter;
        }

        private void ContinuePanning(MouseEventArgs e)
        {
            if (mouseStart.HasValue)
            {
                var deltaX = e.X - mouseStart.Value.X;
                var deltaY = e.Y - mouseStart.Value.Y;
                ViewCenter = new PointF(viewCenterStart.Value.X + deltaX, viewCenterStart.Value.Y + deltaY);
            }
        }

        private void CancelPanning()
        {
            mouseStart = null;
            if (viewCenterStart.HasValue)
            {
                ViewCenter = viewCenterStart.Value;
                viewCenterStart = null;
            }
        }

        private void EndPanning()
        {
            mouseStart = null;
            viewCenterStart = null;
        }
    }
}
