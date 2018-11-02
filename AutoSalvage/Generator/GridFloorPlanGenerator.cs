using AutoSalvage.Entities;
using AutoSalvage.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AutoSalvage.Generator
{
    /// <summary>
    /// Creates a <see cref="FloorPlan"/> in an aligned grid layout.
    /// </summary>
    public class GridFloorPlanGenerator : IFloorPlanGenerator
    {
        private const int DoorLength = 2;
        private const int RoomCount = 10;
        private const int WallThickness = 1;

        private static readonly Size RoomSize = new Size(6, 6);
        private static readonly Size[] RoomDeltas =
        {
            new Size(0, -RoomSize.Height - WallThickness),
            new Size(0,  RoomSize.Height + WallThickness),
            new Size(-RoomSize.Width  - WallThickness, 0),
            new Size( RoomSize.Width  + WallThickness, 0),
        };
        private static readonly Rectangle Size1Rectangle = new Rectangle(0, 0, 1, 1);

        private readonly Random random;
        private readonly List<WeightedItem<Func<Entity>>> randomEntities;
        private readonly IUidGenerator<string> uidGenerator;
        private FloorPlanBuilder floorPlan;

        /// <summary>
        /// Initialises a new instance of the <see cref="GridFloorPlanGenerator"/> class.
        /// </summary>
        /// <param name="random">An initialised random number generator.  If null, a default <see cref="Random"/> is created.</param>
        /// <param name="uidGenerator">The generator for floor plan, room and door ids.</param>
        public GridFloorPlanGenerator(Random random, IUidGenerator<string> uidGenerator)
        {
            this.random = random ?? new Random();
            this.uidGenerator = uidGenerator;

            randomEntities = new List<WeightedItem<Func<Entity>>> {
                new WeightedItem<Func<Entity>>( 6, CreateNothingElse),
                new WeightedItem<Func<Entity>>( 1, CreateObstruction),
                new WeightedItem<Func<Entity>>( 2, CreateScrapPile),
                new WeightedItem<Func<Entity>>( 2, CreateDisabledDrone),
                new WeightedItem<Func<Entity>>( 1, CreateDestroyedDrone),
            };
        }

        /// <summary>
        /// Creates an entire floor plan.
        /// </summary>
        /// <returns>The floor plan.</returns>
        /// <remarks>Not thread-safe.</remarks>
        public FloorPlan CreateFloorPlan()
        {
            floorPlan = new FloorPlanBuilder(uidGenerator.Next());

            CreateFirstRoom();

            while (floorPlan.AllRooms.Count < RoomCount)
                CreateRoom();

            return floorPlan.FloorPlan;
        }

        /// <summary>
        /// Creates the first room of the floor plan.
        /// </summary>
        /// <returns>The room.</returns>
        private Room CreateFirstRoom()
        {
            var room = new Room(uidGenerator.Next());
            room.Bounds = new Rectangle(new Point(RoomSize.Width / 2, RoomSize.Height / 2), RoomSize);
            floorPlan.AddRoom(room);
            return room;
        }

        /// <summary>
        /// Creates another room for the floor plan.
        /// </summary>
        /// <returns></returns>
        private Room CreateRoom()
        {
            var room = new Room(uidGenerator.Next());
            room.Bounds = RandomlySelectRoomBounds();
            floorPlan.AddRoom(room);

            var neighbouringRooms = GetNeighbouringRooms(room);
            foreach (var neighbouringRoom in neighbouringRooms)
            {
                var door = CreateDoor(room, neighbouringRoom);
            }

            FillWithEntities(room);

            return room;
        }

        /// <summary>
        /// Randomly returns the bounds of a room that can exist adjacent to an existing room.
        /// </summary>
        /// <returns>Randomly selected bounds for a new room.</returns>
        private Rectangle RandomlySelectRoomBounds()
        {
            var locations = floorPlan.AllRooms
                .Select(room => room.Bounds.Location)
                .SelectMany(location => RoomDeltas
                    .Select(delta => location + delta)
                )
                .Distinct()
                .Except(floorPlan.AllRooms.Select(room => room.Bounds.Location))
                .ToList();

            return new Rectangle(GetRandom(locations), RoomSize);
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

            return floorPlan.AllRooms.Where(r => r != room && r.Bounds.IntersectsWith(bounds));
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
            floorPlan.AddDoor(door, room1, room2);
            return door;
        }

        /// <summary>
        /// Returns the area directly between the two rooms.
        /// </summary>
        /// <param name="room1">The first room.</param>
        /// <param name="room2">The second room.</param>
        /// <returns>The area directly between the two rooms.  If the rooms are not 
        /// aligned horizontally or vertically, returns a <see cref="Rectangle"/> with a
        /// <see cref="Rectangle.Size"/> of <see cref="Size.Empty"/>.</returns>
        /// <exception cref="ArgumentException">The two rooms overlap.</exception>
        private Rectangle CalculatePotentialDoorBounds(Room room1, Room room2)
        {
            var bounds1 = room1.Bounds;
            var bounds2 = room2.Bounds;

            var bounds = Rectangle.Empty;
            bounds.X      = Math.Max(bounds1.Left  , bounds2.Left  );
            bounds.Y      = Math.Max(bounds1.Top   , bounds2.Top   );
            bounds.Width  = Math.Min(bounds1.Right , bounds2.Right ) - bounds.X;
            bounds.Height = Math.Min(bounds1.Bottom, bounds2.Bottom) - bounds.Y;

            if (bounds.Width > 0 && bounds.Height > 0)
                throw new ArgumentException("The rooms overlap.");

            if (bounds.Width < 0 && bounds.Height < 0)
            {
                bounds.Width = 0;
                bounds.Height = 0;
            }
            else if (bounds.Width < 0)
            {
                bounds.X += bounds.Width;
                bounds.Width = -bounds.Width;
            }
            else if (bounds.Height < 0)
            {
                bounds.Y += bounds.Height;
                bounds.Height = -bounds.Height;
            }

            return bounds;
        }

        /// <summary>
        /// Adds <see cref="Entity"/> objects to the <see cref="Room"/>.
        /// </summary>
        /// <param name="room">The <see cref="Room"/> to populate.</param>
        private void FillWithEntities(Room room)
        {
            var entities = new List<Entity>();
            while (true)
            {
                var entity = GetRandom(randomEntities)();
                if (entity == null)
                    break;

                do
                {
                    var location = room.Bounds.Location + new Size(random.Next(room.Bounds.Width - entity.Bounds.Width + 1), random.Next(room.Bounds.Height - entity.Bounds.Height + 1));
                    entity.Bounds = new Rectangle(location, entity.Bounds.Size);
                }
                while (entities.Any(other => other.Bounds.IntersectsWith(entity.Bounds)));

                entities.Add(entity);
            }
            room.FloorPlan.entities.AddRange(entities);
        }

        private Entity CreateNothingElse() => null;

        private Entity CreateObstruction() => new Obstruction { Bounds = Size1Rectangle };

        private Entity CreateScrapPile() => new ScrapPile { Bounds = Size1Rectangle };

        private Entity CreateDisabledDrone()
        {
            return new Drone
            {
                Bounds = Size1Rectangle,
                Health = 0,
                MaximumHealth = 100,
            };
        }

        private Entity CreateDestroyedDrone()
        {
            return new Drone
            {
                Bounds = Size1Rectangle,
                Health = 0,
                MaximumHealth = 0,
            };
        }

        /// <summary>
        /// Removes and returns a random item from the collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="items">The items from which to choose a random item.</param>
        /// <returns>The randomly chosen item.</returns>
        private T PopRandom<T>(List<T> items)
        {
            var result = GetRandom(items);
            items.Remove(result);
            return result;
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

        private T GetRandom<T>(ICollection<WeightedItem<T>> items)
        {
            var totalWeight = items.Sum(item => item.Weight);
            var n = random.Next(totalWeight);
            foreach (var item in items)
            {
                n -= item.Weight;
                if (n < 0)
                    return item.Value;
            }
            throw new Exception("Basic math has failed.");
        }

        private class WeightedItem<T>
        {
            public int Weight { get; }
            public T Value { get; }

            public WeightedItem(int weight, T value)
            {
                Weight = weight;
                Value = value;
            }
        }
    }
}
