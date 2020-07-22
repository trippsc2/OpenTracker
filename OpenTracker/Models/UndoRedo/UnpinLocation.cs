using OpenTracker.ViewModels.UIPanels.LocationsPanel.PinnedLocations;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for unpinning a location.
    /// </summary>
    public class UnpinLocation : IUndoable
    {
        private readonly ObservableCollection<PinnedLocationVM> _pinnedLocations;
        private readonly PinnedLocationVM _pinnedLocation;
        private int? _existingIndex;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinnedLocations">
        /// The observable collection of pinned locations.
        /// </param>
        /// <param name="pinnedLocation">
        /// The pinned location view-model to be added.
        /// </param>
        public UnpinLocation(ObservableCollection<PinnedLocationVM> pinnedLocations,
            PinnedLocationVM pinnedLocation)
        {
            _pinnedLocations = pinnedLocations;
            _pinnedLocation = pinnedLocation;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _existingIndex = _pinnedLocations.IndexOf(_pinnedLocation);
            _pinnedLocations.Remove(_pinnedLocation);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            if (_existingIndex.HasValue)
            {
                _pinnedLocations.Insert(_existingIndex.Value, _pinnedLocation);
            }
        }
    }
}
