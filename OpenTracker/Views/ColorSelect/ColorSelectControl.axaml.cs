using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.ColorSelect;

namespace OpenTracker.Views.ColorSelect;

public sealed class ColorSelectControl : ReactiveUserControl<ColorSelectControlVM>
{
    public ColorSelectControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}