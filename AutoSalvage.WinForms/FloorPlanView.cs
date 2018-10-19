using AutoSalvage.World;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoSalvage.WinForms
{

    public partial class FloorPlanView : UserControl
    {
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
        public Point ViewCenter
        {
            get => viewCenter;
            set
            {
                if (viewCenter == value)
                    return;

                viewCenter = value;
                Invalidate();
            }
        }
        private Point viewCenter;
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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var translation = new SizeF(Width / 2f, Height / 2f);

            foreach (var room in floorPlan.Rooms.Values)
            {
                var bounds = room.Bounds.ToRectangleF().Transform(translation, zoom);
                e.Graphics.DrawRectangle(Pens.White, bounds);
            }

            foreach (var door in floorPlan.Doors.Values)
            {
                var bounds = door.Bounds.ToRectangleF().Transform(translation, zoom);
                e.Graphics.DrawRectangle(Pens.SkyBlue, bounds);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }
    }
}
