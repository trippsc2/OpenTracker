using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Locations
{
    public interface ILocation : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; set; }
        int Accessible { get; }
        int Available { get; }
        LocationID ID { get; }
        List<MapLocation> MapLocations { get; }
        string Name { get; }
        List<ISection> Sections { get; }
        int Total { get; }

        void Reset();
    }
}