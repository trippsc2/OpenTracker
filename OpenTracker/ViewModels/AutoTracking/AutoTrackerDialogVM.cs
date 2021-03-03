using Avalonia.Threading;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Utils.Dialog;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.AutoTracking
{
    /// <summary>
    /// This class contains the auto-tracker dialog window ViewModel.
    /// </summary>
    public class AutoTrackerDialogVM : DialogViewModelBase, IAutoTrackerDialogVM
    {
        private readonly IAutoTracker _autoTracker;

        private readonly DispatcherTimer _memoryCheckTimer;
        private int _tickCount;

        public bool UriTextBoxEnabled =>
            _autoTracker.CanConnect();

        private string _uriString = "ws://localhost:8080";
        public string UriString
        {
            get => _uriString;
            set => this.RaiseAndSetIfChanged(ref _uriString, value);
        }

        public bool DevicesComboBoxEnabled =>
            _autoTracker.CanStart() && Devices.Count > 0;

        public List<string> Devices =>
            _autoTracker.Devices;

        private string? _device;
        public string? Device
        {
            get => _device;
            set => this.RaiseAndSetIfChanged(ref _device, value);
        }

        public bool RaceIllegalTracking =>
            _autoTracker.RaceIllegalTracking;

        private bool _canConnect;
        public bool CanConnect
        {
            get => _canConnect;
            private set => this.RaiseAndSetIfChanged(ref _canConnect, value);
        }

        private bool _canGetDevices;
        public bool CanGetDevices
        {
            get => _canGetDevices;
            private set => this.RaiseAndSetIfChanged(ref _canGetDevices, value);
        }

        private bool _canDisconnect;
        public bool CanDisconnect
        {
            get => _canDisconnect;
            private set => this.RaiseAndSetIfChanged(ref _canDisconnect, value);
        }

        private bool _canStart;
        public bool CanStart
        {
            get => _canStart;
            private set => this.RaiseAndSetIfChanged(ref _canStart, value);
        }

        public IAutoTrackerLogVM Log { get; }
        public IAutoTrackerStatusVM Status { get; }

        public ReactiveCommand<Unit, Unit> ConnectCommand { get; }
        public ReactiveCommand<Unit, Unit> GetDevicesCommand { get; }
        public ReactiveCommand<Unit, Unit> DisconnectCommand { get; }
        public ReactiveCommand<Unit, Unit> StartCommand { get; }

        public ReactiveCommand<Unit, Unit> ToggleRaceIllegalTrackingCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isConnecting;
        public bool IsConnecting =>
            _isConnecting.Value;

        private readonly ObservableAsPropertyHelper<bool> _isGettingDevices;
        public bool IsGettingDevices =>
            _isGettingDevices.Value;

        private readonly ObservableAsPropertyHelper<bool> _isDisconnecting;
        public bool IsDisconnecting =>
            _isDisconnecting.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStarting;
        public bool IsStarting =>
            _isStarting.Value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTracker">
        /// The autotracker data.
        /// </param>
        /// <param name="logService">
        /// The autotracker log service.
        /// </param>
        /// <param name="status">
        /// The autotracker status control ViewModel.
        /// </param>
        /// <param name="log">
        /// The autotracker log control ViewModel.
        /// </param>
        public AutoTrackerDialogVM(
            IAutoTracker autoTracker, IAutoTrackerLogService logService,
            IAutoTrackerStatusVM status, IAutoTrackerLogVM log)
        {
            _autoTracker = autoTracker;

            Status = status;
            Log = log;

            _memoryCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            _memoryCheckTimer.Tick += OnMemoryCheckTimerTick;

            ConnectCommand = ReactiveCommand.CreateFromTask(
                Connect, this.WhenAnyValue(x => x.CanConnect));
            ConnectCommand.IsExecuting.ToProperty(
                this, x => x.IsConnecting, out _isConnecting);
            ConnectCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); });

            GetDevicesCommand = ReactiveCommand.CreateFromTask(
                GetDevices, this.WhenAnyValue(x => x.CanGetDevices));
            GetDevicesCommand.IsExecuting.ToProperty(
                this, x => x.IsGettingDevices, out _isGettingDevices);
            GetDevicesCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); });

            DisconnectCommand = ReactiveCommand.CreateFromTask(
                Stop, this.WhenAnyValue(x => x.CanDisconnect));
            DisconnectCommand.IsExecuting.ToProperty(
                this, x => x.IsDisconnecting, out _isDisconnecting);
            DisconnectCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); });

            StartCommand = ReactiveCommand.CreateFromTask(
                Start, this.WhenAnyValue(x => x.CanStart));
            StartCommand.IsExecuting.ToProperty(
                this, x => x.IsStarting, out _isStarting);
            StartCommand.ThrownExceptions
                .Subscribe(ex => { logService.Log(LogLevel.Fatal, ex.Message); });

            ToggleRaceIllegalTrackingCommand = ReactiveCommand.Create(ToggleRaceIllegalTracking);

            PropertyChanged += OnPropertyChanged;
            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateCommandCanExecuteProperties();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on itself.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UriString))
            {
                UpdateCanConnect();
            }

            if (e.PropertyName == nameof(Device))
            {
                UpdateCanStart();
            }
        }

        /// <summary>
        /// Subscribes to the dispatcher timer Tick event.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The arguments of the Tick event.
        /// </param>
        private void OnMemoryCheckTimerTick(object? sender, EventArgs e)
        {
            _tickCount++;

            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await _autoTracker.InGameCheck();

                if (_tickCount == 5)
                {
                    _tickCount = 0;
                    await _autoTracker.MemoryCheck();
                }
            });
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IAutoTracker interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTracker.RaceIllegalTracking))
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    this.RaisePropertyChanged(nameof(RaceIllegalTracking));
                });
            }

            if (e.PropertyName == nameof(IAutoTracker.Devices))
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    this.RaisePropertyChanged(nameof(Devices));
                    this.RaisePropertyChanged(nameof(DevicesComboBoxEnabled));
                });
            }

            if (e.PropertyName == nameof(IAutoTracker.Status))
            {
                UpdateCommandCanExecuteProperties();

                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (_autoTracker.Status != ConnectionStatus.Connected &&
                        _memoryCheckTimer.IsEnabled)
                    {
                        _memoryCheckTimer.Stop();
                    }

                    this.RaisePropertyChanged(nameof(UriTextBoxEnabled));
                    this.RaisePropertyChanged(nameof(DevicesComboBoxEnabled));
                });
            }
        }

        /// <summary>
        /// Updates the "can execute" properties for all commands.
        /// </summary>
        private void UpdateCommandCanExecuteProperties()
        {
            UpdateCanConnect();
            UpdateCanGetDevices();
            UpdateCanDisconnect();
            UpdateCanStart();
        }

        /// <summary>
        /// Updates the CanConnect property.
        /// </summary>
        private void UpdateCanConnect()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanConnect = CanCreateWebSocketUri() && _autoTracker.CanConnect();
            });
        }

        /// <summary>
        /// Updates the CanGetDevices property.
        /// </summary>
        private void UpdateCanGetDevices()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanGetDevices = _autoTracker.CanGetDevices();
            });
        }

        /// <summary>
        /// Updates the CanGetDisconnect property.
        /// </summary>
        private void UpdateCanDisconnect()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanDisconnect = _autoTracker.CanDisconnect();
            });
        }

        /// <summary>
        /// Updates the CanStart property.
        /// </summary>
        private void UpdateCanStart()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanStart = !(Device is null) && Device != string.Empty && _autoTracker.CanStart();
            });
        }

        /// <summary>
        /// Connects to the web socket at the specified URI string.
        /// </summary>
        private async Task Connect()
        {
            await _autoTracker.Connect(UriString);
        }

        /// <summary>
        /// Sets the value of the Devices property to an observable collection of strings representing
        /// the devices returns by the SNES connector.
        /// </summary>
        /// <returns>
        /// An observable representing the result of the command.
        /// </returns>
        private async Task GetDevices()
        {
            await _autoTracker.GetDevices();
        }

        /// <summary>
        /// Stops auto-tracking.
        /// </summary>
        private async Task Stop()
        {
            _memoryCheckTimer.Stop();
            await _autoTracker.Disconnect();
        }

        /// <summary>
        /// Starts auto-tracking.
        /// </summary>
        private async Task Start()
        {    
            if (Device is null)
            {
                throw new NullReferenceException();
            }
            
            await _autoTracker.Start(Device);
            _memoryCheckTimer.Start();
        }

        /// <summary>
        /// Toggles the race illegal tracking flag.
        /// </summary>
        private void ToggleRaceIllegalTracking()
        {
            _autoTracker.RaceIllegalTracking = !_autoTracker.RaceIllegalTracking;
        }

        /// <summary>
        /// Returns whether the UriString property value is a valid URI to be accepted by the web
        /// socket library.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the UriString property value can be accepted by the
        /// web socket library.
        /// </returns>
        private bool CanCreateWebSocketUri()
        {
            if (!Uri.IsWellFormedUriString(UriString, UriKind.Absolute))
            {
                return false;
            }

            var uri = new Uri(UriString);

            if (uri == null)
            {
                return false;
            }

            if (!uri.IsAbsoluteUri)
            {
                return false;
            }

            var scheme = uri.Scheme;

            if (!(scheme == "ws" || scheme == "wss"))
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
}
