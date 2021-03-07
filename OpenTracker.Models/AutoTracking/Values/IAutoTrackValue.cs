using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the interface representing autotracking result value.
    /// </summary>
    public interface IAutoTrackValue : INotifyPropertyChanged
    {
        int? CurrentValue { get; }
    }
}