using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.Views
{
    public class MessageBoxDialog : DialogWindowBase<bool>
    {
        public MessageBoxDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
