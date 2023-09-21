using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.ColorSelect;

namespace OpenTracker.Views.ColorSelect;

public sealed class ColorSelectDialog : ReactiveWindow<ColorSelectDialogVM>
{
    public ColorSelectDialog()
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