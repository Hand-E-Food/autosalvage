namespace AutoSalvage.World.Generator
{
    internal interface IFloorPlanGenerator
    {
        /// <summary>
        /// Creates an entire floor plan.
        /// </summary>
        /// <returns>The floor plan.</returns>
        /// <remarks>Not thread-safe.</remarks>
        FloorPlan CreateFloorPlan();
    }
}