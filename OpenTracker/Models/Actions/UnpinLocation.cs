using OpenTracker.Models.Interfaces;
using OpenTracker.ViewModels;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.Actions
{
    public class UnpinLocation : IUndoable
    {
        private readonly ObservableCollection<LocationControlVM> _pinnedLocations;
        private readonly LocationControlVM _pinnedLocation;
        private int? _existingIndex;

        public UnpinLocation(ObservableCollection<LocationControlVM> pinnedLocations,
            LocationControlVM pinnedLocation)
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
