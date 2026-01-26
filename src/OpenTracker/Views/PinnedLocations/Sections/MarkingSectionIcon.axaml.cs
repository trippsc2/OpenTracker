using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.PinnedLocations.Sections;

public partial class MarkingSectionIcon : UserControl
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