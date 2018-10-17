using System.Linq;

namespace AutoSalvage.World
{
    public class DoorFace
    {
        public Door Door { get; internal set; }

        public DoorFace OtherFace => Door.Faces.First(face => face != this);

        public Room Room { get; internal set; }
    }
}