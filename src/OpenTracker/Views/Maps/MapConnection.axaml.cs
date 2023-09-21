using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Maps;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.Maps;

public sealed class MapConnection : ReactiveUserControl<MapConnectionVM>
{
    private Line Line => this.FindControl<Line>(nameof(Line));
    
    public MapConnection()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            if (ViewModel is null)
            {
                return;
            }

            Line.Events()
                .PointerReleased
                .InvokeCommand(ViewModel.HandleClickCommand)
                .DisposeWith(disposables);
            Line.Events()
                .PointerEnter
                .InvokeCommand(ViewModel.HandlePointerEnterCommand)
                .DisposeWith(disposables);
            Line.Events()
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