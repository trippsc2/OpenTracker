using System.ComponentModel;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This is the interface containing tracker GUI settings.
    /// </summary>
    public interface ITrackerSettings : INotifyPropertyChanged
    {
        bool DisplayAllLocations { get; set; }
        bool ShowItemCountsOnMap { get; set; }
    }
}