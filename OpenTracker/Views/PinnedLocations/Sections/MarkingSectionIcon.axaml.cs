using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations.Sections;

namespace OpenTracker.Views.PinnedLocations.Sections;

public sealed class MarkingSectionIcon : ReactiveUserControl<MarkingSectionIconVM>
{
    public MarkingSectionIcon()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}