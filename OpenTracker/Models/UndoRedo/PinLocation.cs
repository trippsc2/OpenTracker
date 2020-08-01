using OpenTracker.ViewModels.PinnedLocations;
using System;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for pinning a location.
    /// </summary>
    public class PinLocation : IUndoable
    {
        private readonly PinnedLocationVM _pinnedLocation;
        private int? _existingIndex;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinnedLocation">
        /// The pinned location view-model to be added.
        /// </param>
        public PinLocation(PinnedLocationVM pinnedLocation)
        {
            _pinnedLocation = pinnedLocation ??
                throw new ArgumentNullException(nameof(pinnedLocation));
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            if (!PinnedLocationCollection.Instance.Contains(_pinnedLocation))
            {
                return true;
            }

            if (PinnedLocationCollection.Instance.IndexOf(_pinnedLocation) == 0)
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
            if (PinnedLocationCollection.Instance.Contains(_pinnedLocation))
            {
                _existingIndex = PinnedLocationCollection.Instance.IndexOf(_pinnedLocation);
                PinnedLocationCollection.Instance.Remove(_pinnedLocation);
            }
            
            PinnedLocationCollection.Instance.Insert(0, _pinnedLocation);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            PinnedLocationCollection.Instance.Remove(_pinnedLocation);

            if (_existingIndex.HasValue)
            {
                PinnedLocationCollection.Instance.Insert(_existingIndex.Value, _pinnedLocation);
            }
        }
    }
}
