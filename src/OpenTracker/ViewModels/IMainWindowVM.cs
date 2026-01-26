using Avalonia;
using Avalonia.Layout;

namespace OpenTracker.ViewModels;

/// <summary>
/// This is the interface for the main window ViewModel.
/// </summary>
public interface IMainWindowVM
{
    bool? Maximized { get; set; }
    double? X { get; set; }
    double? Y { get; set; }

    void ChangeLayout(Orientation orientation);
    void OnClose(bool maximized, Rect bounds, PixelPoint pixelPoint);
}