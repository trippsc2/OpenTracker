using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views.SmallItemControls
{
    public class DungeonItemSmallItemControl : UserControl
    {
        private IClickHandler ViewModelClickHandler =>
            DataContext as IClickHandler;

        public DungeonItemSmallItemControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnItemClick(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                ViewModelClickHandler.OnLeftClick(e.KeyModifiers == KeyModifiers.Control);
            }

            if (e.InitialPressMouseButton == MouseButton.Right)
            {
                ViewModelClickHandler.OnRightClick(e.KeyModifiers == KeyModifiers.Control);
            }
        }
    }
}
