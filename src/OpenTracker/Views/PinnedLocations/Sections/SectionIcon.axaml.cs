using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.PinnedLocations.Sections;

public partial class SectionIcon : UserControl
{
    public SectionIcon()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}