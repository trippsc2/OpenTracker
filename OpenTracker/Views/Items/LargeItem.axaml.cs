using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Items;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views.Items;

public sealed class LargeItem : ReactiveUserControl<LargeItemVM>
{
    private Panel Panel => this.FindControl<Panel>(nameof(Panel));

    public LargeItem()
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