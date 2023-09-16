using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Markings.Images;

namespace OpenTracker.Views.Markings.Images;

public sealed class MarkingImage : ReactiveUserControl<MarkingImageVM>
{
    public MarkingImage()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}