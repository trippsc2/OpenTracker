using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.Views.Dialogs;

public partial class AutoTrackerDialog : DialogWindowBase
{
    public AutoTrackerDialog()
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