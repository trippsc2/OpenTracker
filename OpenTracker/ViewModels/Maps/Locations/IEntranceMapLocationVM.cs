using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.Maps.Locations
{
    public interface IEntranceMapLocationVM : IMapLocationVMBase
    {
        IMapLocation MapLocation { get; }

        void ConnectLocation(IMapLocation location);
    }
}