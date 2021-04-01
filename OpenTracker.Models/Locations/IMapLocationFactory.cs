using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains the creation logic for map location data.
    /// </summary>
    public interface IMapLocationFactory
    {
        IList<IMapLocation> GetMapLocations(ILocation location);
    }
}