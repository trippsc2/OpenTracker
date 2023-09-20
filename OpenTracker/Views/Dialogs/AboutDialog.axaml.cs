using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;

namespace OpenTracker.Views.Dialogs;

public sealed class AboutDialog : ReactiveWindow<AboutDialogVM>
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