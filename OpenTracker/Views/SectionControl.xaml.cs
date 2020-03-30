using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class SectionControl : UserControl
    {
        private ISectionControlVM _viewModel => DataContext as ISectionControlVM;

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
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    _viewModel.OpenMarkingSelect();
                    break;
            }
        }

        private void OnClickSection(object sender, PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    _viewModel.ChangeAvailable();
                    break;
                case MouseButton.Right:
                    _viewModel.ChangeAvailable(true);
                    break;
            
            }
        }
    }
}
