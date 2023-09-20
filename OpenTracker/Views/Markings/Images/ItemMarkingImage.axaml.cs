using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Markings.Images;

namespace OpenTracker.Views.Markings.Images;

public sealed class ItemMarkingImage : ReactiveUserControl<ItemMarkingImageVM>
{
    public ItemMarkingImage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}