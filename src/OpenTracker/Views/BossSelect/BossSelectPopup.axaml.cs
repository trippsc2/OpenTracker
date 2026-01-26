using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.BossSelect;

public partial class BossSelectPopup : UserControl
{
    public BossSelectPopup()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}