using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.AutoTracking;
using ReactiveUI;

namespace OpenTracker.Views.AutoTracking;

public sealed class AutoTrackerStatus : ReactiveUserControl<AutoTrackerStatusVM>
{
    private TextBlock StatusText => this.FindControl<TextBlock>("StatusText");
    
    public AutoTrackerStatus()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.StatusText,
                    v => v.StatusText.Text)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.StatusTextColor,
                    v => v.StatusText.Foreground)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}