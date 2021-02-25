using System.ComponentModel;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This interface contains GUI tracking settings data.
    /// </summary>
    public interface ITrackerSettings : INotifyPropertyChanged
    {
        bool DisplayAllLocations { get; set; }
        bool ShowItemCountsOnMap { get; set; }
    }
}