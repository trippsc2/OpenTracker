using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Capture
{
    public class CaptureManager : ICaptureManager
    {
        public CaptureWindowCollection Windows { get; } = new CaptureWindowCollection();

        public CaptureManager()
        {
            Windows.Add(new CaptureWindowVM("Window 1"));
        }
    }
}