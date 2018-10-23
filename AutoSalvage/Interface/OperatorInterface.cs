using AutoSalvage.Entities;
using AutoSalvage.World;
using System.Collections.Generic;
using System.Linq;

namespace AutoSalvage.Interface
{
    public class OperatorInterface
    {
        public IReadOnlyList<DroneInterface> Drones { get; }
        internal List<DroneInterface> drones;

        public FloorPlanInterface FloorPlan { get; internal set; }

        internal OperatorInterface(FloorPlan floorPlan, IEnumerable<Drone> drones)
        {
            FloorPlan = new FloorPlanInterface(floorPlan);
            this.drones = drones.Select(drone => new DroneInterface(drone)).ToList();
            Drones = this.drones.AsReadOnly();
        }
    }
}
