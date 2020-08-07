using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.PinnedLocations
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
