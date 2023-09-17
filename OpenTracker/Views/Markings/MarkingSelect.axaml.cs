using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Markings;

namespace OpenTracker.Views.Markings;

public sealed class MarkingSelect : ReactiveUserControl<MarkingSelectVM>
{
    public MarkingSelect()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}