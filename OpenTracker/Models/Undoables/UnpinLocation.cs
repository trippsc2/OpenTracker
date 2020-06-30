using OpenTracker.ViewModels;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for unpinning a location.
    /// </summary>
    public class UnpinLocation : IUndoable
    {
        private readonly ObservableCollection<LocationControlVM> _pinnedLocations;
        private readonly LocationControlVM _pinnedLocation;
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
        public UnpinLocation(ObservableCollection<LocationControlVM> pinnedLocations,
            LocationControlVM pinnedLocation)
        {
            _pinnedLocations = pinnedLocations;
            _pinnedLocation = pinnedLocation;
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
