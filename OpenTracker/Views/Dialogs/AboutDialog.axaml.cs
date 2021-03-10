using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.Views.Dialogs
{
    public class AboutDialog : DialogWindowBase
    {
        public AboutDialog()
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
