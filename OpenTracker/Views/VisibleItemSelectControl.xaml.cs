using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class VisibleItemSelectControl : UserControl
    {
        private IVisibleItemSelectControlVM _viewModel => DataContext as IVisibleItemSelectControlVM;
        public VisibleItemSelectControl()
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
                _viewModel.SelectItem();
        }
    }
}
