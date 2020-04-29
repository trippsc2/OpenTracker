using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class ItemControl : UserControl
    {
        private IClickHandler ViewModelClickHandler => DataContext as IClickHandler;

        public ItemControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnItemClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                ViewModelClickHandler.OnLeftClick();

            if (e.InitialPressMouseButton == MouseButton.Right)
                ViewModelClickHandler.OnRightClick();
        }
    }
}
