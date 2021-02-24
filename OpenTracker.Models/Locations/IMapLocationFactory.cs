using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    public interface IMapLocationFactory
    {
        List<IMapLocation> GetMapLocations(ILocation location);
    }
}