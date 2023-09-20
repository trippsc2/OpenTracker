using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;

namespace OpenTracker.Views.Dialogs;

public sealed class SequenceBreakDialog : ReactiveWindow<SequenceBreakDialogVM>
{
    public SequenceBreakDialog()
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