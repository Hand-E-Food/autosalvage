using System.Drawing;

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
        public Rectangle Bounds
        {
            get => bounds;
            internal set => bounds = value;
        }
        private Rectangle bounds = Rectangle.Empty;

        /// <summary>
        /// True if this <see cref="Entity"/> obstructs the movement of another <see cref="Entity"/>.
        /// </summary>
        public abstract bool IsObstruction { get; }

        /// <summary>
        /// This <see cref="Entity"/>'s display name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// This <see cref="Entity"/>'s X position.
        /// </summary>
        internal int X
        {
            get => bounds.X;
            set => bounds.X = value;
        }

        /// <summary>
        /// This <see cref="Entity"/>'s Y position.
        /// </summary>
        internal int Y
        {
            get => bounds.Y;
            set => bounds.Y = value;
        }
    }
}
