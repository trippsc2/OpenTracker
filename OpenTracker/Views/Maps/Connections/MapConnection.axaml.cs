using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views.Maps.Connections
{
    public class MapConnection : UserControl
    {
        private IClickHandler? ViewModelClickHandler =>
            DataContext as IClickHandler;
        private IPointerOver? ViewModelPointerOver =>
            DataContext as IPointerOver;

        public MapConnection()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (ViewModelClickHandler == null)
            {
                return;
            }

            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                ViewModelClickHandler.OnLeftClick();
            }

            if (e.InitialPressMouseButton == MouseButton.Right)
            {
                ViewModelClickHandler.OnRightClick();
            }
        }

        private void OnPointerEnter(object sender, PointerEventArgs e)
        {
            if (ViewModelPointerOver == null)
            {
                return;
            }

            ViewModelPointerOver.OnPointerEnter();
        }

        private void OnPointerLeave(object sender, PointerEventArgs e)
        {
            if (ViewModelPointerOver == null)
            {
                return;
            }

            ViewModelPointerOver.OnPointerLeave();
        }
    }
}
