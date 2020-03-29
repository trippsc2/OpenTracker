using OpenTracker.Interfaces;
using OpenTracker.ViewModels;
using System.Collections.ObjectModel;

namespace OpenTracker.Actions
{
    public class UnpinLocation : IUndoable
    {
        private readonly ObservableCollection<PinnedLocationControlVM> _pinnedLocations;
        private readonly PinnedLocationControlVM _pinnedLocation;
        private int? _existingIndex;

        public UnpinLocation(ObservableCollection<PinnedLocationControlVM> pinnedLocations,
            PinnedLocationControlVM pinnedLocation)
        {
            _pinnedLocations = pinnedLocations;
            _pinnedLocation = pinnedLocation;
        }

        public void Execute()
        {
            _existingIndex = _pinnedLocations.IndexOf(_pinnedLocation);
            _pinnedLocations.Remove(_pinnedLocation);
        }

        public void Undo()
        {
            if (_existingIndex.HasValue)
                _pinnedLocations.Insert(_existingIndex.Value, _pinnedLocation);
        }
    }
}
