using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Connections;

namespace OpenTracker.Models.Locations.Map.Connections;

/// <summary>
/// This class contains the <see cref="ObservableCollection{T}"/> container for <see cref="IMapConnection"/>
/// objects.
/// </summary>
public class MapConnectionCollection : IMapConnectionCollection
{
    private readonly ILocationDictionary _locations;

    private readonly IMapConnection.Factory _connectionFactory;
    private readonly IAddMapConnection.Factory _addConnectionFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="locations">
    ///     The <see cref="ILocationDictionary"/>.
    /// </param>
    /// <param name="connectionFactory">
    ///     An Autofac factory for creating new <see cref="IMapConnection"/> objects.
    /// </param>
    /// <param name="addConnectionFactory">
    ///     An Autofac factory for creating new <see cref="IAddMapConnection"/> objects.
    /// </param>
    public MapConnectionCollection(
        ILocationDictionary locations, IMapConnection.Factory connectionFactory,
        IAddMapConnection.Factory addConnectionFactory)
    {
        _locations = locations;
        _connectionFactory = connectionFactory;
        _addConnectionFactory = addConnectionFactory;
    }

    public ObservableCollection<IMapConnection> Connections { get; } = [];

    public IUndoable AddConnection(IMapLocation location1, IMapLocation location2)
    {
        return _addConnectionFactory(_connectionFactory(location1, location2));
    }

    public IList<ConnectionSaveData> Save()
    {
        return Connections.Select(connection => connection.Save()).ToList();
    }

    public void Load(IList<ConnectionSaveData>? saveData)
    {
        if (saveData == null)
        {
            return;
        }

        Connections.Clear();

        foreach (var connection in saveData)
        {
            Connections.Add(_connectionFactory(
                _locations[connection.Location1].MapLocations[connection.Index1],
                _locations[connection.Location2].MapLocations[connection.Index2]));
        }
    }

    public void Reset()
    {
        Connections.Clear();
    }
}