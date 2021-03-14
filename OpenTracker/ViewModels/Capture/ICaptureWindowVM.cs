using ReactiveUI;

namespace OpenTracker.ViewModels.Capture
{
    public interface ICaptureWindowVM : IReactiveObject
    {
        bool Open { get; set; }
        double? Height { get; set; }
        double? Width { get; set; }
        string Name { get; set; }
    }
}