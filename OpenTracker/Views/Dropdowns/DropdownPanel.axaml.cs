using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Dropdowns
{
    public class DropdownPanel : UserControl
    {
        public DropdownPanel()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
