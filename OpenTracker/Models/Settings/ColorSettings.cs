using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Utils;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This class contains color settings data.
    /// </summary>
    public class ColorSettings : IColorSettings
    {
        public event PropertyChangedEventHandler? PropertyChanged;

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
            new ObservableDictionary<AccessibilityLevel, string>(
                new Dictionary<AccessibilityLevel, string>
                {
                    { AccessibilityLevel.None, "#ffff3030" },
                    { AccessibilityLevel.Inspect, "#ff6495ed" },
                    { AccessibilityLevel.Partial, "#ffff8c00" },
                    { AccessibilityLevel.SequenceBreak, "#ffffff00" },
                    { AccessibilityLevel.Normal, "#ff00ff00" },
                    { AccessibilityLevel.Cleared, "#ff333333" }
                });

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// A string representing the property name that changed.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
