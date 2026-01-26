using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Markings;

public partial class MarkingSelectButton : UserControl
{
    public MarkingSelectButton()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}