using Avalonia.Threading;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.AutotrackerConnectors;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using WebSocketSharp;

namespace OpenTracker.ViewModels
{
    public class AutoTrackerDialogVM : ViewModelBase, ISave
    {
        private readonly AutoTracker _autoTracker;
        private readonly DispatcherTimer _inGameTimer;
        private readonly DispatcherTimer _memoryCheckTimer;

        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> ResetLogCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isStarting;
        public bool IsStarting =>
            _isStarting.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStopping;
        public bool IsStopping =>
            _isStopping.Value;

        public ObservableCollection<(string, LogLevel)> LogMessages { get; }
        public ObservableCollection<string> LogLevelOptions { get; }

        private string _uriString = "ws://localhost:8080";
        public string URIString
        {
            get => _uriString;
            set => this.RaiseAndSetIfChanged(ref _uriString, value);
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
            set => this.RaiseAndSetIfChanged(ref _logLevel, Enum.Parse<WebSocketSharp.LogLevel>(value));
        }

        private bool _visibleLog;
        public bool VisibleLog
        {
            get => _visibleLog;
            set => this.RaiseAndSetIfChanged(ref _visibleLog, value);
        }

        private string _logText;
        public string LogText
        {
            get => _logText;
            private set => this.RaiseAndSetIfChanged(ref _logText, value);
        }

        public AutoTrackerDialogVM(AutoTracker autoTracker)
        {
            _autoTracker = autoTracker ?? throw new ArgumentNullException(nameof(autoTracker));
            _autoTracker.MessageHandler = PushToLog;

            _inGameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _inGameTimer.Tick += OnInGameTimerTick;

            _memoryCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            _memoryCheckTimer.Tick += OnMemoryCheckTimerTick;

            StartCommand = ReactiveCommand.CreateFromObservable(StartAsync, this.WhenAnyValue(x => x.CanStart));
            StartCommand.IsExecuting.ToProperty(this, x => x.IsStarting, out _isStarting);

            StopCommand = ReactiveCommand.CreateFromObservable(StopAsync, this.WhenAnyValue(x => x.CanStop));
            StopCommand.IsExecuting.ToProperty(this, x => x.IsStopping, out _isStopping);

            ResetLogCommand = ReactiveCommand.Create(ResetLog);

            LogMessages = new ObservableCollection<(string, LogLevel)>();
            LogLevelOptions = new ObservableCollection<string>();

            foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
                LogLevelOptions.Add(level.ToString());

            PropertyChanged += OnPropertyChanged;

            LogMessages.CollectionChanged += OnLogMessageChanged;

            _autoTracker.PropertyChanging += OnAutoTrackerChanging;
            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateCanStart();
            UpdateCanStop();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LogLevel))
                RefreshLog();

            if (e.PropertyName == nameof(URIString))
                UpdateCanStart();
        }    

        private void OnInGameTimerTick(object sender, EventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _autoTracker.InGameCheck();
            });
        }

        private void OnMemoryCheckTimerTick(object sender, EventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _autoTracker.MemoryCheck();
            });
        }

        private void OnLogMessageChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    try
                    {
                        (string, LogLevel) message = ((string, LogLevel))item;

                        if (message.Item2 >= _logLevel)
                            AddLog(message);
                    }
                    finally { }
                }
            }
        }

        private void OnAutoTrackerChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(AutoTracker.Connector) && _autoTracker.Connector != null)
            {
                _autoTracker.Connector.ConnectionStatusChanged -= OnConnectorStatusChanged;
                _autoTracker.Connector.PropertyChanged -= OnConnectorPropertyChanged;
            }
        }

        private void OnAutoTrackerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AutoTracker.Connector) && _autoTracker.Connector != null)
            {
                _autoTracker.Connector.ConnectionStatusChanged += OnConnectorStatusChanged;
                _autoTracker.Connector.PropertyChanged += OnConnectorPropertyChanged;
            }

            UpdateCanStart();
            UpdateCanStop();
        }

        private void OnConnectorStatusChanged(object sender, (ConnectionStatus, string) e)
        {
            UpdateCanStart();
            UpdateCanStop();
            UpdateStatusText(e.Item1);
        }

        private void OnConnectorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateCanStart();
            UpdateCanStop();
        }

        private void UpdateCanStart()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (_autoTracker.Connector == null && CanCreateWebSocketUri())
                    CanStart = true;
                else
                    CanStart = false;
            });
        }

        private void Start()
        {
            if (_autoTracker.Connector != null)
                Stop();

            ResetLog();

            _autoTracker.Start(URIString);

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _inGameTimer.Start();
                _memoryCheckTimer.Start();
            });
        }

        private IObservable<Unit> StartAsync()
        {
            return Observable.Start(() => { Start(); });
        }

        private void UpdateCanStop()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (_autoTracker.Connector != null)
                    CanStop = true;
                else
                    CanStop = false;
            });
        }

        private void Stop()
        {
            _inGameTimer.Stop();
            _memoryCheckTimer.Stop();
            _autoTracker.Stop();
        }

        private IObservable<Unit> StopAsync()
        {
            return Observable.Start(() => { Stop(); });
        }

        private void UpdateStatusText(ConnectionStatus status)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                switch (status)
                {
                    case ConnectionStatus.NotConnected:
                        StatusTextColor = "#ffffff";
                        StatusText = "NOT CONNECTED";
                        break;
                    case ConnectionStatus.Connecting:
                        StatusTextColor = "#ffff00";
                        StatusText = "CONNECTING";
                        break;
                    case ConnectionStatus.Open:
                        StatusTextColor = "#00ff00";
                        StatusText = "CONNECTED";
                        break;
                    case ConnectionStatus.Error:
                        StatusTextColor = "#ff3030";
                        StatusText = "ERROR";
                        break;
                }
            });
        }

        private void AddLog((string, LogLevel) message)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText += message.Item2.ToString().ToUpper(CultureInfo.CurrentCulture) +
                    ": " + message.Item1 + "\n";
            });
        }

        private void ResetLog()
        {
            LogMessages.Clear();
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText = "";
            });
        }

        private void RefreshLog()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText = "";
            });

            foreach ((string, LogLevel) message in LogMessages)
            {
                if (message.Item2 >= _logLevel)
                    AddLog(message);
            }
        }

        private void PushToLog(string rawMessage, LogLevel level)
        {
            LogMessages.Add((rawMessage, level));
        }

        private bool CanCreateWebSocketUri()
        {
            var uri = URIString.ToUri();

            if (uri == null)
                return false;

            if (!uri.IsAbsoluteUri)
                return false;

            var schm = uri.Scheme;

            if (!(schm == "ws" || schm == "wss"))
                return false;

            var port = uri.Port;

            if (port == 0)
                return false;

            if (uri.Fragment.Length > 0)
                return false;

            return true;
        }

        public void Save(string path)
        {
            using StreamWriter file = new StreamWriter(path);
            foreach ((string, LogLevel) message in LogMessages)
                file.WriteLine(message.Item2.ToString().ToUpper(CultureInfo.CurrentCulture) + ": " + message.Item1);
        }
    }
}