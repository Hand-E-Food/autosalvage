using System;
using System.Drawing;

namespace AutoSalvage.World.Generator
{
    internal class DoorGenerator : IDoorGenerator
    {
        private readonly Random random;
        private readonly IUidGenerator<string> uidGenerator;

        public DoorGenerator(Random random, IUidGenerator<string> uidGenerator)
        {
            this.random = random;
            this.uidGenerator = uidGenerator;
        }

        /// <summary>
        /// Creates a door joining the two rooms.
        /// </summary>
        /// <param name="room1">The first room.</param>
        /// <param name="room2">The second room.</param>
        /// <returns>The door joining the two rooms.  Null if a door cannot be placed between these rooms.</returns>
        public Door CreateDoor(Room room1, Room room2)
        {
            var bounds = CalculatePotentialDoorBounds(room1, room2);
            if (bounds.Height == Door.NormalThickness && bounds.Width >= Door.NormalLength)
            {
                bounds.X += random.Next(bounds.Width - Door.NormalLength + 1);
                bounds.Width = Door.NormalLength;
            }
            else if (bounds.Width == Door.NormalThickness && bounds.Height >= Door.NormalLength)
            {
                bounds.Y += random.Next(bounds.Height - Door.NormalLength + 1);
                bounds.Height = Door.NormalLength;
            }
            else
            {
                return null;
            }

            var builder = new DoorBuilder(uidGenerator.Next());
            builder.PositionDoor(bounds);
            builder.JoinRooms(room1, room2);
            return builder.Door;
        }

        private Rectangle CalculatePotentialDoorBounds(Room room1, Room room2)
        {
            Rectangle bounds = Rectangle.Empty;
            Room roomA, roomB;

            if (room1.Bounds.Left < room2.Bounds.Left)
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
    }
}
