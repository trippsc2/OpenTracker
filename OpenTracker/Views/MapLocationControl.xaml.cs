using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class MapLocationControl : UserControl
    {
        private IClearAvailableSections ViewModelClearAvailableSections => DataContext as IClearAvailableSections;
        public IPinLocation ViewModelPinLocation => DataContext as IPinLocation;
        
        public MapLocationControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Right)
                ViewModelClearAvailableSections.ClearAvailableSections();
        }

        private void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            ViewModelPinLocation.PinLocation();
        }
    }
}
