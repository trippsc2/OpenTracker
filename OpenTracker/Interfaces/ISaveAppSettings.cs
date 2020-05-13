using Avalonia;

namespace OpenTracker.Interfaces
{
    public interface ISaveAppSettings
    {
        void SaveAppSettings(bool maximized, Rect bounds);
    }
}
