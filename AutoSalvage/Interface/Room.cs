using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AutoSalvage.Interface
{
    public class Room
    {
        /// <summary>
        /// The <see cref="World.Room"/> being interfaced.
        /// </summary>
        private World.Room room;

        /// <summary>
        /// This <see cref="Room"/>'s location and size.  Location coordinates may be negative.
        /// </summary>
        public Rectangle Bounds => room.Bounds;

        /// <summary>
        /// The <see cref="DoorFace"/>s adjoining this <see cref="Room"/>.
        /// </summary>
        public IReadOnlyDictionary<string, DoorFace> Doors { get; }
        internal Dictionary<string, DoorFace> doors = new Dictionary<string, DoorFace>();

        /// <summary>
        /// The <see cref="FloorPlan"/> this <see cref="Room"/> exists on.
        /// </summary>
        public FloorPlan FloorPlan { get; }

        /// <summary>
        /// This <see cref="Room"/>'s unique identifier.
        /// </summary>
        public string Id => room.Id;

        /// <summary>
        /// Initialises a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="room">The <see cref="World.Room"/> being interfaced.</param>
        /// <param name="floorPlan">The <see cref="FloorPlan"/> this <see cref="Room"/> exists on.</param>
        internal Room(World.Room room, FloorPlan floorPlan)
        {
            this.room = room;
            this.FloorPlan = floorPlan;

            Doors = new ReadOnlyDictionary<string, DoorFace>(doors);

            floorPlan.rooms.Add(Id, this);
        }
    }
}