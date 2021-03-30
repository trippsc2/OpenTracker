using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    ///     This interface contains the map location connection data.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// The first map location to connect.
        /// </summary>
        IMapLocation Location1 { get; }
        
        /// <summary>
        /// The second map location to connect.
        /// </summary>
        IMapLocation Location2 { get; }

        /// <summary>
        ///     A factory for creating new map connections.
        /// </summary>
        /// <param name="location1">
        ///     The first location to connect.
        /// </param>
        /// <param name="location2">
        ///     The second location to connect.
        /// </param>
        /// <returns>
        ///     A new map connection.
        /// </returns>
        delegate IConnection Factory(IMapLocation location1, IMapLocation location2);

        /// <summary>
        ///     Creates an undoable action to remove the connection.
        /// </summary>
        /// <returns>
        ///     An undoable action to remove the connection.
        /// </returns>
        IUndoable CreateRemoveConnectionAction();

        /// <summary>
        ///     Returns a new connection save data instance for this connection.
        /// </summary>
        /// <returns>
        ///     A new connection save data instance.
        /// </returns>
        ConnectionSaveData Save();
    }
}