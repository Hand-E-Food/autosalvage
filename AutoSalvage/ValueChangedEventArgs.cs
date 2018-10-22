using System;

namespace AutoSalvage
{
    /// <summary>
    /// Contains information on the previous state of a changed value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class ValueChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// The property's previous value.
        /// </summary>
        public T OldValue { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ValueChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="oldValue">The property's previous value.</param>
        public ValueChangedEventArgs(T oldValue)
        {
            OldValue = oldValue;
        }
    }
}
