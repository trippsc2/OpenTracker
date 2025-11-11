using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.BossSelect
{
    public partial class BossSelectButton : UserControl
    {
        public BossSelectButton()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
