namespace AutoSalvage.Generator
{
    /// <summary>
    /// Produces unique identifiers.
    /// </summary>
    /// <typeparam name="T">The type of the identifiers.</typeparam>
    public interface IUidGenerator<T>
    {
        /// <summary>
        /// Returns the next unique identifier.
        /// </summary>
        /// <returns>The next unique identifier.</returns>
        T Next();
    }
}