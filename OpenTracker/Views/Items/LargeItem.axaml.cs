using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Items;

namespace OpenTracker.Views.Items;

public sealed class LargeItem : ReactiveUserControl<LargeItemVM>
{
    public LargeItem()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}