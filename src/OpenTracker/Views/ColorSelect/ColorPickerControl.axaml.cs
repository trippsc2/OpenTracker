using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.ColorSelect
{
    public partial class ColorPickerControl : UserControl
    {
        public ColorPickerControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
