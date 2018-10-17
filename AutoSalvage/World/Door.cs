using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AutoSalvage.World
{
    public class Door
    {
        public const int NormalLength = 2;
        public const int NormalThickness = 1;

        public Rectangle Bounds { get; internal set; }

        public IReadOnlyList<DoorFace> Faces { get; }
        internal DoorFace[] faces = new DoorFace[2];

        public FloorPlan FloorPlan { get; internal set; }

        public string Id { get; }

        public Door(string id)
        {
            Id = id;
            Faces = new ReadOnlyCollection<DoorFace>(faces);
        }
    }
}