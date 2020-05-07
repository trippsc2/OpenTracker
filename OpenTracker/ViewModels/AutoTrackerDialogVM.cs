using Avalonia.Threading;
using OpenTracker.Models;
using OpenTracker.Models.AutotrackerConnectors;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reactive;
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
        public ReactiveCommand<Unit, Unit> ToggleDebugLogCommand { get; }

        public ObservableCollection<(string, LogLevel)> LogMessages { get; }

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

        private string _statusTextColor;
        public string StatusTextColor
        {
            get => _statusTextColor;
            private set => this.RaiseAndSetIfChanged(ref _statusTextColor, value);
        }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            private set => this.RaiseAndSetIfChanged(ref _statusText, value);
        }

        private LogLevel _logLevel;
        public LogLevel LogLevel
        {
            get => _logLevel;
            set => this.RaiseAndSetIfChanged(ref _logLevel, value);
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

            StartCommand = ReactiveCommand.Create(Start, this.WhenAnyValue(x => x.CanStart));
            StopCommand = ReactiveCommand.Create(Stop, this.WhenAnyValue(x => x.CanStop));
            ToggleDebugLogCommand = ReactiveCommand.Create(ToggleLog);

            LogMessages = new ObservableCollection<(string, LogLevel)>();

            LogMessages.CollectionChanged += OnLogMessageChanged;

            _autoTracker.PropertyChanging += OnAutoTrackerChanging;
            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateCanStart();
            UpdateCanStop();
            UpdateStatusText();
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
            
            foreach ((string, LogLevel) message in LogMessages)
            {
                if (message.Item2 >= LogLevel)
                    logText += message.Item1 + "\n";
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
                _autoTracker.Connector.ConnectionStatusChanged -= OnConnectorChanged;
                _autoTracker.Connector.PropertyChanged -= OnConnectorPropertyChanged;
            }
        }

        private void OnAutoTrackerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AutoTracker.Connector) && _autoTracker.Connector != null)
            {
                _autoTracker.Connector.ConnectionStatusChanged += OnConnectorChanged;
                _autoTracker.Connector.PropertyChanged += OnConnectorPropertyChanged;
            }

            UpdateCanStart();
            UpdateCanStop();
            UpdateStatusText();
        }

        private void OnConnectorChanged(object sender, (ConnectionStatus, string) e)
        {
            UpdateCanStart();
            UpdateCanStop();
            UpdateStatusText();
        }

        private void OnConnectorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateCanStart();
            UpdateCanStop();
        }

        private void UpdateCanStart()
        {
            if (_autoTracker.Connector == null)
                CanStart = true;
            else
                CanStart = false;
        }

        private void UpdateCanStop()
        {
            if (_autoTracker.Connector != null)
                CanStop = true;
            else
                CanStop = false;
        }

        private void UpdateStatusText()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (_autoTracker.Connector == null)
                {
                    StatusTextColor = "#f5f5f5";
                    StatusText = "NOT STARTED";
                }
                else if (_autoTracker.Connector.Connected)
                {
                    StatusTextColor = "#00ff00";
                    StatusText = "CONNECTED";
                }
                else
                {
                    StatusTextColor = "#ff3030";
                    StatusText = "ERROR";
                }
            });
        }

        private void Start()
        {
            if (_autoTracker.Connector != null)
                Stop();

            LogMessages.Clear();

            _autoTracker.Start(PushToLog);

            _inGameTimer.Start();
            _memoryCheckTimer.Start();
        }

        private void Stop()
        {
            _inGameTimer.Stop();
            _memoryCheckTimer.Stop();
            _autoTracker.Stop();
        }

        private void ToggleLog()
        {
            VisibleLog = !VisibleLog;
        }

        private void PushToLog(string rawMessage, LogLevel level)
        {
            LogMessages.Add((rawMessage, level));
        }
    }
}