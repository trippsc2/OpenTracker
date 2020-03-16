using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class DungeonPrizeControl : UserControl
    {
        private IItemControlVM _viewModel => DataContext as IItemControlVM;

        public DungeonPrizeControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    _viewModel.ChangeItem();
                    break;
                case MouseButton.Right:
                    _viewModel.ChangeItem(true);
                    break;
            }
        }
    }
}
