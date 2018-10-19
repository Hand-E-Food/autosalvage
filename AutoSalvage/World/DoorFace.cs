using System.Linq;

namespace AutoSalvage.World
{
    /// <summary>
    /// One side of a <see cref="Door"/>, facing a <see cref="Room"/>.
    /// </summary>
    public class DoorFace
    {
        /// <summary>
        /// The <see cref="Door"/> this <see cref="DoorFace"/> is part of.
        /// </summary>
        public Door Door { get; internal set; }

        /// <summary>
        /// The <see cref="DoorFace"/> on the other side of the <see cref="Door"/> this <see cref="DoorFace"/> is part of.
        /// </summary>
        public DoorFace OtherFace => Door.Faces.First(face => face != this);

        /// <summary>
        /// The <see cref="Room"/> this <see cref="DoorFace"/> faces.
        /// </summary>
        public Room Room { get; internal set; }
    }
}