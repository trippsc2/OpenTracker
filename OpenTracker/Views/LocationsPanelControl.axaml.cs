using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views
{
    public class LocationsPanelControl : UserControl
    {
        public LocationsPanelControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
