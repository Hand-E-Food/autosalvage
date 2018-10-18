using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoSalvage.World
{
    public class FloorPlan
    {
        public IReadOnlyDictionary<string, Door> Doors { get; }
        internal Dictionary<string, Door> doors = new Dictionary<string, Door>();

        /// <summary>
        /// The room where the player starts.
        /// </summary>
        public Room FirstRoom { get; internal set; }

        /// <summary>
        /// The floor plan's unique identifier.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// The floor plan's rooms, with their <see cref="Room.Id"/> as the key.
        /// </summary>
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
