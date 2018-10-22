using System;

namespace AutoSalvage.Objects
{
    /// <summary>
    /// An <see cref="Entity"/> that can be damaged.
    /// </summary>
    public interface IDamagable
    {
        /// <summary>
        /// Triggered when this <see cref="Entity"/> is destroyed.
        /// </summary>
        event EventHandler Destroyed;

        /// <summary>
        /// The <see cref="Entity"/>'s <see cref="Health"/>.
        /// </summary>
        int Health { get; }
        /// <summary>
        /// Triggered when this <see cref="Entity"/>'s <see cref="Health"/> has changed.
        /// </summary>
        event EventHandler<ValueChangedEventArgs<int>> HealthChanged;

        /// <summary>
        /// This <see cref="Entity"/>'s maximum <see cref="Health"/>.
        /// </summary>
        int MaximumHealth { get; }
    }
}
