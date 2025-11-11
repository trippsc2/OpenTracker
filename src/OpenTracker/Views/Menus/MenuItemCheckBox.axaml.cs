using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Menus
{
    public partial class MenuItemCheckBox : UserControl
    {
        public MenuItemCheckBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}