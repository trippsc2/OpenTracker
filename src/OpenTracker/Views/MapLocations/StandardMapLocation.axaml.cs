using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.MapLocations
{
    public partial class StandardMapLocation : UserControl
    {
        public StandardMapLocation()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}