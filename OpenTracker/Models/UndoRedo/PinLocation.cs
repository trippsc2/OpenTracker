using OpenTracker.ViewModels;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for pinning a location.
    /// </summary>
    public class PinLocation : IUndoable
    {
        private readonly ObservableCollection<PinnedLocationControlVM> _pinnedLocations;
        private readonly PinnedLocationControlVM _pinnedLocation;
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
        public PinLocation(ObservableCollection<PinnedLocationControlVM> pinnedLocations,
            PinnedLocationControlVM pinnedLocation)
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
