using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations.Sections;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.PinnedLocations.Sections;

public sealed class MarkingSectionIcon : ReactiveUserControl<MarkingSectionIconVM>
{
    private Panel Panel => this.FindControl<Panel>(nameof(Panel));

    public MarkingSectionIcon()
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
                .InvokeCommand(ViewModel.HandleClick)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}