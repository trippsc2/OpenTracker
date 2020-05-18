using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class ConnectorControl : UserControl
    {
        private IClickHandler ViewModelClickHandler =>
            DataContext as IClickHandler;
        private IPointerOver ViewModelPointerOver =>
            DataContext as IPointerOver;

        public ConnectorControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                ViewModelClickHandler.OnLeftClick();

            if (e.InitialPressMouseButton == MouseButton.Right)
                ViewModelClickHandler.OnRightClick();
        }

        private void OnPointerEnter(object sender, PointerEventArgs e)
        {
            ViewModelPointerOver.OnPointerEnter();
        }

        private void OnPointerLeave(object sender, PointerEventArgs e)
        {
            ViewModelPointerOver.OnPointerLeave();
        }
    }
}
