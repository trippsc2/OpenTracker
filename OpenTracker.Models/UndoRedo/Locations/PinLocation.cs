using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations
{
    /// <summary>
    /// This class contains undoable action data to pin a location.
    /// </summary>
    public class PinLocation : IUndoable
    {
        private readonly IPinnedLocationCollection _pinnedLocations;

        private readonly ILocation _pinnedLocation;
        private int? _existingIndex;

        public delegate PinLocation Factory(ILocation pinnedLocation);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinnedLocations">
        /// The pinned location collection.
        /// </param>
        /// <param name="pinnedLocation">
        /// The pinned location to be added.
        /// </param>
        public PinLocation(IPinnedLocationCollection pinnedLocations, ILocation pinnedLocation)
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
            if (!_pinnedLocations.Contains(_pinnedLocation))
            {
                return true;
            }

            return _pinnedLocations.IndexOf(_pinnedLocation) != 0;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
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
        public void ExecuteUndo()
        {
            _pinnedLocations.Remove(_pinnedLocation);

            if (_existingIndex.HasValue)
            {
                _pinnedLocations.Insert(_existingIndex.Value, _pinnedLocation);
            }
        }
    }
}
