using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.Dialogs;

namespace OpenTracker.Views.Dialogs;

public sealed class ErrorBoxDialog : DialogWindowBase<ErrorBoxDialogVM>
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