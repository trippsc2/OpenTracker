using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Reset
{
    /// <summary>
    /// This class contains logic for resetting the tracker.
    /// </summary>
    public class ResetManager : IResetManager
    {
        private readonly IAutoTracker _autoTracker;
        private readonly IBossPlacementDictionary _bossPlacements;
        private readonly IMapConnectionCollection _connections;
        private readonly IDropdownDictionary _dropdowns;
        private readonly IItemDictionary _items;
        private readonly ILocationDictionary _locations;
        private readonly IPinnedLocationCollection _pinnedLocations;
        private readonly IPrizePlacementDictionary _prizePlacements;
        private readonly IUndoRedoManager _undoRedoManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTracker">
        ///     The <see cref="IAutoTracker"/>.
        /// </param>
        /// <param name="bossPlacements">
        ///     The <see cref="IBossPlacementDictionary"/>.
        /// </param>
        /// <param name="connections">
        ///     The <see cref="IMapConnectionCollection"/>.
        /// </param>
        /// <param name="dropdowns">
        ///     The <see cref="IDropdownDictionary"/>.
        /// </param>
        /// <param name="items">
        ///     The <see cref="IItemDictionary"/>.
        /// </param>
        /// <param name="locations">
        ///     The <see cref="ILocationDictionary"/>.
        /// </param>
        /// <param name="pinnedLocations">
        ///     The <see cref="IPinnedLocationCollection"/>.
        /// </param>
        /// <param name="prizePlacements">
        ///     The <see cref="IPrizePlacementDictionary"/>.
        /// </param>
        /// <param name="undoRedoManager">
        ///     The <see cref="IUndoRedoManager"/>.
        /// </param>
        public ResetManager(
            IAutoTracker autoTracker, IBossPlacementDictionary bossPlacements, IMapConnectionCollection connections,
            IDropdownDictionary dropdowns, IItemDictionary items, ILocationDictionary locations,
            IPinnedLocationCollection pinnedLocations, IPrizePlacementDictionary prizePlacements,
            IUndoRedoManager undoRedoManager)
        {
            _autoTracker = autoTracker;
            _bossPlacements = bossPlacements;
            _connections = connections;
            _dropdowns = dropdowns;
            _items = items;
            _locations = locations;
            _pinnedLocations = pinnedLocations;
            _prizePlacements = prizePlacements;
            _undoRedoManager = undoRedoManager;
        }

        public void Reset()
        {
            _undoRedoManager.Reset();
            _pinnedLocations.Clear();
            _autoTracker.Disconnect().Wait();
            _bossPlacements.Reset();
            _locations.Reset();
            _prizePlacements.Reset();
            _items.Reset();
            _dropdowns.Reset();
            _connections.Clear();
        }
    }
}
