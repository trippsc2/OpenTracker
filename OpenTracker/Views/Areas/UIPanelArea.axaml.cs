using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Areas;

namespace OpenTracker.Views.Areas;

public sealed class UIPanelArea : ReactiveUserControl<UIPanelAreaVM>
{
    public UIPanelArea()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}