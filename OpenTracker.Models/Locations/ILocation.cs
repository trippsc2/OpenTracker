using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains location data.
    /// </summary>
    public interface ILocation : ISaveable<LocationSaveData>, INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
        int Accessible { get; }
        int Available { get; }
        LocationID ID { get; }
        List<IMapLocation> MapLocations { get; }
        string Name { get; }
        List<ISection> Sections { get; }
        int Total { get; }
        ILocationNoteCollection Notes { get; }
        bool Visible { get; }

        delegate ILocation Factory(LocationID id);

        bool CanBeCleared(bool force);
        void Reset();
    }
}