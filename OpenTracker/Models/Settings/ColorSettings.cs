using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Utils;
using System.ComponentModel;

namespace OpenTracker.Models.Settings
{
    public class ColorSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _emphasisFontColor = "#ff00ff00";
        public string EmphasisFontColor
        {
            get => _emphasisFontColor;
            set
            {
                if (_emphasisFontColor != value)
                {
                    _emphasisFontColor = value;
                    OnPropertyChanged(nameof(EmphasisFontColor));
                }
            }
        }

        private string _connectorColor = "#ff40e0d0";
        public string ConnectorColor
        {
            get => _connectorColor;
            set
            {
                if (_connectorColor != value)
                {
                    _connectorColor = value;
                    OnPropertyChanged(nameof(ConnectorColor));
                }
            }
        }

        public ObservableDictionary<AccessibilityLevel, string> AccessibilityColors { get; } =
            new ObservableDictionary<AccessibilityLevel, string>();

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
