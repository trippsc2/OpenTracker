using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Reset
{
    /// <summary>
    /// This is the class for resetting the tracker.
    /// </summary>
    public class ResetManager : IResetManager
    {
        private readonly IAutoTracker _autoTracker;
        private readonly IBossPlacementDictionary _bossPlacements;
        private readonly IConnectionCollection _connections;
        private readonly IDropdownDictionary _dropdowns;
        private readonly IItemDictionary _items;
        private readonly ILocationDictionary _locations;
        private readonly IPinnedLocationCollection _pinnedLocations;
        private readonly IPrizePlacementDictionary _prizePlacements;
        private readonly IUndoRedoManager _undoRedoManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public ResetManager(
            IAutoTracker autoTracker, IBossPlacementDictionary bossPlacements,
            IConnectionCollection connections, IDropdownDictionary dropdowns, IItemDictionary items,
            ILocationDictionary locations, IPinnedLocationCollection pinnedLocations,
            IPrizePlacementDictionary prizePlacements, IUndoRedoManager undoRedoManager)
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

        /// <summary>
        /// Executes resetting the tracker.
        /// </summary>
        public void Reset()
        {
            _undoRedoManager.Reset();
            _pinnedLocations.Clear();
            _autoTracker.Stop();
            _bossPlacements.Reset();
            _locations.Reset();
            _prizePlacements.Reset();
            _items.Reset();
            _dropdowns.Reset();
            _connections.Clear();
        }
    }
}
