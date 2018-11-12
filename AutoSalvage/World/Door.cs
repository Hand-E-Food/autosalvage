using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AutoSalvage.World
{
    /// <summary>
    /// A door between two rooms.
    /// </summary>
    internal class Door
    {
        /// <summary>
        /// This <see cref="Door"/>'s location and size.
        /// </summary>
        public Rectangle Bounds { get; internal set; }

        /// <summary>
        /// This <see cref="Door"/>'s two faces, defining the rooms they join.
        /// </summary>
        public IReadOnlyList<DoorFace> Faces { get; }
        internal DoorFace[] faces = new DoorFace[2];

        /// <summary>
        /// The <see cref="FloorPlan"/> this <see cref="Door"/> exists on.
        /// </summary>
        public FloorPlan FloorPlan { get; internal set; }

        /// <summary>
        /// This <see cref="Door"/>'s unique identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Door"/> class.
        /// </summary>
        /// <param name="id">The <see cref="Door"/>'s unique identifier.</param>
        public Door(string id)
        {
            Id = id;
            Faces = new ReadOnlyCollection<DoorFace>(faces);
        }
    }
}