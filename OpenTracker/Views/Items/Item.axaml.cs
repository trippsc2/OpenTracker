using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Items;

namespace OpenTracker.Views.Items;

public sealed class Item : ReactiveUserControl<ItemVM>
{
    public Item()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}