using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This interface contains color settings data.
    /// </summary>
    public interface IColorSettings : IReactiveObject
    {
        ObservableDictionary<AccessibilityLevel, string> AccessibilityColors { get; }
        string ConnectorColor { get; set; }
        string EmphasisFontColor { get; set; }
    }
}