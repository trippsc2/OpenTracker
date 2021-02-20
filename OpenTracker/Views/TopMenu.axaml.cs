using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ThemeManager;

namespace OpenTracker.Views
{
    public class TopMenu : UserControl
    {
        public static IThemeSelector Selector =>
            App.Selector!;

        public TopMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
