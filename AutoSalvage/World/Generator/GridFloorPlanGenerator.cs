using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSalvage.World.Generator
{
    internal class GridFloorPlanGenerator : IFloorPlanGenerator
    {
        private readonly IDoorGenerator doorGenerator;
        private readonly IRoomGenerator roomGenerator;
        private readonly IUidGenerator<string> uidGenerator;

        public GridFloorPlanGenerator(IDoorGenerator doorGenerator, IRoomGenerator roomGenerator, IUidGenerator<string> uidGenerator)
        {
            this.doorGenerator = doorGenerator;
            this.roomGenerator = roomGenerator;
            this.uidGenerator = uidGenerator;
        }

        public FloorPlan Generate()
        {
            var floorPlanBuilder = new FloorPlanBuilder(uidGenerator.Next());
            
            for (int i = 0; i < 10; i++)
            {
                var roomBuilder = roomGenerator.CreateRoom();
            }
        }
    }
}
