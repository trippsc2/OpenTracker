using Avalonia.Media;

namespace OpenTracker.ViewModels.Capture;

public interface ICaptureWindowVM : ICaptureControlVM
{
    bool IsOpen { get; }
    double? Height { get; set; }
    double? Width { get; set; }
    SolidColorBrush? BackgroundColor { get; set; }
}