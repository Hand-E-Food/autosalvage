using System;
using System.Drawing;

namespace AutoSalvage.World.Generator
{
    internal class DoorBuilder
    {
        public Door Door { get; }

        public DoorBuilder(string id)
        {
            Door = new Door(id);
        }

        public DoorBuilder (Door door)
        {
            Door = door;
        }

        public void JoinRooms(Room room1, Room room2)
        {
            if (room1.FloorPlan != room2.FloorPlan)
                throw new ArgumentException($"{nameof(room1)} and {nameof(room2)} must have the same {nameof(Room.FloorPlan)}.");

            var face1 = new DoorFace { Door = Door, Room = room1 };
            Door.faces[0] = face1;
            room1.doors.Add(Door.Id, face1);

            var face2 = new DoorFace { Door = Door, Room = room2 };
            Door.faces[1] = face2;
            room2.doors.Add(Door.Id, face2);

            var floorPlan = room1.FloorPlan;
            Door.FloorPlan = floorPlan;
            floorPlan.doors.Add(Door.Id, Door);
        }

        public void PositionDoor(Rectangle bounds)
        {
            Door.Bounds = bounds;
        }
    }
}
