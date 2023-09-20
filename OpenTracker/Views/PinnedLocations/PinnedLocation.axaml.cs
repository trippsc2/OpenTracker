using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.PinnedLocations;

public sealed class PinnedLocation : ReactiveUserControl<PinnedLocationVM>
{
    private Image CloseImage => this.FindControl<Image>(nameof(CloseImage));

    public PinnedLocation()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            if (ViewModel is null)
            {
                return;
            }

            CloseImage.Events()
                .PointerReleased
                .InvokeCommand(ViewModel.HandleClickCommand)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}