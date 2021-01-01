using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for unpinning a location.
    /// </summary>
    public class UnpinLocation : IUndoable
    {
        private readonly ILocation _pinnedLocation;
        private int? _existingIndex;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinnedLocation">
        /// The pinned location ViewModel instance to be added.
        /// </param>
        public UnpinLocation(ILocation pinnedLocation)
        {
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
            _existingIndex = PinnedLocationCollection.Instance.IndexOf(_pinnedLocation);
            PinnedLocationCollection.Instance.Remove(_pinnedLocation);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            if (_existingIndex.HasValue)
            {
                PinnedLocationCollection.Instance.Insert(_existingIndex.Value, _pinnedLocation);
            }
        }
    }
}
