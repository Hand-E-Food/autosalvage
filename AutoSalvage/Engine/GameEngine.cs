﻿using AutoSalvage.Entities;
using AutoSalvage.Generator;
using AutoSalvage.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoSalvage.Engine
{
    public class GameEngine
    {
        private static Rectangle Size1Rectangle = new Rectangle(0, 0, 1, 1);

        private readonly Random random;

        protected List<Entities.Drone> Drones { get; } = new List<Entities.Drone>();

        public GameEngine(Random random)
        {
            this.random = random;

            Drones.Add(new Entities.Drone {
                MaximumHealth = 100,
                Health = 100,
                Bounds = Size1Rectangle,
            });
        }

        public OperatorInterface CreateNextFloorPlan()
        {
            var generator = CreateFloorPlanGenerator();
            var floorPlan = generator.CreateFloorPlan();
            var builder = new FloorPlanBuilder(floorPlan);
            var roomBounds = floorPlan.FirstRoom.Bounds;

            foreach (var drone in Drones)
            {
                var droneSize = drone.Bounds.Size;
                Rectangle droneBounds;
                do
                {
                    droneBounds = random.Next(roomBounds, droneSize);
                }
                while (!builder.IsEmptySpace(droneBounds));
                drone.Bounds = droneBounds;
                floorPlan.entities.Add(drone);
            }

            return new OperatorInterface(floorPlan, Drones);
        }

        private IFloorPlanGenerator CreateFloorPlanGenerator()
        {
            var random = new Random();
            return new GridFloorPlanGenerator(random, new Hex4UidGenerator(random));
        }
    }
}
