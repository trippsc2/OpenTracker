using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.UIPanels.LocationsPanel
{
    public class PinnedLocationNoteArea : UserControl
    {
        public PinnedLocationNoteArea()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
