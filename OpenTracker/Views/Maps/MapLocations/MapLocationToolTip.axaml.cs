using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Maps.MapLocations
{
    public class MapLocationToolTip : UserControl
    {
        public MapLocationToolTip()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
