using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AutoSalvage.World
{
    public class Room
    {
        public Rectangle Bounds { get; internal set; }

        public IReadOnlyDictionary<string, DoorFace> Doors { get; }
        internal Dictionary<string, DoorFace> doors = new Dictionary<string, DoorFace>();

        public FloorPlan FloorPlan { get; internal set; }

        public string Id { get; }

        public Room(string id)
        {
            Id = id;
            Doors = new ReadOnlyDictionary<string, DoorFace>(doors);
        }
    }
}
