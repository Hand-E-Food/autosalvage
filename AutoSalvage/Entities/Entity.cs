﻿using System.Drawing;

namespace AutoSalvage.Entities
{
    /// <summary>
    /// Any object in a world.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// This <see cref="Entity"/>'s footprint.
        /// </summary>
        public Rectangle Bounds { get; internal set; }

        /// <summary>
        /// True if this <see cref="Entity"/> obstructs the movement of another <see cref="Entity"/>.
        /// </summary>
        public abstract bool IsObstruction { get; }

        /// <summary>
        /// This <see cref="Entity"/>'s display name.
        /// </summary>
        public abstract string Name { get; }
    }
}
