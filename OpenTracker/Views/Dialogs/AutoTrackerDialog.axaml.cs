using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;

namespace OpenTracker.Views.Dialogs;

public sealed class AutoTrackerDialog : ReactiveWindow<AutoTrackerDialogVM>
{
    private TextBox UriTextBox => this.FindControl<TextBox>(nameof(UriTextBox));
    private Button ConnectButton => this.FindControl<Button>(nameof(ConnectButton));
    private Button GetDevicesButton => this.FindControl<Button>(nameof(GetDevicesButton));
    private Button DisconnectButton => this.FindControl<Button>(nameof(DisconnectButton));
    private ComboBox DevicesComboBox => this.FindControl<ComboBox>(nameof(DevicesComboBox));
    private CheckBox RaceIllegalTrackingCheckBox => this.FindControl<CheckBox>(nameof(RaceIllegalTrackingCheckBox));
    private Button StartButton => this.FindControl<Button>(nameof(StartButton));
    private ContentControl StatusContentControl => this.FindControl<ContentControl>(nameof(StatusContentControl));
    
    public AutoTrackerDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel,
                    vm => vm.UriString,
                    v => v.UriTextBox.Text)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.UriTextBoxEnabled,
                    v => v.UriTextBox.IsEnabled)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Devices,
                    v => v.DevicesComboBox.Items)
                .DisposeWith(disposables);
            this.Bind(ViewModel,
                    vm => vm.Device,
                    v => v.DevicesComboBox.SelectedItem)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.DevicesComboBoxEnabled,
                    v => v.DevicesComboBox.IsEnabled)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.RaceIllegalTracking,
                    v => v.RaceIllegalTrackingCheckBox.IsChecked)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Status,
                    v => v.StatusContentControl.Content)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel,
                    vm => vm.ConnectCommand,
                    v => v.ConnectButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel,
                    vm => vm.GetDevicesCommand,
                    v => v.GetDevicesButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel,
                    vm => vm.DisconnectCommand,
                    v => v.DisconnectButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel,
                    vm => vm.ToggleRaceIllegalTrackingCommand,
                    v => v.RaceIllegalTrackingCheckBox)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel,
                    vm => vm.StartCommand,
                    v => v.StartButton)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}