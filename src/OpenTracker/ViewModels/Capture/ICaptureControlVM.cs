using System.Collections.ObjectModel;
using ReactiveUI;

namespace OpenTracker.ViewModels.Capture
{
    public interface ICaptureControlVM : IReactiveObject
    {
        string Name { get; set; }
        
        ObservableCollection<ICaptureControlVM> Contents { get; }
    }
}