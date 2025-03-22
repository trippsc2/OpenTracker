namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains creation logic for <see cref="ILocation"/> objects.
    /// </summary>
    public interface ILocationFactory
    {
        /// <summary>
        /// A factory for creating the <see cref="ILocationFactory"/> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="ILocationFactory"/> object.
        /// </returns>
        delegate ILocationFactory Factory();
        
        /// <summary>
        /// Returns a new <see cref="ILocation"/> object for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="ILocation"/> object.
        /// </returns>
        ILocation GetLocation(LocationID id);
    }
}