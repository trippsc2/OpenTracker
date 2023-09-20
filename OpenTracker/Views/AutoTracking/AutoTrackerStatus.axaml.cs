using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.AutoTracking;

namespace OpenTracker.Views.AutoTracking;

public sealed class AutoTrackerStatus : ReactiveUserControl<AutoTrackerStatusVM>
{
    public AutoTrackerStatus()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}