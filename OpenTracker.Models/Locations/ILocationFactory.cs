namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains creation logic for location data.
    /// </summary>
    public interface ILocationFactory
    {
        /// <summary>
        ///     A factory for creating the location factory.
        /// </summary>
        /// <returns>
        ///     The location factory.
        /// </returns>
        delegate ILocationFactory Factory();
        
        /// <summary>
        ///     Returns a new location for the specified ID.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A new location.
        /// </returns>
        ILocation GetLocation(LocationID id);
    }
}