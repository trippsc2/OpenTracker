using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations;

namespace OpenTracker.Views.PinnedLocations;

public sealed class PinnedLocation : ReactiveUserControl<PinnedLocationVM>
{
    public PinnedLocation()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}