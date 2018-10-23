using AutoSalvage.Entities;
using System;
using System.Collections.Generic;

namespace AutoSalvage.Interface
{
    public class DroneInterface : EntityInterface
    {
        protected Drone Drone => (Drone)Entity;

        public Queue<string> CommandQueue { get; } = new Queue<string>();

        public event EventHandler Destroyed;

        public int Health => Drone.Health;
        public event EventHandler<ValueChangedEventArgs<int>> HealthChanged;

        public int MaximumHealth => Drone.MaximumHealth;
        public event EventHandler<ValueChangedEventArgs<int>> MaximumHealthChanged;

        internal DroneInterface(Drone drone) : base(drone)
        {
            Drone.Destroyed += (sender, e) => Destroyed?.Invoke(this, e);
            Drone.HealthChanged += (sender, e) => HealthChanged?.Invoke(this, e);
            Drone.MaximumHealthChanged += (sender, e) => MaximumHealthChanged?.Invoke(this, e);
        }
    }
}
