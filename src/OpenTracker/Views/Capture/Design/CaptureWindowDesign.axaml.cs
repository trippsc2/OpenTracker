using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Capture.Design;

public partial class CaptureWindowDesign : UserControl
{
    public CaptureWindowDesign()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}