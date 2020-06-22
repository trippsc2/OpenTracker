using OpenTracker.Models;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface to connect two map locations.
    /// </summary>
    public interface IConnectLocation
    {
        void ConnectLocation(IConnectLocation location);
    }
}
