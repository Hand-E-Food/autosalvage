using AutoSalvage.World;
using System.Collections.Generic;

namespace AutoSalvage.Interface
{
    public class FloorPlanInterface
    {
        public FloorPlan FloorPlan => floorPlan;
        private readonly FloorPlan floorPlan;

        public Queue<string> CommandQueue { get; } = new Queue<string>();

        public FloorPlanInterface(FloorPlan floorPlan)
        {
            this.floorPlan = floorPlan;
        }
    }
}