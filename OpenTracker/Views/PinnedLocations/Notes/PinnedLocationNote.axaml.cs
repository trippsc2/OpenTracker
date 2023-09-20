using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations.Notes;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.PinnedLocations.Notes;

public sealed class PinnedLocationNote : ReactiveUserControl<PinnedLocationNoteVM>
{
    private Panel Panel => this.FindControl<Panel>(nameof(Panel));

    public PinnedLocationNote()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            if (ViewModel is null)
            {
                return;
            }

            Panel.Events()
                .PointerReleased
                .InvokeCommand(ViewModel.HandleClickCommand)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}