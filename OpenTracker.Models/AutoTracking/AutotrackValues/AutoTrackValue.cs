using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    public abstract class AutoTrackValue : IAutoTrackValue
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int? _currentValue;
        public int? CurrentValue
        {
            get => _currentValue;
            protected set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    OnPropertyChanged(nameof(CurrentValue));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
