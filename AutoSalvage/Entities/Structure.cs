using System;

namespace AutoSalvage.Entities
{
    /// <summary>
    /// An immovable structure.
    /// </summary>
    public abstract class Structure : Entity, IDamagable
    {
        public event EventHandler Destroyed;
        protected void OnDestroyed() => Destroyed?.Invoke(this, EventArgs.Empty);

        public int Health
        {
            get => health;
            set
            {
                if (health == value)
                    return;

                if (value > MaximumHealth)
                    throw new ArgumentOutOfRangeException(nameof(Health), $"{nameof(Health)} cannot exceed {nameof(MaximumHealth)}.");

                var oldHealth = health;
                health = value;
                OnHealthChanged(new ValueChangedEventArgs<int>(oldHealth));
                if (oldHealth > 0 && health <= 0)
                    OnDestroyed();
            }
        }
        private int health;
        public event EventHandler<ValueChangedEventArgs<int>> HealthChanged;
        protected void OnHealthChanged(ValueChangedEventArgs<int> e) => HealthChanged?.Invoke(this, e);

        public override bool IsObstruction => true;

        public abstract int MaximumHealth { get; }

        public Structure()
        {
            Health = MaximumHealth;
        }
    }
}
