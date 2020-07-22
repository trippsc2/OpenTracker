using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views.UIPanels.LocationsPanel.PinnedLocations
{
    public class PinnedLocation : UserControl
    {
        public IClickHandler ViewModelClickHandler =>
            DataContext as IClickHandler;

        public PinnedLocation()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                ViewModelClickHandler.OnLeftClick();
            }
        }
    }
}
