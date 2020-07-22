using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views.SectionControls
{
    public class ItemSectionIconControl : UserControl
    {
        private IClickHandler ViewModelClickHandler =>
            DataContext as IClickHandler;

        public ItemSectionIconControl()
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
                ViewModelClickHandler.OnLeftClick(e.KeyModifiers == KeyModifiers.Control);
            }

            if (e.InitialPressMouseButton == MouseButton.Right)
            {
                ViewModelClickHandler.OnRightClick(e.KeyModifiers == KeyModifiers.Control);
            }
        }
    }
}
