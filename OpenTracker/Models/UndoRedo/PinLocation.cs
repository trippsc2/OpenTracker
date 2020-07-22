using OpenTracker.ViewModels.UIPanels.LocationsPanel.PinnedLocations;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for pinning a location.
    /// </summary>
    public class PinLocation : IUndoable
    {
        private readonly PinnedLocationVM _pinnedLocation;
        private readonly ObservableCollection<PinnedLocationVM> _pinnedLocations;
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
        public PinLocation(
            PinnedLocationVM pinnedLocation,
            ObservableCollection<PinnedLocationVM> pinnedLocations)
        {
            _pinnedLocation = pinnedLocation ?? throw new ArgumentNullException(nameof(pinnedLocation));
            _pinnedLocations = pinnedLocations ?? throw new ArgumentNullException(nameof(pinnedLocations));
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            if (!_pinnedLocations.Contains(_pinnedLocation))
            {
                return true;
            }

            if (_pinnedLocations.IndexOf(_pinnedLocation) == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            if (_pinnedLocations.Contains(_pinnedLocation))
            {
                _existingIndex = _pinnedLocations.IndexOf(_pinnedLocation);
                _pinnedLocations.Remove(_pinnedLocation);
            }
            
            _pinnedLocations.Insert(0, _pinnedLocation);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _pinnedLocations.Remove(_pinnedLocation);

            if (_existingIndex.HasValue)
            {
                _pinnedLocations.Insert(_existingIndex.Value, _pinnedLocation);
            }
        }
    }
}
