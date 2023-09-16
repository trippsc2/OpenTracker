using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels;
using ReactiveUI;

namespace OpenTracker.Views;

public sealed class StatusBar : ReactiveUserControl<StatusBarVM>
{
    private ContentControl AutoTrackerStatus => this.FindControl<ContentControl>("AutoTrackerStatus");
    
    public StatusBar()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.AutoTrackerStatus,
                    v => v.AutoTrackerStatus.Content)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}