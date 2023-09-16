using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations.Notes;

namespace OpenTracker.Views.PinnedLocations.Notes;

public sealed class PinnedLocationNote : ReactiveUserControl<PinnedLocationNoteVM>
{
    public PinnedLocationNote()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}