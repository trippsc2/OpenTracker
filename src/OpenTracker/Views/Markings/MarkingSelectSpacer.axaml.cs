using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Markings;

public partial class MarkingSelectSpacer : UserControl
{
    public MarkingSelectSpacer()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}