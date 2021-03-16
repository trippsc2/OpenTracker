using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Capture.Design
{
    public class CaptureWindowDesign : UserControl
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
}