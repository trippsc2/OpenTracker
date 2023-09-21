using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Models.Logging;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.AutoTracking;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Dialogs;

/// <summary>
/// This class contains the auto-tracker dialog window ViewModel.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class AutoTrackerDialogVM : ViewModel
{

    private readonly DispatcherTimer _memoryCheckTimer;
    private int _tickCount;
    
    private IAutoTracker AutoTracker { get; }
    
    public AutoTrackerStatusVM Status { get; }

    [ObservableAsProperty]
    public bool UriTextBoxEnabled { get; }
    [ObservableAsProperty]
    public bool DevicesComboBoxEnabled { get; }
    [ObservableAsProperty]
    public IList<string> Devices { get; } = new List<string>();
    [ObservableAsProperty]
    public bool RaceIllegalTracking { get; }
    [Reactive]
    public string UriString { get; set; } = "ws://localhost:8080";
    [Reactive]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Device { get; set; }

    [Reactive]
    public ReactiveCommand<Unit, Unit> ConnectCommand { get; private set; } = default!;
    [Reactive]
    public ReactiveCommand<Unit, Unit> GetDevicesCommand { get; private set; } = default!;
    [Reactive]
    public ReactiveCommand<Unit, Unit> DisconnectCommand { get; private set; } = default!;
    [Reactive]
    public ReactiveCommand<Unit, Unit> StartCommand { get; private set; } = default!;

    [Reactive]
    public ReactiveCommand<Unit, Unit> ToggleRaceIllegalTrackingCommand { get; private set; } = default!;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="autoTracker">
    ///     The auto-tracker data.
    /// </param>
    /// <param name="logService">
    ///     The auto-tracker log service.
    /// </param>
    /// <param name="status">
    ///     The auto-tracker status control ViewModel.
    /// </param>
    public AutoTrackerDialogVM(IAutoTracker autoTracker, IAutoTrackerLogService logService, AutoTrackerStatusVM status)
    {
        AutoTracker = autoTracker;

        Status = status;

        _memoryCheckTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
        _memoryCheckTimer.Tick += OnMemoryCheckTimerTick;

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.AutoTracker.Status)
                .Select(_ => AutoTracker.CanConnect())
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.UriTextBoxEnabled)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.AutoTracker.Devices,
                    x => x.AutoTracker.Status,
                    (_, _) => AutoTracker.CanStart() && Devices.Count > 0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.DevicesComboBoxEnabled)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.AutoTracker.Devices)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Devices)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.AutoTracker.RaceIllegalTracking)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.RaceIllegalTracking)
                .DisposeWith(disposables);

            var canConnect = this
                .WhenAnyValue(
                    x => x.UriString,
                    x => x.AutoTracker.Status,
                    (_, _) => CanCreateWebSocketUri() && AutoTracker.CanConnect())
                .ObserveOn(RxApp.MainThreadScheduler);
            var canGetDevices = this
                .WhenAnyValue(x => x.AutoTracker.Status)
                .Select(_ => AutoTracker.CanGetDevices())
                .ObserveOn(RxApp.MainThreadScheduler);
            var canDisconnect = this
                .WhenAnyValue(x => x.AutoTracker.Status)
                .Select(_ => AutoTracker.CanDisconnect())
                .ObserveOn(RxApp.MainThreadScheduler);
            var canStart = this
                .WhenAnyValue(
                    x => x.Device,
                    x => x.AutoTracker.Status,
                    (device, _) => !string.IsNullOrEmpty(device) && AutoTracker.CanStart())
                .ObserveOn(RxApp.MainThreadScheduler);
            
            ConnectCommand = ReactiveCommand
                .CreateFromTask(ConnectAsync, canConnect)
                .DisposeWith(disposables);
            ConnectCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); })
                .DisposeWith(disposables);
            GetDevicesCommand = ReactiveCommand
                .CreateFromTask(GetDevicesAsync, canGetDevices)
                .DisposeWith(disposables);
            GetDevicesCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); })
                .DisposeWith(disposables);
            DisconnectCommand = ReactiveCommand
                .CreateFromTask(StopAsync, canDisconnect)
                .DisposeWith(disposables);
            DisconnectCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); })
                .DisposeWith(disposables);
            StartCommand = ReactiveCommand
                .CreateFromTask(StartAsync, canStart)
                .DisposeWith(disposables);
            StartCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); })
                .DisposeWith(disposables);

            ToggleRaceIllegalTrackingCommand = ReactiveCommand
                .Create(ToggleRaceIllegalTracking)
                .DisposeWith(disposables);

        });
    }

    /// <summary>
    /// Subscribes to the dispatcher timer Tick event.
    /// </summary>
    /// <param name="sender">
    /// The event sender.
    /// </param>
    /// <param name="e">
    /// The Tick event args.
    /// </param>
    private async void OnMemoryCheckTimerTick(object? sender, EventArgs e)
    {
        _tickCount++;

        await AutoTracker.InGameCheck();

        if (_tickCount != 5)
        {
            return;
        }
            
        _tickCount = 0;
        await AutoTracker.MemoryCheck();
    }

    private async Task ConnectAsync()
    {
        await AutoTracker.Connect(UriString);
    }

    private async Task GetDevicesAsync()
    {
        await AutoTracker.GetDevices();
    }

    private async Task StopAsync()
    {
        _memoryCheckTimer.Stop();
        await AutoTracker.Disconnect();
    }

    private async Task StartAsync()
    {    
        await AutoTracker.Start(Device!);
        _memoryCheckTimer.Start();
    }

    private void ToggleRaceIllegalTracking()
    {
        AutoTracker.RaceIllegalTracking = !AutoTracker.RaceIllegalTracking;
    }

    private bool CanCreateWebSocketUri()
    {
        if (!Uri.IsWellFormedUriString(UriString, UriKind.Absolute))
        {
            return false;
        }

        var uri = new Uri(UriString);

        if (!uri.IsAbsoluteUri)
        {
            return false;
        }

        var scheme = uri.Scheme;

        if (scheme is not ("ws" or "wss"))
        {
            return false;
        }

        var port = uri.Port;

        if (port == 0)
        {
            return false;
        }

        return uri.Fragment.Length <= 0;
    }
}