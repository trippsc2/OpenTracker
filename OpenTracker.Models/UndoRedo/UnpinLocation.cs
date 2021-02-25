using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to unpin a location.
    /// </summary>
    public class UnpinLocation : IUndoable
    {
        private readonly IPinnedLocationCollection _pinnedLocations;

        private readonly ILocation _pinnedLocation;

        private int? _existingIndex;

        public delegate UnpinLocation Factory(ILocation pinnedLocation);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinnedLocations">
        /// The pinned locations collection.
        /// </param>
        /// <param name="pinnedLocation">
        /// The pinned location ViewModel instance to be added.
        /// </param>
        public UnpinLocation(IPinnedLocationCollection pinnedLocations, ILocation pinnedLocation)
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
