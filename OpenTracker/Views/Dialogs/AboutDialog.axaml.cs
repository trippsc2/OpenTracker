using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;

namespace OpenTracker.Views.Dialogs;

public sealed class AboutDialog : ReactiveWindow<AboutDialogVM>
{
    private TextBlock VersionText => this.FindControl<TextBlock>(nameof(VersionText));
    
    public AboutDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.Version,
                    v => v.VersionText.Text)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}