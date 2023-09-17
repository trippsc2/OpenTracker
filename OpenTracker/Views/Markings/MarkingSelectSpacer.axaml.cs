using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Markings;

namespace OpenTracker.Views.Markings;

public sealed class MarkingSelectSpacer : ReactiveUserControl<MarkingSelectSpacerVM>
{
    public MarkingSelectSpacer()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}