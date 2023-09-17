using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations.Sections;

namespace OpenTracker.Views.PinnedLocations.Sections;

public sealed class BossSectionIcon : ReactiveUserControl<BossSectionIconVM>
{
    public BossSectionIcon()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}