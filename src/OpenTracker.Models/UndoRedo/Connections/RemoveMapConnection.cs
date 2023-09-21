using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Connections;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to remove a new <see cref="IMapConnection"/> from the
/// map.
/// </summary>
[DependencyInjection]
public sealed class RemoveMapConnection : IRemoveMapConnection
{
    private readonly IMapConnectionCollection _mapConnections;
    private readonly IMapConnection _mapConnection;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mapConnections">
    /// The connection collection.
    /// </param>
    /// <param name="mapConnection">
    /// The connection to be removed.
    /// </param>
    public RemoveMapConnection(IMapConnectionCollection mapConnections, IMapConnection mapConnection)
    {
        _mapConnections = mapConnections;
        _mapConnection = mapConnection;
    }

    public bool CanExecute()
    {
        return true;
    }

    public void ExecuteDo()
    {
        _mapConnections.Remove(_mapConnection);
    }

    public void ExecuteUndo()
    {
        _mapConnections.Add(_mapConnection);
    }
}