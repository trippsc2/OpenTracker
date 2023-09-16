using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.Dialogs;

namespace OpenTracker.Views.Dialogs;

public sealed class MessageBoxDialog : DialogWindowBase<MessageBoxDialogVM, bool>
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