using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.ColorSelect;

namespace OpenTracker.Views.ColorSelect;

public sealed class ColorSelectDialog : DialogWindowBase<ColorSelectDialogVM>
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