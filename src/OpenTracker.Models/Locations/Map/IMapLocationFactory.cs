using System.Collections.Generic;

namespace OpenTracker.Models.Locations.Map
{
    /// <summary>
    /// This interface contains the creation logic for <see cref="IMapLocation"/> objects.
    /// </summary>
    public interface IMapLocationFactory
    {
        /// <summary>
        /// Returns the <see cref="IList{T}"/> of <see cref="IMapLocation"/> objects for the specified location.
        /// </summary>
        /// <param name="location">
        ///     The <see cref="ILocation"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="IList{T}"/> of <see cref="IMapLocation"/> objects.
        /// </returns>
        IList<IMapLocation> GetMapLocations(ILocation location);
    }
}