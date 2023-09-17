using System.Reactive;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;

namespace OpenTracker.Views.Dialogs;

public sealed class ErrorBoxDialog : ReactiveWindow<ErrorBoxDialogVM>
{
    private TextBlock TextBlock => this.FindControl<TextBlock>(nameof(TextBlock));
    private Button OkButton => this.FindControl<Button>(nameof(OkButton));
    
    public ErrorBoxDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.Title,
                    v => v.Title)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Text,
                    v => v.TextBlock.Text)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel,
                    vm => vm.OkCommand,
                    v => v.OkButton)
                .DisposeWith(disposables);
            
            if (ViewModel is null)
            {
                return;
            }
            
            ViewModel!.RequestCloseInteraction.RegisterHandler(interaction =>
                {
                    interaction.SetOutput(Unit.Default);
                    Close(interaction.Input);
                })
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}