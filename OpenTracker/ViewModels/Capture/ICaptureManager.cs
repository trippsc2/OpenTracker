using System.Reactive;
using ReactiveUI;

namespace OpenTracker.ViewModels.Capture
{
    public interface ICaptureManager
    {
        ReactiveCommand<ICaptureWindowVM, Unit> OpenCaptureWindow { get; }
        void GenerateInitialData();
    }
}