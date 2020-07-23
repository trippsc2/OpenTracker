using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views.MarkingSelect
{
    public class MarkingSelectButton : UserControl
    {
        public ISelectItem ViewModelSelectItem => DataContext as ISelectItem;

        public MarkingSelectButton()
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
                ViewModelSelectItem.SelectItem();
            }
        }
    }
}
