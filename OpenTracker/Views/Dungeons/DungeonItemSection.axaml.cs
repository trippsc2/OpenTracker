using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Dungeons
{
    public class DungeonItemSection : UserControl
    {
        public DungeonItemSection()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
