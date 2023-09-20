using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.MapLocations;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.MapLocations;

public sealed class MapLocation : ReactiveUserControl<MapLocationVM>
{
    private DockPanel DockPanel => this.FindControl<DockPanel>(nameof(DockPanel));

    public MapLocation()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            if (ViewModel is null)
            {
                return;
            }

            DockPanel.Events()
                .PointerEnter
                .InvokeCommand(ViewModel.HandlePointerEnterCommand)
                .DisposeWith(disposables);
            DockPanel.Events()
                .PointerLeave
                .InvokeCommand(ViewModel.HandlePointerLeaveCommand)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}