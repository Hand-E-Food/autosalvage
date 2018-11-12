using System.Collections.Generic;
using System.Linq;

namespace AutoSalvage.Interface
{
    /// <summary>
    /// An interface for operating drones and exploring floor plans.
    /// </summary>
    public class OperatorInterface
    {
        /// <summary>
        /// The <see cref="Drone"/>s under the operator's control.
        /// </summary>
        public IReadOnlyList<Drone> Drones { get; }
        internal List<Drone> drones;

        /// <summary>
        /// The <see cref="FloorPlan"/> being explored.
        /// </summary>
        public FloorPlan FloorPlan { get; internal set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="OperatorInterface"/> class.
        /// </summary>
        /// <param name="floorPlan">The <see cref="World.FloorPlan"/> to explore.</param>
        /// <param name="drones">The <see cref="Drone"/>s under the operator's control.</param>
        internal OperatorInterface(World.FloorPlan floorPlan, IEnumerable<Entities.Drone> drones)
        {
            FloorPlan = new FloorPlan(floorPlan);
            this.drones = drones.Select(drone => new Drone(drone)).ToList();
            Drones = this.drones.AsReadOnly();
        }
    }
}
