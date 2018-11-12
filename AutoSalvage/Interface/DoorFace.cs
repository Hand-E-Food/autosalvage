using System.Linq;

namespace AutoSalvage.Interface
{
    public class DoorFace
    {
        private readonly World.DoorFace doorFace;

        /// <summary>
        /// The <see cref="Interface.Door"/> this <see cref="DoorFace"/> is part of.
        /// </summary>
        public Door Door { get; }

        /// <summary>
        /// The <see cref="DoorFace"/> on the other side of the <see cref="Interface.Door"/> this <see cref="DoorFace"/> is part of.
        /// </summary>
        public DoorFace OtherFace => Door.Faces.First(face => face != this);

        /// <summary>
        /// The <see cref="Interface.Room"/> this <see cref="DoorFace"/> faces.
        /// </summary>
        public Room Room { get; internal set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="DoorFace"/> class.
        /// </summary>
        /// <param name="doorFace">The <see cref="World.DoorFace"/> to interface.</param>
        /// <param name="door">The <see cref="Interface.Door"/> this <see cref="DoorFace"/> exists on.</param>
        internal DoorFace(World.DoorFace doorFace, Door door)
        {
            this.doorFace = doorFace;
            this.Door = door;
        }
    }
}