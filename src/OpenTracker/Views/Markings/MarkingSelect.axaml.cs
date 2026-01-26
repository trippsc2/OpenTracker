using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Markings;

public partial class MarkingSelect : UserControl
{
    public MarkingSelect()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}