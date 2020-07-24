using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Markings;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the interface for location data.
    /// </summary>
    public interface ILocation : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
        int Accessible { get; }
        int Available { get; }
        LocationID ID { get; }
        List<MapLocation> MapLocations { get; }
        string Name { get; }
        List<ISection> Sections { get; }
        int Total { get; }
        ObservableCollection<IMarking> Notes { get; }

        bool CanBeCleared(bool force);
        void Load(LocationSaveData saveData);
        void Reset();
        LocationSaveData Save();
    }
}