using System.Collections.Generic;

namespace AutoSalvage.World.Generator
{
    internal class FloorPlanBuilder
    {
        public ICollection<Door> AllDoors => FloorPlan.doors.Values;

        public ICollection<Room> AllRooms => FloorPlan.rooms.Values;

        public FloorPlan FloorPlan { get; }

        public FloorPlanBuilder(string id)
        {
            FloorPlan = new FloorPlan(id);
        }

        public FloorPlanBuilder(FloorPlan floorPlan)
        {
            FloorPlan = floorPlan;
        }

        public void AddDoor(Door door, Room room1, Room room2)
        {
            var face1 = new DoorFace { Door = door, Room = room1 };
            door.faces[0] = face1;
            room1.doors.Add(door.Id, face1);

            var face2 = new DoorFace { Door = door, Room = room2 };
            door.faces[1] = face2;
            room2.doors.Add(door.Id, face2);

            var floorPlan = room1.FloorPlan;
            door.FloorPlan = floorPlan;
            floorPlan.doors.Add(door.Id, door);
        }

        public void AddRoom(Room room)
        {
            room.FloorPlan = FloorPlan;
            if (FloorPlan.rooms.Count == 0)
                FloorPlan.FirstRoom = room;
            FloorPlan.rooms.Add(room.Id, room);
        }
    }
}
