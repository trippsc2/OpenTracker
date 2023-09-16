using OpenTracker.Models.Locations.Map.Connections;

namespace OpenTracker.Models.UndoRedo.Connections;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to add a new <see cref="IMapConnection"/> to the map.
/// </summary>
public class AddMapConnection : IAddMapConnection
{
    private readonly IMapConnectionCollection _mapConnections;
    private readonly IMapConnection _mapConnection;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mapConnections">
    ///     The <see cref="IMapConnectionCollection"/>.
    /// </param>
    /// <param name="mapConnection">
    ///     The <see cref="IMapConnection"/>.
    /// </param>
    public AddMapConnection(IMapConnectionCollection mapConnections, IMapConnection mapConnection)
    {
        _mapConnections = mapConnections;
        _mapConnection = mapConnection;
    }

    public bool CanExecute()
    {
        return !_mapConnections.Contains(_mapConnection);
    }

    public void ExecuteDo()
    {
        _mapConnections.Add(_mapConnection);
    }

    public void ExecuteUndo()
    {
        _mapConnections.Remove(_mapConnection);
    }
}