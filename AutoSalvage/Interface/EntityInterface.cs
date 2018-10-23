using AutoSalvage.Entities;
using System;
using System.Drawing;

namespace AutoSalvage.Interface
{
    public class EntityInterface
    {
        protected Entity Entity { get; }

        public Rectangle Bounds => Entity.Bounds;

        public HealthRank HealthRank
        {
            get
            {
                if (!(Entity is IDamagable damagable))
                    return HealthRank.Sturdy;

                if (damagable.Health <= 0)
                    return HealthRank.Destroyed;

                return (HealthRank)Math.Ceiling((double)HealthRank.Sturdy * damagable.Health / damagable.MaximumHealth);
            }
        }

        public string Name => Entity.Name;

        internal EntityInterface(Entity entity)
        {
            Entity = entity;
        }
    }
}
