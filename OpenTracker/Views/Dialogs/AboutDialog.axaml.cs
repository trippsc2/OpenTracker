using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.Dialogs;

namespace OpenTracker.Views.Dialogs;

public sealed class AboutDialog : DialogWindowBase<AboutDialogVM>
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