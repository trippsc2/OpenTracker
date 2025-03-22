using OpenTracker.Models.Locations.Map;

namespace OpenTracker.ViewModels.MapLocations
{
    public interface IMapLocationVMFactory
    {
        IMapLocationVM GetMapLocation(IMapLocation mapLocation);
    }
}