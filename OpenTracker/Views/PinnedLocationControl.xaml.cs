using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class PinnedLocationControl : UserControl
    {
        private IPinnedLocationControlVM _viewModel => DataContext as IPinnedLocationControlVM;

        public PinnedLocationControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                _viewModel.Close();
        }
    }
}
