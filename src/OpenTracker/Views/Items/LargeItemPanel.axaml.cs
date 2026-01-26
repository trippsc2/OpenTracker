using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Items;

public partial class LargeItemPanel : UserControl
{
    public LargeItemPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}