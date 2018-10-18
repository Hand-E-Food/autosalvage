using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AutoSalvage.World.Generator
{
    internal class GridFloorPlanGenerator : IFloorPlanGenerator
    {
        private const int DoorLength = 2;
        private const int WallThickness = 1;

        private readonly Random random;
        private readonly IUidGenerator<string> uidGenerator;

        private FloorPlanBuilder floorPlanBuilder;

        /// <summary>
        /// Initialises a new instance of the <see cref="GridFloorPlanGenerator"/> class.
        /// </summary>
        /// <param name="random">An initialised random number generator.  If null, a default <see cref="Random"/> is created.</param>
        /// <param name="uidGenerator">The generator for floor plan, room and door ids.</param>
        public GridFloorPlanGenerator(Random random, IUidGenerator<string> uidGenerator)
        {
            this.random = random ?? new Random();
            this.uidGenerator = uidGenerator;
        }

        /// <summary>
        /// Creates an entire floor plan.
        /// </summary>
        /// <returns>The floor plan.</returns>
        /// <remarks>Not thread-safe.</remarks>
        public FloorPlan CreateFloorPlan()
        {
            floorPlanBuilder = new FloorPlanBuilder(uidGenerator.Next());

            CreateFirstRoom();

            for (int i = 0; i < 10; i++)
                CreateRoom();

            return floorPlanBuilder.FloorPlan;
        }

        /// <summary>
        /// Creates the first room of the floor plan.
        /// </summary>
        /// <returns>The room.</returns>
        private Room CreateFirstRoom()
        {
            var room = new Room(uidGenerator.Next());
            room.Bounds = new Rectangle(0, 0, 6, 6);
            floorPlanBuilder.AddRoom(room);
            return room;
        }

        /// <summary>
        /// Creates another room for the floor plan.
        /// </summary>
        /// <returns></returns>
        private Room CreateRoom()
        {
            var room = new Room(uidGenerator.Next());
            room.Bounds = bounds;

            var neighbouringRooms = GetNeighbouringRooms(room);
            foreach (var neighbouringRoom in neighbouringRooms)
            {
                var door = CreateDoor(room, neighbouringRoom);
            }

            floorPlanBuilder.AddRoom(room);
            return room;
        }

        /// <summary>
        /// Gets all rooms that have only a wall between them and the specified room.
        /// </summary>
        /// <param name="room">The room to find the neighbours of.</param>
        /// <returns>The rooms neighbouring the specified <paramref name="room"/>.</returns>
        private IEnumerable<Room> GetNeighbouringRooms(Room room)
        {
            const int Margin = WallThickness + 1;

            var bounds = room.Bounds;
            bounds.X -= Margin;
            bounds.Y -= Margin;
            bounds.Width += Margin * 2;
            bounds.Height += Margin * 2;

            return floorPlanBuilder.AllRooms.Where(r => r != room && r.Bounds.IntersectsWith(bounds));
        }

        /// <summary>
        /// Creates a door joining the two rooms.
        /// </summary>
        /// <param name="room1">The first room.</param>
        /// <param name="room2">The second room.</param>
        /// <returns>The door joining the two rooms.  Null if a door cannot be placed between these rooms.</returns>
        private Door CreateDoor(Room room1, Room room2)
        {
            var bounds = CalculatePotentialDoorBounds(room1, room2);
            if (bounds.Height == WallThickness && bounds.Width >= DoorLength)
            {
                bounds.X += random.Next(bounds.Width - DoorLength + 1);
                bounds.Width = DoorLength;
            }
            else if (bounds.Width == WallThickness && bounds.Height >= DoorLength)
            {
                bounds.Y += random.Next(bounds.Height - DoorLength + 1);
                bounds.Height = DoorLength;
            }
            else
            {
                return null;
            }

            var door = new Door(uidGenerator.Next());
            door.Bounds = bounds;
            floorPlanBuilder.AddDoor(door, room1, room2);
            return door;
        }

        /// <summary>
        /// Returns the area directly between the two rooms.
        /// </summary>
        /// <param name="room1">The first room.</param>
        /// <param name="room2">The second room.</param>
        /// <returns>The area directly between the two rooms.</returns>
        /// <exception cref="ArgumentException">The two rooms overlap or have no orthoginal alignment.</exception>
        private Rectangle CalculatePotentialDoorBounds(Room room1, Room room2)
        {
            if (room1.Bounds.IntersectsWith(room2.Bounds))
                throw new ArgumentException("The two rooms overlap.");

            Rectangle bounds = Rectangle.Empty;
            Room roomA, roomB;

            if (room1.Bounds.Right < room2.Bounds.Left)
            {
                roomA = room1;
                roomB = room2;
            }
            else
            {
                roomA = room2;
                roomB = room1;
            }
            bounds.X = roomA.Bounds.Right + 1;
            bounds.Width = roomB.Bounds.Left - bounds.X;

            if (room1.Bounds.Top < room2.Bounds.Top)
            {
                roomA = room1;
                roomB = room2;
            }
            else
            {
                roomA = room2;
                roomB = room1;
            }
            bounds.Y = roomA.Bounds.Bottom + 1;
            bounds.Height = roomB.Bounds.Top - bounds.Y;

            return bounds;
        }

        /// <summary>
        /// Returns a random item from the collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The items from which to choose a random item.</param>
        /// <returns>The randomly chosen item.</returns>
        private T GetRandom<T>(ICollection<T> items)
        {
            using (var enumerator = items.GetEnumerator())
            {
                var i = random.Next(items.Count);
                while (i-- >= 0)
                    enumerator.MoveNext();
                return enumerator.Current;
            }
        }
    }
}
