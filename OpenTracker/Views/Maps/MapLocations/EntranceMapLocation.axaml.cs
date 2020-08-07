using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using OpenTracker.Interfaces;
using System.Linq;

namespace OpenTracker.Views.Maps.MapLocations
{
    public class EntranceMapLocation : UserControl
    {
        private IClickHandler ViewModelClickHandler =>
            DataContext as IClickHandler;
        private IDoubleClickHandler ViewModelPinLocation =>
            DataContext as IDoubleClickHandler;
        private IPointerOver ViewModelPointerOver =>
            DataContext as IPointerOver;
        public IConnectLocation ViewModelConnectLocation =>
            DataContext as IConnectLocation;

        public EntranceMapLocation()
        {
            this.InitializeComponent();

            AddHandler(DragDrop.DropEvent, OnDrop);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                DataObject dragData = new DataObject();
                dragData.Set(DataFormats.Text, this);

                await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Copy).ConfigureAwait(false);
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.Get(DataFormats.Text) is EntranceMapLocation mapEntrance &&
                mapEntrance != this)
            {
                ViewModelConnectLocation.ConnectLocation(mapEntrance.ViewModelConnectLocation);
            }
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Right &&
                this.GetVisualsAt(e.GetPosition(this)).Any(x => this == x || this.IsVisualAncestorOf(x)))
            {
                ViewModelClickHandler.OnRightClick(e.KeyModifiers == KeyModifiers.Control);
            }
        }

        private void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            ViewModelPinLocation.OnDoubleClick();
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
