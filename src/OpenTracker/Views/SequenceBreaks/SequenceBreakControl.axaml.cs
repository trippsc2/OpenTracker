using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.SequenceBreaks
{
    public partial class SequenceBreakControl : UserControl
    {
        public SequenceBreakControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
