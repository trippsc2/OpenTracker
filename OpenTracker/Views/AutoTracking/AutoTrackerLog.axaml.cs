using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.AutoTracking;

namespace OpenTracker.Views.AutoTracking;

public sealed class AutoTrackerLog : ReactiveUserControl<AutoTrackerLogVM>
{
    public AutoTrackerLog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}