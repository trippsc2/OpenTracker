using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Areas
{
    public partial class UIPanelArea : UserControl
    {
        public UIPanelArea()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
