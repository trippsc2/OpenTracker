using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class DungeonItemControl : UserControl
    {
        private IItemControlVM _viewModel => DataContext as IItemControlVM;
        public DungeonItemControl()
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
                _viewModel.ChangeItem();

            if (e.InitialPressMouseButton == MouseButton.Right)
                _viewModel.ChangeItem(true);
        }
    }
}
