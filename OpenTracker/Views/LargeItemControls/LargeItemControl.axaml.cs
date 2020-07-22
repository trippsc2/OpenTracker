using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views.LargeItemControls
{
    public class LargeItemControl : UserControl
    {
        private IClickHandler ViewModelClickHandler =>
            DataContext as IClickHandler;

        public LargeItemControl()
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

            if (e.InitialPressMouseButton == MouseButton.Right)
            {
                ViewModelClickHandler.OnRightClick();
            }
        }
    }
}
