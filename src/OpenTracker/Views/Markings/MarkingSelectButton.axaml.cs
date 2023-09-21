using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Markings;

namespace OpenTracker.Views.Markings;

public sealed class MarkingSelectButton : ReactiveUserControl<MarkingSelectButtonVM>
{
    public MarkingSelectButton()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}