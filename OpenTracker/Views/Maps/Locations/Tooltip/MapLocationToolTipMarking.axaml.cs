using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Maps.Locations.Tooltip
{
    public class MapLocationToolTipMarking : UserControl
    {
        public MapLocationToolTipMarking()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
