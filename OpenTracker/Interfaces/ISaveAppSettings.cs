using Avalonia;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface for saving app settings.
    /// </summary>
    public interface ISaveAppSettings
    {
        void SaveAppSettings(bool maximized, Rect bounds);
    }
}
