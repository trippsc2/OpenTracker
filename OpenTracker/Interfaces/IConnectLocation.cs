using OpenTracker.Models.Locations;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface to connect two map locations.
    /// </summary>
    public interface IConnectLocation
    {
        IMapLocation MapLocation { get; }

        void ConnectLocation(IConnectLocation location);
    }
}
