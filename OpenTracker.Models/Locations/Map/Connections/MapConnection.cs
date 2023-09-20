using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Connections;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Locations.Map.Connections;

/// <summary>
/// This class contains the map location connection data.
/// </summary>
[DependencyInjection]
public sealed class MapConnection : IMapConnection
{
    private readonly IRemoveMapConnection.Factory _removeConnectionFactory;
        
    public IMapLocation Location1 { get; }
    public IMapLocation Location2 { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="removeConnectionFactory">
    ///     An Autofac factory for creating new <see cref="IRemoveMapConnection"/> objects.
    /// </param>
    /// <param name="location1">
    ///     A <see cref="IMapLocation"/> representing the first location to connect.
    /// </param>
    /// <param name="location2">
    ///     A <see cref="IMapLocation"/> representing the second location to connect.
    /// </param>
    public MapConnection(
        IRemoveMapConnection.Factory removeConnectionFactory, IMapLocation location1, IMapLocation location2)
    {
        Location1 = location1;
        Location2 = location2;
        _removeConnectionFactory = removeConnectionFactory;
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            IMapConnection connection => (Location1 == connection.Location1 || Location1 == connection.Location2) &&
                                         (Location2 == connection.Location2 || Location2 == connection.Location1),
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return Location1.GetHashCode() ^ Location2.GetHashCode();
    }

    public IUndoable CreateRemoveConnectionAction()
    {
        return _removeConnectionFactory(this);
    }

    public ConnectionSaveData Save()
    {
        return new()
        {
            Location1 = Location1.Location.ID,
            Location2 = Location2.Location.ID,
            Index1 = Location1.Location.MapLocations.IndexOf(Location1),
            Index2 = Location2.Location.MapLocations.IndexOf(Location2)
        };
    }
}