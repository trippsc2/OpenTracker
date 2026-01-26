using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.AutoTracking;

public partial class AutoTrackerLog : UserControl
{
    public AutoTrackerLog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}