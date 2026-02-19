using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Locations.Map.Connections;

/// <summary>
///     This interface contains the collection container for map connections.
/// </summary>
public interface IMapConnectionCollection : IResettable, ISaveable<IList<ConnectionSaveData>>
{
    /// <summary>
    ///     A factory for creating the connection collection.
    /// </summary>
    /// <returns>
    ///     The connection collection.
    /// </returns>
    delegate IMapConnectionCollection Factory();

    /// <summary>
    /// Gets an <see cref="ObservableCollection{T}"/> of <see cref="IMapConnection"/> objects representing the
    /// connections between map locations.
    /// </summary>
    ObservableCollection<IMapConnection> Connections { get; }

    /// <summary>
    ///     Creates an undoable action to add a connection and sends it to the undo/redo manager.
    /// </summary>
    /// <param name="location1">
    ///     The first map location of the connection.
    /// </param>
    /// <param name="location2">
    ///     The second map location of the connection.
    /// </param>
    IUndoable AddConnection(IMapLocation location1, IMapLocation location2);
}