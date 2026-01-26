using System.ComponentModel;
using Avalonia.Media;
using Avalonia.Threading;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Capture.Design;

public class CaptureWindowDesignVM : ViewModelBase, ICaptureDesignVM
{
    private readonly ICaptureWindowVM _captureWindow;

    public SolidColorBrush? BackgroundColor
    {
        get => _captureWindow.BackgroundColor;
        set => _captureWindow.BackgroundColor = value;
    }

    public delegate CaptureWindowDesignVM Factory(ICaptureWindowVM captureWindowVM);

    public CaptureWindowDesignVM(ICaptureWindowVM captureWindow)
    {
        _captureWindow = captureWindow;

        _captureWindow.PropertyChanged += OnCaptureWindowChanged;
    }

    private async void OnCaptureWindowChanged(object? sender, PropertyChangedEventArgs e)
    {
        await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(e.PropertyName));
    }
}