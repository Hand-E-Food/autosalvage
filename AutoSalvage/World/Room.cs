using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AutoSalvage.World
{
    /// <summary>
    /// A room.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// This <see cref="Room"/>'s location and size.  Location coordinates may be negative.
        /// </summary>
        public Rectangle Bounds { get; internal set; }

        /// <summary>
        /// The <see cref="DoorFace"/>s adjoining this <see cref="Room"/>.
        /// </summary>
        public IReadOnlyDictionary<string, DoorFace> Doors { get; }
        internal Dictionary<string, DoorFace> doors = new Dictionary<string, DoorFace>();

        /// <summary>
        /// The <see cref="FloorPlan"/> this <see cref="Room"/> exists on.
        /// </summary>
        public FloorPlan FloorPlan { get; internal set; }

        /// <summary>
        /// This <see cref="Room"/>'s unique identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="id">The <see cref="Room"/>'s unique identifier.</param>
        public Room(string id)
        {
            Id = id;
            Doors = new ReadOnlyDictionary<string, DoorFace>(doors);
        }
    }
}
