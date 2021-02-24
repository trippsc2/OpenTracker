using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using OpenTracker.Interfaces;
using System.Linq;

namespace OpenTracker.Views.Maps.Locations
{
    public class DungeonMapLocation : UserControl
    {
        private IClickHandler? ClickHandler =>
            DataContext as IClickHandler;
        private IDoubleClickHandler? DoubleClickHandler =>
            DataContext as IDoubleClickHandler;
        private IPointerOver? PointerOver =>
            DataContext as IPointerOver;

        public DungeonMapLocation()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            if (ClickHandler == null)
            {
                return;
            }

            if (e.InitialPressMouseButton == MouseButton.Right &&
                this.GetVisualsAt(e.GetPosition(this)).Any(x => this == x ||
                this.IsVisualAncestorOf(x)))
            {
                ClickHandler.OnRightClick(e.KeyModifiers == KeyModifiers.Control);
            }
        }

        private void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            if (DoubleClickHandler == null)
            {
                return;
            }

            DoubleClickHandler.OnDoubleClick();
        }

        private void OnPointerEnter(object sender, PointerEventArgs e)
        {
            if (PointerOver == null)
            {
                return;
            }

            PointerOver.OnPointerEnter();
        }

        private void OnPointerLeave(object sender, PointerEventArgs e)
        {
            if (PointerOver == null)
            {
                return;
            }

            PointerOver.OnPointerLeave();
        }
    }
}
