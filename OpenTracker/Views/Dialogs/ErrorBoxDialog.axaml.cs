using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.Views.Dialogs
{
    public class ErrorBoxDialog : DialogWindowBase
    {
        public ErrorBoxDialog()
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
