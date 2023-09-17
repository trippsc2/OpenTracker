using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels;
using OpenTracker.ViewModels.AutoTracking;
using OpenTracker.ViewModels.Dialogs;
using OpenTracker.Views.Dialogs;
using ReactiveUI;
using Splat;

namespace OpenTracker.Views.AutoTracking;

public sealed class AutoTrackerLog : ReactiveUserControl<AutoTrackerLogVM>
{
    private ComboBox LogLevelComboBox => this.FindControl<ComboBox>("LogLevelComboBox");
    private ToggleButton ShowLogToggleButton => this.FindControl<ToggleButton>("ShowLogToggleButton");
    private Button ResetLogButton => this.FindControl<Button>("ResetLogButton");
    private Button SaveLogButton => this.FindControl<Button>("SaveLogButton");
    private Border LogBorder => this.FindControl<Border>("LogBorder");
    
    public AutoTrackerLog()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.LogLevelOptions,
                    v => v.LogLevelComboBox.Items)
                .DisposeWith(disposables);
            this.Bind(ViewModel,
                    vm => vm.LogVisible,
                    v => v.ShowLogToggleButton.IsChecked)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.LogVisible,
                    v => v.LogBorder.IsVisible)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel,
                    vm => vm.ResetLogCommand,
                    v => v.ResetLogButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel,
                    vm => vm.SaveLogCommand,
                    v => v.SaveLogButton)
                .DisposeWith(disposables);

            if (ViewModel is null)
            {
                return;
            }
            
            ViewModel!.OpenErrorBoxInteraction.RegisterHandler(async interaction =>
            {
                await OpenErrorBoxAsync(interaction.Input).ConfigureAwait(true);
                interaction.SetOutput(Unit.Default);
            });
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private static async Task OpenErrorBoxAsync(ErrorBoxDialogVM viewModel)
    {
        var mainWindow = Locator.Current.GetService<IViewFor<MainWindowVM>>()! as MainWindow;
        var view = Locator.Current.GetService<IViewFor<ErrorBoxDialogVM>>() as ErrorBoxDialog;
        
        view!.ViewModel = viewModel;
        
        await view.ShowDialog(mainWindow).ConfigureAwait(true);
    }
}