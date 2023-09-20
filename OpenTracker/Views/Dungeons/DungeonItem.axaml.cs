using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dungeons;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.Dungeons;

public sealed class DungeonItem : ReactiveUserControl<DungeonItemVM>
{
    private Panel Panel => this.FindControl<Panel>("Panel");

    public DungeonItem()
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