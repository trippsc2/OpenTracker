using ReactiveUI;

namespace OpenTracker.ViewModels.Capture
{
    public interface ICaptureWindowVM : ICaptureControlVM
    {
        bool Open { get; set; }
        double? Height { get; set; }
        double? Width { get; set; }
    }
}