using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains location data.
    /// </summary>
    public interface ILocation : IReactiveObject, ISaveable<LocationSaveData>
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