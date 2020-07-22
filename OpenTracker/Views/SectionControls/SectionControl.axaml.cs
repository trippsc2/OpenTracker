using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.SectionControls
{
    public class SectionControl : UserControl
    {
        public SectionControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
