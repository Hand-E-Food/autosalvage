using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AutoSalvage.Interface
{
    public class Door
    {
        /// <summary>
        /// The <see cref="World.Door"/> being interfaced.
        /// </summary>
        private readonly World.Door door;

        /// <summary>
        /// This <see cref="Door"/>'s location and size.
        /// </summary>
        public Rectangle Bounds => door.Bounds;

        /// <summary>
        /// This <see cref="Door"/>'s two faces, defining the rooms they join.
        /// </summary>
        public IReadOnlyList<DoorFace> Faces { get; }
        internal DoorFace[] faces = new DoorFace[2];

        /// <summary>
        /// The <see cref="Interface.FloorPlan"/> this <see cref="Door"/> exists on.
        /// </summary>
        public FloorPlan FloorPlan { get; }

        /// <summary>
        /// This <see cref="Door"/>'s unique identifier.
        /// </summary>
        public string Id => door.Id;

        /// <summary>
        /// Initialises a new instance of the <see cref="Door"/> class.
        /// </summary>
        /// <param name="door">The <see cref="World.Door"/> to interface.</param>
        /// <param name="floorPlan">The <see cref="Interface.FloorPlan"/> this <see cref="Door"/> exists on.</param>
        internal Door(World.Door door, FloorPlan floorPlan)
        {
            this.door = door;
            this.FloorPlan = floorPlan;

            Faces = new ReadOnlyCollection<DoorFace>(faces);
        }
    }
}