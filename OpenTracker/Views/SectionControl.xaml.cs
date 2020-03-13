using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views
{
    public class SectionControl : UserControl
    {
        public SectionControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
