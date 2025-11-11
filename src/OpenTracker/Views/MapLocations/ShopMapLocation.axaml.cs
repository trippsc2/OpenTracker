using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.MapLocations
{
    public partial class ShopMapLocation : UserControl
    {
        public ShopMapLocation()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}