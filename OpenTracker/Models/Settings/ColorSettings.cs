using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This class contains color settings data.
    /// </summary>
    public class ColorSettings : ReactiveObject, IColorSettings
    {
        private string _emphasisFontColor = "#ff00ff00";
        public string EmphasisFontColor
        {
            get => _emphasisFontColor;
            set => this.RaiseAndSetIfChanged(ref _emphasisFontColor, value);
        }

        private string _connectorColor = "#ff40e0d0";
        public string ConnectorColor
        {
            get => _connectorColor;
            set => this.RaiseAndSetIfChanged(ref _connectorColor, value);
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
    }
}
