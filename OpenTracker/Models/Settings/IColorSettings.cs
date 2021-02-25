using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Utils;
using System.ComponentModel;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This interface contains color settings data.
    /// </summary>
    public interface IColorSettings : INotifyPropertyChanged
    {
        ObservableDictionary<AccessibilityLevel, string> AccessibilityColors { get; }
        string ConnectorColor { get; set; }
        string EmphasisFontColor { get; set; }
    }
}