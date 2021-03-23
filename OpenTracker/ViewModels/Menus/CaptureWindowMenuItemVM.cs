using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Threading;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Capture;
using ReactiveUI;

namespace OpenTracker.ViewModels.Menus
{
    public class CaptureWindowMenuItemVM : ViewModelBase, ICaptureWindowMenuItemVM
    {
        private readonly ICaptureWindowVM _captureWindow;

        public object Model => _captureWindow;
        
        public IMenuItemIconVM? Icon { get; }
        public object Header => _captureWindow.Name;
        public ICommand? Command { get; }
        public object? CommandParameter { get; }
        public IList<IMenuItemVM> Items { get; } = new List<IMenuItemVM>();

        public CaptureWindowMenuItemVM(
            ICaptureManager captureManager, MenuItemCheckBoxVM.Factory iconFactory,
            CaptureWindowOpenRequirement.Factory captureWindowOpenFactory, ICaptureWindowVM captureWindow)
        {
            _captureWindow = captureWindow;
            
            Icon = iconFactory(captureWindowOpenFactory(captureWindow));
            Command = captureManager.OpenCaptureWindow;
            CommandParameter = captureWindow;

            _captureWindow.PropertyChanged += OnCaptureWindowChanged;
        }

        private async void OnCaptureWindowChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ICaptureWindowVM.Name))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Header)));
            }
        }
    }
}