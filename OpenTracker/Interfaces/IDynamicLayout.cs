using Avalonia.Layout;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface for a dynamic UI layout.
    /// </summary>
    public interface IDynamicLayout
    {
        void ChangeLayout(Orientation orientation);
    }
}
