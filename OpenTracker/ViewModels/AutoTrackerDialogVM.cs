using Avalonia.Threading;
using OpenTracker.Models;
using OpenTracker.Models.AutotrackerConnectors;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using WebSocketSharp;

namespace OpenTracker.ViewModels
{
    public class AutoTrackerDialogVM : ViewModelBase
    {
        private readonly AutoTracker _autoTracker;
        private readonly DispatcherTimer _inGameTimer;
        private readonly DispatcherTimer _memoryCheckTimer;

        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isStarting;
        public bool IsStarting => _isStarting.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStopping;
        public bool IsStopping => _isStopping.Value;

        public ObservableCollection<(string, WebSocketSharp.LogLevel)> LogMessages { get; }
        public ObservableCollection<string> LogLevelOptions
        {
            get
            {
                ObservableCollection<string> options = new ObservableCollection<string>();

                foreach (WebSocketSharp.LogLevel level in Enum.GetValues(typeof(WebSocketSharp.LogLevel)))
                    options.Add(level.ToString());

                return options;
            }
        }

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

        private WebSocketSharp.LogLevel _logLevel = WebSocketSharp.LogLevel.Info;
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
            _autoTracker = autoTracker;

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

            LogMessages = new ObservableCollection<(string, WebSocketSharp.LogLevel)>();

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
                LogMessages.Clear();

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
            string logText = "";

            foreach ((string, WebSocketSharp.LogLevel) message in LogMessages)
            {
                logText += string.Format("{0}: " + message.Item1 + "\n",
                    message.Item2.ToString().ToUpper());
            }

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText = logText;
            });
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

            LogMessages.Clear();

            _autoTracker.Start(URIString, PushToLog);

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

        private void PushToLog(string rawMessage, WebSocketSharp.LogLevel level)
        {
            if (_logLevel <= level)
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
    }
}