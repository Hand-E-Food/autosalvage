using AutoSalvage.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoSalvage.World
{
    /// <summary>
    /// An entire network of <see cref="Room"/>s and <see cref="Door"/>s.
    /// </summary>
    public class FloorPlan
    {
        /// <summary>
        /// This <see cref="FloorPlan"/>'s <see cref="Door"/>s.  The key is the <see cref="Door.Id"/> value.
        /// </summary>
        public IReadOnlyDictionary<string, Door> Doors { get; }
        internal Dictionary<string, Door> doors = new Dictionary<string, Door>();

        public IReadOnlyCollection<Entity> Entities { get; }
        internal List<Entity> entities = new List<Entity>();

        /// <summary>
        /// The <see cref="Room"/> where the player starts.
        /// </summary>
        public Room FirstRoom { get; internal set; }

        /// <summary>
        /// This <see cref="FloorPlan"/>'s unique identifier.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// This <see cref="FloorPlan"/>'s <see cref="Room"/>s.  The key is the <see cref="Door.Id"/> value.
        /// </summary>
        public IReadOnlyDictionary<string, Room> Rooms { get; }
        internal Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        /// <summary>
        /// Initialises a new instance of the <see cref="FloorPlan"/> class.
        /// </summary>
        /// <param name="id">The <see cref="FloorPlan"/>'s unique identifier.</param>
        public FloorPlan(string id)
        {
            Id = id;

            Doors = new ReadOnlyDictionary<string, Door>(doors);
            Entities = new ReadOnlyCollection<Entity>(entities);
            Rooms = new ReadOnlyDictionary<string, Room>(rooms);
        }
    }
}
