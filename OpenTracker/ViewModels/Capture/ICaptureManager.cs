using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Capture
{
    public interface ICaptureManager
    {
        CaptureWindowCollection Windows { get; }
    }
}