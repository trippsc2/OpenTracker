using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.PinnedLocations
{
    public interface IPinnedLocationVMFactory
    {
        IPinnedLocationVM GetLocationControlVM(ILocation location);
    }
}