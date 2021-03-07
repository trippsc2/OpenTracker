using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the base class representing autotracking result value.
    /// </summary>
    public abstract class AutoTrackValue : IAutoTrackValue
    {
        public event PropertyChangedEventHandler? PropertyChanged;

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

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
