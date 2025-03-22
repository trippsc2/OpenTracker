using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Maps
{
    public class MapConnection : UserControl
    {
        public MapConnection()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
