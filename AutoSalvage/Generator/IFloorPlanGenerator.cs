using AutoSalvage.World;

namespace AutoSalvage.Generator
{
    /// <summary>
    /// A class that creates <see cref="FloorPlan"/>s.
    /// </summary>
    public interface IFloorPlanGenerator
    {
        /// <summary>
        /// Creates an entire <see cref="FloorPlan"/>.
        /// </summary>
        /// <returns>The <see cref="FloorPlan"/>.</returns>
        /// <remarks>Not thread-safe.</remarks>
        FloorPlan CreateFloorPlan();
    }
}