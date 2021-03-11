using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.MapLocations
{
    public interface IMapLocationVMFactory
    {
        IMapLocationVM GetMapLocation(IMapLocation mapLocation);
    }
}