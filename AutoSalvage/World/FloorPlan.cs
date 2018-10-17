using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoSalvage.World
{
    public class FloorPlan
    {
        public IReadOnlyDictionary<string, Door> Doors { get; }
        internal Dictionary<string, Door> doors = new Dictionary<string, Door>();

        public Room FirstRoom { get; internal set; }

        public string Id { get; internal set; }

        public IReadOnlyDictionary<string, Room> Rooms { get; }
        internal Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        public FloorPlan(string id)
        {
            Id = id;

            Doors = new ReadOnlyDictionary<string, Door>(doors);
            Rooms = new ReadOnlyDictionary<string, Room>(rooms);
        }
    }
}
