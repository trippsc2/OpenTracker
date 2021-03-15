using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the base class representing auto-tracking result value.
    /// </summary>
    public abstract class AutoTrackValue : ReactiveObject, IAutoTrackValue
    {
        private int? _currentValue;
        public int? CurrentValue
        {
            get => _currentValue;
            protected set => this.RaiseAndSetIfChanged(ref _currentValue, value);
        }
    }
}
