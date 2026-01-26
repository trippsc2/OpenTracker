using Avalonia;
using Avalonia.Markup.Xaml;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.Views.ColorSelect;

public partial class ColorSelectDialog : DialogWindowBase
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