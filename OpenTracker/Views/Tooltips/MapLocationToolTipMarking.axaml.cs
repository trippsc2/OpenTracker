using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Tooltips
{
    public class MapLocationToolTipMarking : UserControl
    {
        public MapLocationToolTipMarking()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
