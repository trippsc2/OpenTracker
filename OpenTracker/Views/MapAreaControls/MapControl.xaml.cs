using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.MapAreaControls
{
    public class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
