using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using OpenTracker.Interfaces;
using System.Linq;

namespace OpenTracker.Views.Maps.Locations
{
    public class ShopMapLocation : UserControl
    {
        private IClickHandler? ViewModelClickHandler =>
            DataContext as IClickHandler;
        private IDoubleClickHandler? ViewModelPinLocation => DataContext
            as IDoubleClickHandler;
        private IPointerOver? ViewModelPointerOver =>
            DataContext as IPointerOver;

        public ShopMapLocation()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            if (ViewModelClickHandler == null)
            {
                return;
            }

            if (e.InitialPressMouseButton == MouseButton.Right &&
                this.GetVisualsAt(e.GetPosition(this)).Any(x => this == x ||
                this.IsVisualAncestorOf(x)))
            {
                ViewModelClickHandler.OnRightClick(e.KeyModifiers == KeyModifiers.Control);
            }
        }

        private void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            if (ViewModelPinLocation == null)
            {
                return;
            }

            ViewModelPinLocation.OnDoubleClick();
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
