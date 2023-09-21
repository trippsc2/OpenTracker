using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Connections;

namespace OpenTracker.Models.Locations.Map.Connections;

/// <summary>
/// This interface contains the map location connection data.
/// </summary>
public interface IMapConnection
{
    /// <summary>
    /// The <see cref="IMapLocation"/> representing the first map location to connect.
    /// </summary>
    IMapLocation Location1 { get; }
        
    /// <summary>
    /// The <see cref="IMapLocation"/> representing the second map location to connect.
    /// </summary>
    IMapLocation Location2 { get; }

    /// <summary>
    /// A factory for creating new <see cref="IMapConnection"/> objects.
    /// </summary>
    /// <param name="location1">
    ///     A <see cref="IMapLocation"/> representing the first location to connect.
    /// </param>
    /// <param name="location2">
    ///     A <see cref="IMapLocation"/> representing the second location to connect.
    /// </param>
    /// <returns>
    ///     A new <see cref="IMapConnection"/> object.
    /// </returns>
    delegate IMapConnection Factory(IMapLocation location1, IMapLocation location2);

    /// <summary>
    /// Creates a new <see cref="IRemoveMapConnection"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IRemoveMapConnection"/> object.
    /// </returns>
    IUndoable CreateRemoveConnectionAction();

    /// <summary>
    /// Returns a new <see cref="ConnectionSaveData"/> object representing save data for this object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="ConnectionSaveData"/> object.
    /// </returns>
    ConnectionSaveData Save();
}