using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoSalvage.Interface
{
    public class FloorPlan
    {
        /// <summary>
        /// The <see cref="World.FloorPlan"/> being interfaced.
        /// </summary>
        private readonly World.FloorPlan floorPlan;

        public Queue<string> CommandQueue { get; } = new Queue<string>();

        /// <summary>
        /// This <see cref="FloorPlan"/>'s <see cref="Door"/>s.  The key is the <see cref="Door.Id"/> value.
        /// </summary>
        public IReadOnlyDictionary<string, Door> Doors { get; }
        internal Dictionary<string, Door> doors = new Dictionary<string, Door>();

        public IReadOnlyCollection<Entity> Entities { get; }
        internal List<Entity> entities = new List<Entity>();

        /// <summary>
        /// This <see cref="FloorPlan"/>'s unique identifier.
        /// </summary>
        public string Id => floorPlan.Id;

        /// <summary>
        /// This <see cref="FloorPlan"/>'s <see cref="Room"/>s.  The key is the <see cref="Room.Id"/> value.
        /// </summary>
        public IReadOnlyDictionary<string, Room> Rooms { get; }
        internal Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        /// <summary>
        /// Initialises a new instance of the <see cref="FloorPlan"/> class.
        /// </summary>
        /// <param name="floorPlan">The <see cref="World.FloorPlan"/> to interface.</param>
        internal FloorPlan(World.FloorPlan floorPlan)
        {
            this.floorPlan = floorPlan;

            Doors = new ReadOnlyDictionary<string, Door>(doors);
            Entities = new ReadOnlyCollection<Entity>(entities);
            Rooms = new ReadOnlyDictionary<string, Room>(rooms);
        }
    }
}