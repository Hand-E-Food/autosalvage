using System;

namespace AutoSalvage.Objects
{
    public class Drone : Entity, IDamagable, ISalvagable
    {
        public event EventHandler Destroyed;
        protected void OnDestroyed() => Destroyed?.Invoke(this, EventArgs.Empty);

        public int Health
        {
            get => health;
            internal set
            {
                if (health == value)
                    return;

                if (value > MaximumHealth)
                    throw new ArgumentOutOfRangeException(nameof(Health), $"{nameof(Health)} cannot exceed {nameof(MaximumHealth)}.");

                var oldValue = health;
                health = value;
                OnHealthChanged(new ValueChangedEventArgs<int>(oldValue));
                if (oldValue > 0 && health <= 0)
                    OnDestroyed();
            }
        }
        private int health;
        public event EventHandler<ValueChangedEventArgs<int>> HealthChanged;
        protected void OnHealthChanged(ValueChangedEventArgs<int> e) => HealthChanged?.Invoke(this, e);

        public int MaximumHealth
        {
            get => maximumHealth;
            internal set
            {
                if (maximumHealth == value)
                    return;

                var oldValue = maximumHealth;
                maximumHealth = value;
                if (health > maximumHealth)
                    Health = maximumHealth;
                OnMaximumHealthChanged(new ValueChangedEventArgs<int>(oldValue));
            }
        }
        private int maximumHealth;
        public event EventHandler<ValueChangedEventArgs<int>> MaximumHealthChanged;
        protected void OnMaximumHealthChanged(ValueChangedEventArgs<int> e) => MaximumHealthChanged?.Invoke(this, e);

        public override bool IsObstruction => true;

        public override string Name => name;
        internal string name = "Drone";

        public int Scrap { get; internal set; } = 7;
    }
}
