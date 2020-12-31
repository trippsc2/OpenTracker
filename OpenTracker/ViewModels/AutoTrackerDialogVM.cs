using Avalonia.Threading;
using OpenTracker.Interfaces;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using WebSocketSharp;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel class for the autotracker dialog window.
    /// </summary>
    public class AutoTrackerDialogVM : ViewModelBase, ISaveData
    {
        private readonly DispatcherTimer _memoryCheckTimer;
        private int _tickCount;

        public string CurrentFilePath =>
            null;

        public ObservableCollection<(LogLevel, string)> LogMessages { get; } =
            new ObservableCollection<(LogLevel, string)>();
        public ObservableCollection<string> LogLevelOptions { get; } =
            new ObservableCollection<string>();
        public ObservableStringBuilder LogText { get; } =
            new ObservableStringBuilder();

        private string _uriString = "ws://localhost:8080";
        public string UriString
        {
            get => _uriString;
            set => this.RaiseAndSetIfChanged(ref _uriString, value);
        }

        private bool _canGetDevices;
        public bool CanGetDevices
        {
            get => _canGetDevices;
            private set => this.RaiseAndSetIfChanged(ref _canGetDevices, value);
        }

        private ObservableCollection<string> _devices;
        public ObservableCollection<string> Devices
        {
            get => _devices;
            private set => this.RaiseAndSetIfChanged(ref _devices, value);
        }

        private string _device;
        public string Device
        {
            get => _device;
            set => this.RaiseAndSetIfChanged(ref _device, value);
        }

        private bool _raceIllegalTracking;
        public bool RaceIllegalTracking
        {
            get => _raceIllegalTracking;
            private set => this.RaiseAndSetIfChanged(ref _raceIllegalTracking, value);
        }

        private bool _canStart;
        public bool CanStart
        {
            get => _canStart;
            private set => this.RaiseAndSetIfChanged(ref _canStart, value);
        }

        private bool _canStop;
        public bool CanStop
        {
            get => _canStop;
            private set => this.RaiseAndSetIfChanged(ref _canStop, value);
        }

        private string _statusTextColor = "#ffffff";
        public string StatusTextColor
        {
            get => _statusTextColor;
            private set => this.RaiseAndSetIfChanged(ref _statusTextColor, value);
        }

        private string _statusText = "NOT CONNECTED";
        public string StatusText
        {
            get => _statusText;
            private set => this.RaiseAndSetIfChanged(ref _statusText, value);
        }

        private LogLevel _logLevel = WebSocketSharp.LogLevel.Info;
        public string LogLevel
        {
            get => _logLevel.ToString();
            set => this.RaiseAndSetIfChanged(ref _logLevel, Enum.Parse<LogLevel>(value));
        }

        private bool _visibleLog;
        public bool VisibleLog
        {
            get => _visibleLog;
            set => this.RaiseAndSetIfChanged(ref _visibleLog, value);
        }

        public ReactiveCommand<Unit, Unit> RaceIllegalTrackingCommand { get; }
        public ReactiveCommand<Unit, Unit> GetDevicesCommand { get; }
        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> ResetLogCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isGettingDevices;
        public bool IsGettingDevices =>
            _isGettingDevices.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStarting;
        public bool IsStarting =>
            _isStarting.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStopping;
        public bool IsStopping =>
            _isStopping.Value;

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoTrackerDialogVM()
        {
            AutoTracker.Instance.LogHandler = HandleLog;

            foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
            {
                LogLevelOptions.Add(level.ToString());
            }

            _memoryCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _memoryCheckTimer.Tick += OnMemoryCheckTimerTick;

            RaceIllegalTrackingCommand = ReactiveCommand.Create(ToggleRaceIllegalTracking);

            GetDevicesCommand = ReactiveCommand.CreateFromObservable(
                GetDevicesAsync, this.WhenAnyValue(x => x.CanGetDevices));
            GetDevicesCommand.IsExecuting.ToProperty(
                this, x => x.IsGettingDevices, out _isGettingDevices);

            StartCommand = ReactiveCommand.CreateFromObservable(
                StartAsync, this.WhenAnyValue(x => x.CanStart));
            StartCommand.IsExecuting.ToProperty(
                this, x => x.IsStarting, out _isStarting);

            StopCommand = ReactiveCommand.CreateFromObservable(
                StopAsync, this.WhenAnyValue(x => x.CanStop));
            StopCommand.IsExecuting.ToProperty(
                this, x => x.IsStopping, out _isStopping);

            ResetLogCommand = ReactiveCommand.Create(ResetLog);

            PropertyChanged += OnPropertyChanged;
            AutoTracker.Instance.PropertyChanged += OnAutoTrackerChanged;
            AutoTracker.Instance.SNESConnector.PropertyChanged += OnConnectorChanged;
            LogMessages.CollectionChanged += OnLogMessageChanged;

            UpdateCanGetDevices();
            UpdateCanStart();
            UpdateCanStop();
            UpdateStatusText();
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
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LogLevel))
            {
                RefreshLog();
            }

            if (e.PropertyName == nameof(UriString))
            {
                UpdateCanGetDevices();
            }

            if (e.PropertyName == nameof(Device))
            {
                UpdateCanStart();
            }
        }    

        /// <summary>
        /// Subscribes to the dispatcher timer and triggers the appropriate memory checks on tick.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void OnMemoryCheckTimerTick(object sender, EventArgs e)
        {
            _tickCount++;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                AutoTracker.Instance.InGameCheck();

                if (_tickCount == 3)
                {
                    _tickCount = 0;

                    foreach (MemorySegmentType segment in Enum.GetValues(typeof(MemorySegmentType)))
                    {
                        AutoTracker.Instance.MemoryCheck(segment);
                    }
                }
            });
        }

        /// <summary>
        /// Subscribes to the ObservableCollection of log messages CollectionChanged event.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private void OnLogMessageChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    (LogLevel, string) message = ((LogLevel, string))item;
                    
                    if (message.Item1 >= _logLevel)
                    {
                        AddLog(message);
                    }
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISNESConnector interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnConnectorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateStatusText();
            UpdateCanGetDevices();
            UpdateCanStart();
            UpdateCanStop();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AutoTracker class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AutoTracker.RaceIllegalTracking))
            {
                UpdateStatusText();
                RaceIllegalTracking = AutoTracker.Instance.RaceIllegalTracking;
            }
        }

        /// <summary>
        /// Updates the CanGetDevices property with a value representing whether the SNES connector
        /// can be queries for a device list.
        /// </summary>
        private void UpdateCanGetDevices()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanGetDevices = CanCreateWebSocketUri() &&
                    (AutoTracker.Instance.SNESConnector.Socket == null ||
                    AutoTracker.Instance.SNESConnector.Status == ConnectionStatus.Connected);
            });
        }

        /// <summary>
        /// Sets the value of the Devices property to an observable collection of strings representing
        /// the devices returns by the SNES connector.
        /// </summary>
        private void GetDevices()
        {
            if (AutoTracker.Instance.SNESConnector.Uri != UriString)
            {
                AutoTracker.Instance.SNESConnector.Uri = UriString;
            }

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                try
                {
                    Devices = new ObservableCollection<string>(AutoTracker.Instance.GetDevices());
                }
                catch (ArgumentNullException)
                { }
            });
        }

        /// <summary>
        /// Returns an observable representing the result of the GetDevices method.
        /// </summary>
        /// <returns>
        /// An observable representing the result of the GetDevices method.
        /// </returns>
        private IObservable<Unit> GetDevicesAsync()
        {
            return Observable.Start(() => { GetDevices(); });
        }

        /// <summary>
        /// Updates the CanStart property with a value representing whether autotracking
        /// can be started.
        /// </summary>
        private void UpdateCanStart()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanStart = AutoTracker.Instance.SNESConnector.Socket != null &&
                    AutoTracker.Instance.SNESConnector.Status != ConnectionStatus.Error &&
                    Device != null && AutoTracker.Instance.SNESConnector.Device != Device;
            });
        }

        /// <summary>
        /// Starts autotracking.
        /// </summary>
        private void Start()
        {
            AutoTracker.Instance.SNESConnector.Device = Device;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _memoryCheckTimer.Start();
            });
        }

        /// <summary>
        /// Returns an observable representing the result of the Start method.
        /// </summary>
        /// <returns>
        /// An observable representing the result of the Start method.
        /// </returns>
        private IObservable<Unit> StartAsync()
        {
            return Observable.Start(() => { Start(); });
        }

        /// <summary>
        /// Updates the CanStop property with a value representing whether autotracking
        /// can be stopped.
        /// </summary>
        private void UpdateCanStop()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanStop = AutoTracker.Instance.SNESConnector.Socket != null;
            });
        }

        /// <summary>
        /// Stops autotracking.
        /// </summary>
        private void Stop()
        {
            _memoryCheckTimer.Stop();
            AutoTracker.Instance.Stop();
        }

        /// <summary>
        /// Returns an observable representing the result of the Stop method.
        /// </summary>
        /// <returns>
        /// An observable representing the result of the Stop method.
        /// </returns>
        private IObservable<Unit> StopAsync()
        {
            return Observable.Start(() => { Stop(); });
        }

        /// <summary>
        /// Updates the status text and text color based on the status of the SNES connector.
        /// </summary>
        private void UpdateStatusText()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                switch (AutoTracker.Instance.SNESConnector.Status)
                {
                    case ConnectionStatus.NotConnected:
                        {
                            StatusTextColor = "#ffffff";
                            StatusText = "NOT CONNECTED";
                        }
                        break;
                    case ConnectionStatus.SelectDevice:
                        {
                            StatusTextColor = "#ffffff";
                            StatusText = "SELECT DEVICE";
                        }
                        break;
                    case ConnectionStatus.Connecting:
                        {
                            StatusTextColor = "#ffff00";
                            StatusText = "CONNECTING";
                        }
                        break;
                    case ConnectionStatus.Attaching:
                        {
                            StatusTextColor = "#ffff00";
                            StatusText = "ATTACHING";
                        }
                        break;
                    case ConnectionStatus.Connected:
                        {
                            StatusTextColor = "#00ff00";
                            var sb = new StringBuilder();
                            sb.Append("CONNECTED (");

                            if (AutoTracker.Instance.RaceIllegalTracking)
                            {
                                sb.Append("RACE ILLEGAL)");
                            }
                            else
                            {
                                sb.Append("RACE LEGAL)");
                            }

                            StatusText = sb.ToString();
                        }
                        break;
                    case ConnectionStatus.Error:
                        {
                            StatusTextColor = "#ff3030";
                            StatusText = "ERROR";
                        }
                        break;
                }
            });
        }

        /// <summary>
        /// Adds a log message to the log view.
        /// </summary>
        /// <param name="message">
        /// The log message.
        /// </param>
        private void AddLog((LogLevel, string) message)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.AppendLine($"{message.Item1.ToString().ToUpper(CultureInfo.CurrentCulture)}:" +
                    $" {message.Item2}");
            });
        }

        /// <summary>
        /// Toggles the race illegal tracking flag.
        /// </summary>
        private void ToggleRaceIllegalTracking()
        {
            AutoTracker.Instance.RaceIllegalTracking = !AutoTracker.Instance.RaceIllegalTracking;
        }

        /// <summary>
        /// Resets the log collection and log view to empty.
        /// </summary>
        private void ResetLog()
        {
            LogMessages.Clear();
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.Clear();
            });
        }

        /// <summary>
        /// Regenerates the log view from the log messages.
        /// </summary>
        private void RefreshLog()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.Clear();
            });

            foreach ((LogLevel, string) message in LogMessages)
            {
                if (message.Item1 >= _logLevel)
                {
                    AddLog(message);
                }
            }
        }

        /// <summary>
        /// Pushes a raw log entry to the log observable collection.
        /// </summary>
        /// <param name="level">
        /// The logging level of the log entry.
        /// </param>
        /// <param name="rawMessage">
        /// The raw message string of the log entry.
        /// </param>
        private void HandleLog(LogLevel level, string rawMessage)
        {
            LogMessages.Add((level, rawMessage));
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

            var schm = uri.Scheme;

            if (!(schm == "ws" || schm == "wss"))
            {
                return false;
            }

            var port = uri.Port;

            if (port == 0)
            {
                return false;
            }

            if (uri.Fragment.Length > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves the log to a text file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The path of the text file.
        /// </param>
        public void Save(string path = null)
        {
            if (path == null)
            {
                return;
            }

            using StreamWriter file = new StreamWriter(path);

            foreach ((LogLevel, string) message in LogMessages)
            {
                file.WriteLine($"{message.Item1.ToString().ToUpper(CultureInfo.CurrentCulture)}: " +
                    message.Item2);
            }
        }
    }
}