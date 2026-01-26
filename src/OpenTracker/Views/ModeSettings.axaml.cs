using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views;

public partial class ModeSettings : UserControl
{
    public ModeSettings()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}