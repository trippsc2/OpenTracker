using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using OpenTracker.Interfaces;
using System.Linq;

namespace OpenTracker.Views
{
    public class MapLocationControl : UserControl
    {
        private IClearAvailableSections ViewModelClearAvailableSections =>
            DataContext as IClearAvailableSections;
        private IOpenMarkingSelect ViewModelOpenMarkingSelect =>
            DataContext as IOpenMarkingSelect;
        private IPinLocation ViewModelPinLocation => DataContext
            as IPinLocation;
        private IPointerOver ViewModelPointerOver =>
            DataContext as IPointerOver;

        public MapLocationControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnImageClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left &&
                this.GetVisualsAt(e.GetPosition(this)).Any(x => this == x || this.IsVisualAncestorOf(x)))
                ViewModelOpenMarkingSelect.OpenMarkingSelect();
        }

        private void OnShapeClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Right &&
                this.GetVisualsAt(e.GetPosition(this)).Any(x => this == x || this.IsVisualAncestorOf(x)))
                ViewModelClearAvailableSections.ClearAvailableSections(e.KeyModifiers == KeyModifiers.Control);
        }

        private void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            ViewModelPinLocation.PinLocation();
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
