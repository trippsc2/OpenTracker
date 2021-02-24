using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This is the interface for connections between two entrance map locations.
    /// </summary>
    public interface IConnection
    {
        IMapLocation Location1 { get; }
        IMapLocation Location2 { get; }

        delegate IConnection Factory(IMapLocation location1, IMapLocation location2);

        bool Equals(object obj);
        int GetHashCode();
        ConnectionSaveData Save();
    }
}