using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views;

public sealed class ModeSettings : ReactiveUserControl<ModeSettingsVM>
{
    private Image ButtonImage => this.FindControl<Image>(nameof(ButtonImage));

    public ModeSettings()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            if (ViewModel is null)
            {
                return;
            }

            ButtonImage.Events()
                .PointerReleased
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.OpenPopupCommand)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}