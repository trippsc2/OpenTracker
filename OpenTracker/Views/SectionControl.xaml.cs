using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class SectionControl : UserControl
    {
        public IClickHandler ViewModelClickHandler => DataContext as IClickHandler;
        public IOpenMarkingSelect ViewModelOpenMarkingSelect => DataContext as IOpenMarkingSelect;

        public SectionControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClickVisibleItem(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                ViewModelOpenMarkingSelect.OpenMarkingSelect();
        }

        private void OnClickSection(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                ViewModelClickHandler.OnLeftClick();

            if (e.InitialPressMouseButton == MouseButton.Right)
                ViewModelClickHandler.OnRightClick();
        }
    }
}
