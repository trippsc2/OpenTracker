using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.Maps.Locations
{
    public interface IMapLocationVMFactory
    {
        IMapLocationVMBase GetMapLocationControlVM(IMapLocation mapLocation);
    }
}