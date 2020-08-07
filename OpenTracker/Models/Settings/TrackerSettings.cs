using System.ComponentModel;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This is the class containing tracker GUI settings.
    /// </summary>
    public class TrackerSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _displayAllLocations = false;
        public bool DisplayAllLocations
        {
            get => _displayAllLocations;
            set
            {
                if (_displayAllLocations != value)
                {
                    _displayAllLocations = value;
                    OnPropertyChanged(nameof(DisplayAllLocations));
                }
            }
        }

        private bool _showItemCountsOnMap = true;
        public bool ShowItemCountsOnMap
        {
            get => _showItemCountsOnMap;
            set
            {
                if (_showItemCountsOnMap != value)
                {
                    _showItemCountsOnMap = value;
                    OnPropertyChanged(nameof(ShowItemCountsOnMap));
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
