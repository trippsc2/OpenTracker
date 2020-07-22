using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.LargeItemControls
{
    public class SpacerLargeItemControl : UserControl
    {
        public SpacerLargeItemControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
