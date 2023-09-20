using System.Reactive;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;

namespace OpenTracker.Views.Dialogs;

public sealed class MessageBoxDialog : ReactiveWindow<MessageBoxDialogVM>
{
    public MessageBoxDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(disposables =>
        {
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