using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.UIPanels
{
    public class UIPanel : UserControl
    {
        public UIPanel()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
