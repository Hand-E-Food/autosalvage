using AutoSalvage.World;
using System.Collections.Generic;

namespace AutoSalvage.Generator
{
    internal class FloorPlanBuilder
    {
        /// <summary>
        /// All doors in the floor plan.
        /// </summary>
        public ICollection<Door> AllDoors => FloorPlan.doors.Values;

        /// <summary>
        /// All rooms in the floor plan.
        /// </summary>
        public ICollection<Room> AllRooms => FloorPlan.rooms.Values;

        /// <summary>
        /// The floor plan being built.
        /// </summary>
        public FloorPlan FloorPlan { get; }

        /// <summary>
        /// Initialises a enw instance of the <see cref="FloorPlanBuilder"/> class, creating a floor plan with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The new floor plan's id.</param>
        public FloorPlanBuilder(string id)
        {
            FloorPlan = new FloorPlan(id);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FloorPlanBuilder"/> class to build upon the specified <see cref="FloorPlan"/>.
        /// </summary>
        /// <param name="floorPlan">The floor plan to build upon.</param>
        public FloorPlanBuilder(FloorPlan floorPlan)
        {
            FloorPlan = floorPlan;
        }

        /// <summary>
        /// Adds a door joining two rooms.
        /// </summary>
        /// <param name="door">The door to add.</param>
        /// <param name="room1">The first room adjoining the door.</param>
        /// <param name="room2">The second room adjoining the door.</param>
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

        /// <summary>
        /// Adds a room.
        /// </summary>
        /// <param name="room">The room to add.</param>
        public void AddRoom(Room room)
        {
            room.FloorPlan = FloorPlan;
            if (FloorPlan.rooms.Count == 0)
                FloorPlan.FirstRoom = room;
            FloorPlan.rooms.Add(room.Id, room);
        }
    }
}
