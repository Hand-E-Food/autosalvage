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

        private SizeF translation;

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

            painters = new Dictionary<Type, IPainter> {
                { typeof(Door), new DoorPainter() },
                { typeof(Drone), new DronePainter() },
                { typeof(Obstruction), new ObstructionPainter() },
                { typeof(Room), new RoomPainter() },
                { typeof(ScrapPile), new ScrapPilePainter() },
            };
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
            translation = new SizeF(Width / 2f, Height / 2f);
            Invalidate();
        }
    }
}
