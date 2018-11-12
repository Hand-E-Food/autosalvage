using System;
using System.Collections.Generic;

namespace AutoSalvage.Interface
{
    public class OperatorDrone : Entity
    {
        protected Entities.Drone drone => (Entities.Drone)entity;

        public Queue<string> CommandQueue { get; } = new Queue<string>();

        public event EventHandler Destroyed;

        public int Health => drone.Health;
        public event EventHandler<ValueChangedEventArgs<int>> HealthChanged;

        public int MaximumHealth => drone.MaximumHealth;
        public event EventHandler<ValueChangedEventArgs<int>> MaximumHealthChanged;

        internal OperatorDrone(Entities.Drone drone) : base(drone)
        {
            this.drone.Destroyed += (sender, e) => Destroyed?.Invoke(this, e);
            this.drone.HealthChanged += (sender, e) => HealthChanged?.Invoke(this, e);
            this.drone.MaximumHealthChanged += (sender, e) => MaximumHealthChanged?.Invoke(this, e);
        }
    }
}
