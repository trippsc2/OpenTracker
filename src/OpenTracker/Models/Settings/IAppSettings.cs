using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This interface contains app settings data.
    /// </summary>
    public interface IAppSettings : ISaveable<AppSettingsSaveData>
    {
        IBoundsSettings Bounds { get; }
        ILayoutSettings Layout { get; }
        ITrackerSettings Tracker { get; }
        IColorSettings Colors { get; }
    }
}