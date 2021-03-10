using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Tooltips
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
