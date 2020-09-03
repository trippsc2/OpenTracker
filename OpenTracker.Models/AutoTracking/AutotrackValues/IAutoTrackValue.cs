using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    public interface IAutoTrackValue : INotifyPropertyChanged
    {
        int? CurrentValue { get; }
    }
}