using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.SequenceBreaks;

namespace OpenTracker.Views.SequenceBreaks;

public sealed class SequenceBreakControl : ReactiveUserControl<SequenceBreakControlVM>
{
    public SequenceBreakControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}