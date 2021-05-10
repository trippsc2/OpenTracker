using OpenTracker.Models.Locations.Map.Connections;

namespace OpenTracker.Models.UndoRedo.Connections
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to remove a new <see cref="IMapConnection"/> from
    /// the map.
    /// </summary>
    public interface IRemoveMapConnection : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IRemoveMapConnection"/> objects.
        /// </summary>
        /// <param name="mapConnection">
        ///     The <see cref="IMapConnection"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IRemoveMapConnection"/> object.
        /// </returns>
        delegate IRemoveMapConnection Factory(IMapConnection mapConnection);
    }
}