namespace AutoSalvage.Entities
{
    /// <summary>
    /// An <see cref="Entity"/> that can be salvaged for scrap.
    /// </summary>
    public interface ISalvagable
    {
        /// <summary>
        /// The amount of scrap that can be salvaged from this <see cref="Entity"/>.
        /// </summary>
        int Scrap { get; }
    }
}
