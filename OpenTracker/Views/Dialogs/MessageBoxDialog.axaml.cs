using System.Reactive;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;

namespace OpenTracker.Views.Dialogs;

public sealed class MessageBoxDialog : ReactiveWindow<MessageBoxDialogVM>
{
    private TextBlock TextBlock => this.FindControl<TextBlock>(nameof(TextBlock));
    private Button YesButton => this.FindControl<Button>(nameof(YesButton));
    private Button NoButton => this.FindControl<Button>(nameof(NoButton));
    
    public MessageBoxDialog()
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
                    vm => vm.YesCommand,
                    v => v.YesButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel,
                    vm => vm.NoCommand,
                    v => v.NoButton)
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