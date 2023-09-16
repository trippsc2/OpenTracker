using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Items;

namespace OpenTracker.Views.Items;

public sealed class LargeItemPanel : ReactiveUserControl<LargeItemPanelVM>
{
    public LargeItemPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}