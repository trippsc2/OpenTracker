using Avalonia;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface for saving app settings.
    /// </summary>
    public interface ICloseHandler
    {
        void Close(bool maximized, Rect bounds);
    }
}
