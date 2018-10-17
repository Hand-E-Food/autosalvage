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
    }
}
