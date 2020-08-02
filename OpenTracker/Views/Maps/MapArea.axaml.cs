using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Maps
{
    public class MapArea : UserControl
    {
        public MapArea()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
