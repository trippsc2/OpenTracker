using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.AutoTracking;

public partial class AutoTrackerStatus : UserControl
{
    public AutoTrackerStatus()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}