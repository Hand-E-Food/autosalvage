using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage.World.Generator
{
    internal class RoomGenerator : IRoomGenerator
    {
        private readonly Random random;
        private readonly IUidGenerator<string> uidGenerator;

        public RoomGenerator(Random random, IUidGenerator<string> uidGenerator)
        {
            this.random = random;
            this.uidGenerator = uidGenerator;
        }

        public Room CreateRoom(Rectangle bounds)
        {
            var builder = new RoomBuilder(uidGenerator.Next());
            builder.PositionRoom(bounds);

            var neighbouringRooms = GetNeighbouringRooms(bounds);

            return builder.Room;
        }

        private static IEnumerable<Room> GetNeighbouringRooms(Rectangle bounds)
        {
            const int Margin = Door.NormalThickness + 1;

            bounds.X -= Margin;
            bounds.Y -= Margin;
            bounds.Width += Margin * 2;
            bounds.Height += Margin * 2;


        }
    }
}
