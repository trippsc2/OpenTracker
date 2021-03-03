using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using OpenTracker.Interfaces;
using System.Linq;

namespace OpenTracker.Views.Maps.Locations
{
    public class EntranceMapLocation : UserControl
    {
        public IConnectLocation? ViewModelConnectLocation =>
            DataContext as IConnectLocation;

        public EntranceMapLocation()
        {
            InitializeComponent();

            AddHandler(DragDrop.DropEvent, OnDrop);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                return;
            }
            
            DataObject dragData = new DataObject();
            dragData.Set(DataFormats.Text, this);

            await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Copy);
        }

        private void OnDrop(object? sender, DragEventArgs e)
        {
            if (e.Data.Get(DataFormats.Text) is EntranceMapLocation mapEntrance && mapEntrance != this)
            {
                if (ViewModelConnectLocation == null ||
                    mapEntrance.ViewModelConnectLocation == null)
                {
                    return;
                }

                ViewModelConnectLocation.ConnectLocation(mapEntrance.ViewModelConnectLocation);
            }
        }
    }
}
