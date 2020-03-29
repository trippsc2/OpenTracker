using OpenTracker.Interfaces;
using OpenTracker.ViewModels;
using System.Collections.ObjectModel;

namespace OpenTracker.Actions
{
    public class PinLocation : IUndoable
    {
        private readonly ObservableCollection<PinnedLocationControlVM> _pinnedLocations;
        private readonly PinnedLocationControlVM _pinnedLocation;
        private int? _existingIndex;

        public PinLocation(ObservableCollection<PinnedLocationControlVM> pinnedLocations,
            PinnedLocationControlVM pinnedLocation)
        {
            _pinnedLocations = pinnedLocations;
            _pinnedLocation = pinnedLocation;
        }

        public void Execute()
        {
            if (_pinnedLocations.Contains(_pinnedLocation))
            {
                _existingIndex = _pinnedLocations.IndexOf(_pinnedLocation);
                _pinnedLocations.Remove(_pinnedLocation);
            }

            _pinnedLocations.Insert(0, _pinnedLocation);
        }

        public void Undo()
        {
            _pinnedLocations.Remove(_pinnedLocation);

            if (_existingIndex.HasValue)
                _pinnedLocations.Insert(_existingIndex.Value, _pinnedLocation);
        }
    }
}
