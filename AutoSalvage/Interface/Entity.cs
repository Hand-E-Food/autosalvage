using AutoSalvage.Entities;
using System;
using System.Drawing;

namespace AutoSalvage.Interface
{
    public class Entity
    {
        /// <summary>
        /// The <see cref="Entities.Entity"/> being interfaced.
        /// </summary>
        protected Entities.Entity entity { get; }

        /// <summary>
        /// This <see cref="Interface.Entity"/>'s footprint.
        /// </summary>
        public Rectangle Bounds => entity.Bounds;

        public HealthRank HealthRank
        {
            get
            {
                if (!(entity is IDamagable damagable))
                    return HealthRank.Sturdy;

                if (damagable.Health <= 0)
                    return HealthRank.Destroyed;

                return (HealthRank)Math.Ceiling((double)HealthRank.Sturdy * damagable.Health / damagable.MaximumHealth);
            }
        }

        /// <summary>
        /// This <see cref="Interface.Entity"/>'s display name.
        /// </summary>
        public string Name => entity.Name;

        /// <summary>
        /// Initialises a new instance of the <see cref="Interface.Entity"/> class.
        /// </summary>
        /// <param name="entity">The <see cref="Entities.Entity"/> to interface.</param>
        internal Entity(Entities.Entity entity)
        {
            this.entity = entity;
        }
    }
}
