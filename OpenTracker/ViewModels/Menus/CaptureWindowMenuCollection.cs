using System.Reactive;
using System.Threading.Tasks;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.Capture;
using ReactiveUI;

namespace OpenTracker.ViewModels.Menus
{
    public class CaptureWindowMenuCollection : ViewModelCollection<IMenuItemVM, ICaptureWindowVM>,
    ICaptureWindowMenuCollection
    {
        private readonly IDialogService _dialogService;
        private readonly ICaptureWindowMenuItemVM.Factory _factory;
        
        private readonly ReactiveCommand<ICaptureWindowVM, Unit> _openCaptureWindow;
        
        public CaptureWindowMenuCollection(
            IDialogService dialogService, ICaptureManager captureManager, ICaptureWindowMenuItemVM.Factory factory) :
            base(captureManager.Windows)
        {
            _dialogService = dialogService;
            _factory = factory;

            _openCaptureWindow = ReactiveCommand.CreateFromTask<ICaptureWindowVM>(OpenCaptureWindowImpl);
        }

        private async Task OpenCaptureWindowImpl(ICaptureWindowVM captureWindow)
        {
            if (captureWindow.Open)
            {
                return;
            }
            
            captureWindow.Open = true;
            await _dialogService.ShowDialogAsync(captureWindow);
            captureWindow.Open = false;
        }

        protected override IMenuItemVM CreateViewModel(ICaptureWindowVM model)
        {
            return _factory(model, _openCaptureWindow);
        }
    }
}