using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.ColorSelect
{
    public class ColorSelectControl : UserControl
    {
        public ColorSelectControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}