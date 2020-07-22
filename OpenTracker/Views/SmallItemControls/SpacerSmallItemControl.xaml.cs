using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.SmallItemControls
{
    public class SpacerSmallItemControl : UserControl
    {
        public SpacerSmallItemControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
