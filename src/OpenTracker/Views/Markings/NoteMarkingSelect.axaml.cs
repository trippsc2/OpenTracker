using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Markings;

namespace OpenTracker.Views.Markings;

public sealed class NoteMarkingSelect : ReactiveUserControl<NoteMarkingSelectVM>
{
    public NoteMarkingSelect()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}