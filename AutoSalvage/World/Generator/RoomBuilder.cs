namespace AutoSalvage.World.Generator
{
    internal class RoomBuilder
    {
        public Room Room { get; }

        public RoomBuilder(string id)
        {
            Room = new Room(id);
        }

        public RoomBuilder(Room room)
        {
            Room = room;
        }

        public void AddToFloorPlan(FloorPlan floorPlan)
        {
            Room.FloorPlan = floorPlan;
            floorPlan.rooms.Add(Room.Id, Room);
        }
    }
}