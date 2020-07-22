using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.UIPanels.LocationsPanel.PinnedLocations.Sections
{
    public class Section : UserControl
    {
        public Section()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
