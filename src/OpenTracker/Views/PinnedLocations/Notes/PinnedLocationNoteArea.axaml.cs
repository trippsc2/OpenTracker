using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.PinnedLocations.Notes;

public partial class PinnedLocationNoteArea : UserControl
{
    public PinnedLocationNoteArea()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}