using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.MapLocations;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.MapLocations;

public sealed class StandardMapLocation : ReactiveUserControl<StandardMapLocationVM>
{
    private Panel Panel => this.FindControl<Panel>(nameof(Panel));

    public StandardMapLocation()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            if (ViewModel is null)
            {
                return;
            }

            Panel.Events()
                .PointerReleased
                .InvokeCommand(ViewModel.HandleClickCommand)
                .DisposeWith(disposables);
            Panel.Events()
                .DoubleTapped
                .InvokeCommand(ViewModel.HandleDoubleClickCommand)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}