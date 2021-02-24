using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using OpenTracker.Interfaces;
using System.Linq;

namespace OpenTracker.Views.Maps.Locations
{
    public class MarkingMapLocation : UserControl
    {
        private IClickHandler ViewModelClickHandler =>
            DataContext as IClickHandler;

        public MarkingMapLocation()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left &&
                this.GetVisualsAt(e.GetPosition(this)).Any(x => this == x || this.IsVisualAncestorOf(x)))
            {
                ViewModelClickHandler.OnLeftClick();
            }
        }
    }
}
