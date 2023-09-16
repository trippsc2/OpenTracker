using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.UIPanels;

public class UIPanel : UserControl
{
    public UIPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}