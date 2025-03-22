using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Markings.Images
{
    public class MarkingImage : UserControl
    {
        public MarkingImage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
