namespace AutoSalvage.World.Generator
{
    internal class FloorPlanBuilder
    {
        public FloorPlan FloorPlan { get; }

        public FloorPlanBuilder(string id)
        {
            FloorPlan = new FloorPlan(id);
        }

        public FloorPlanBuilder(FloorPlan floorPlan)
        {
            FloorPlan = floorPlan;
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
