using OpenTracker.Utils;
using OpenTracker.ViewModels.Capture;

namespace OpenTracker.ViewModels.Menus
{
    public class CaptureWindowMenuCollection : ViewModelCollection<IMenuItemVM, ICaptureWindowVM>,
    ICaptureWindowMenuCollection
    {
        private readonly ICaptureWindowMenuItemVM.Factory _factory;
        
        public CaptureWindowMenuCollection(
            ICaptureWindowCollection captureWindows, ICaptureWindowMenuItemVM.Factory factory)
            : base(captureWindows)
        {
            _factory = factory;
        }
        
        protected override IMenuItemVM CreateViewModel(ICaptureWindowVM model)
        {
            return _factory(model);
        }
    }
}