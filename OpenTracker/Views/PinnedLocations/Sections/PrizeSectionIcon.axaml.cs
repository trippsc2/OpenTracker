using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations.Sections;

namespace OpenTracker.Views.PinnedLocations.Sections;

public sealed class PrizeSectionIcon : ReactiveUserControl<PrizeSectionIconVM>
{
    public PrizeSectionIcon()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}