using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using OpenTracker.Utils.Dialog;
using ReactiveUI;

namespace OpenTracker.ViewModels.Capture
{
    public class CaptureManager : ICaptureManager
    {
        private readonly IDialogService _dialogService;
        private readonly Lazy<ICaptureWindowCollection> _windows;
        
        public ReactiveCommand<ICaptureWindowVM, Unit> OpenCaptureWindow { get; }

        public CaptureManager(IDialogService dialogService, ICaptureWindowCollection.Factory windows)
        {
            _dialogService = dialogService;
            _windows = new Lazy<ICaptureWindowCollection>(windows());

            OpenCaptureWindow = ReactiveCommand.CreateFromTask<ICaptureWindowVM>(OpenCaptureWindowImpl);
        }

        private async Task OpenCaptureWindowImpl(ICaptureWindowVM captureWindow)
        {
            if (captureWindow.Open)
            {
                return;
            }

            captureWindow.Open = true;
            await Dispatcher.UIThread.InvokeAsync(async () =>
                await _dialogService.ShowDialogAsync(captureWindow));
            captureWindow.Open = false;
        }

        public void GenerateInitialData()
        {
            _windows.Value.Add(new CaptureWindowVM("Window 1"));
        }
    }
}