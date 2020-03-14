using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class MapLocationControl : UserControl
    {
        private IMapLocationControlVM _viewModel => DataContext as IMapLocationControlVM;
        
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
                _viewModel.ClearAvailableSections();
        }

        private void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PinLocation();
        }
    }
}
