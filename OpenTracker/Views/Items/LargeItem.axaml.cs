using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Items
{
    public class LargeItem : UserControl
    {
        public LargeItem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}