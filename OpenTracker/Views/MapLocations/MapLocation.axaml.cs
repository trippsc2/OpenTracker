using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.MapLocations
{
    public class MapLocation : UserControl
    {
        public MapLocation()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}