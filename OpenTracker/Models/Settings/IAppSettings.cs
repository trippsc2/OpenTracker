using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This is the interface for application data to be saved to file.
    /// </summary>
    public interface IAppSettings : ISaveable<AppSettingsSaveData>
    {
        IBoundsSettings Bounds { get; }
        ILayoutSettings Layout { get; }
        ITrackerSettings Tracker { get; }
        IColorSettings Colors { get; }
    }
}