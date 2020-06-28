using Avalonia.Threading;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
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

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model class for the autotracker dialog window.
    /// </summary>
    public class AutoTrackerDialogVM : ViewModelBase, ISave
    {
        private readonly AutoTracker _autoTracker;
        private readonly DispatcherTimer _memoryCheckTimer;
        private int _tickCount;

        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> ResetLogCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isStarting;
        public bool IsStarting =>
            _isStarting.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStopping;
        public bool IsStopping =>
            _isStopping.Value;

        public ObservableCollection<(LogLevel, string)> LogMessages { get; }
        public ObservableCollection<string> LogLevelOptions { get; }
        public ObservableStringBuilder LogText { get; }

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

        private LogLevel _logLevel = Models.Enums.LogLevel.Info;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTracker">
        /// The autotracker model class.
        /// </param>
        public AutoTrackerDialogVM(AutoTracker autoTracker)
        {
            _autoTracker = autoTracker ?? throw new ArgumentNullException(nameof(autoTracker));
            _autoTracker.MessageHandler = PushToLog;

            _memoryCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _memoryCheckTimer.Tick += OnMemoryCheckTimerTick;

            StartCommand = ReactiveCommand.CreateFromObservable(StartAsync, this.WhenAnyValue(x => x.CanStart));
            StartCommand.IsExecuting.ToProperty(this, x => x.IsStarting, out _isStarting);

            StopCommand = ReactiveCommand.CreateFromObservable(StopAsync, this.WhenAnyValue(x => x.CanStop));
            StopCommand.IsExecuting.ToProperty(this, x => x.IsStopping, out _isStopping);

            ResetLogCommand = ReactiveCommand.Create(ResetLog);

            LogMessages = new ObservableCollection<(LogLevel, string)>();
            LogLevelOptions = new ObservableCollection<string>();
            LogText = new ObservableStringBuilder();

            foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
            {
                LogLevelOptions.Add(level.ToString());
            }

            PropertyChanged += OnPropertyChanged;

            LogMessages.CollectionChanged += OnLogMessageChanged;

            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateCanStart();
            UpdateCanStop();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LogLevel))
                RefreshLog();

            if (e.PropertyName == nameof(URIString))
                UpdateCanStart();
        }    

        /// <summary>
        /// Subscribes to the dispatcher timer for the memory checks.
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
                _autoTracker.InGameCheck();

                if (_tickCount == 3)
                {
                    _tickCount = 0;

                    foreach (MemorySegmentType segment in Enum.GetValues(typeof(MemorySegmentType)))
                    {
                        _autoTracker.MemoryCheck(segment);
                    }
                }

            });
        }

        /// <summary>
        /// Subscribes to the ObservableCollection of log message's CollectionChanged event.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
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
                        {
                            AddLog(message);
                        }
                    }
                    finally { }
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event of the Autotracker class.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void OnAutoTrackerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AutoTracker.SNESConnector) && _autoTracker.SNESConnector != null)
            {
                _autoTracker.SNESConnector.PropertyChanged += OnConnectorPropertyChanged;
            }

            UpdateCanStart();
            UpdateCanStop();
        }

        /// <summary>
        /// Subscribes to the ConnectionStatusChanged event of the USB2SNESConnector class.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void OnConnectorStatusChanged(object sender, (ConnectionStatus, string) e)
        {
            UpdateCanStart();
            UpdateCanStop();
            UpdateStatusText(e.Item1);
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event of the USB2SNESConnector class.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void OnConnectorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateCanStart();
            UpdateCanStop();
        }

        /// <summary>
        /// Updates the CanStart value.
        /// </summary>
        private void UpdateCanStart()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanStart = _autoTracker.SNESConnector == null && CanCreateWebSocketUri();
            });
        }

        /// <summary>
        /// Starts autotracking.
        /// </summary>
        private void Start()
        {
            if (_autoTracker.SNESConnector != null)
            {
                Stop();
            }

            ResetLog();
            _autoTracker.Start(URIString);

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _inGameTimer.Start();
                _memoryCheckTimer.Start();
            });
        }

        /// <summary>
        /// Returns the observable result of the Start method.
        /// </summary>
        /// <returns>
        /// The observable result of the Start method.
        /// </returns>
        private IObservable<Unit> StartAsync()
        {
            return Observable.Start(() => { Start(); });
        }

        /// <summary>
        /// Updates the CanStop value.
        /// </summary>
        private void UpdateCanStop()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanStop = _autoTracker.SNESConnector != null;
            });
        }

        /// <summary>
        /// Stops autotracking.
        /// </summary>
        private void Stop()
        {
            _inGameTimer.Stop();
            _memoryCheckTimer.Stop();
            _autoTracker.Stop();
        }

        /// <summary>
        /// Returns the observable result of the Stop method.
        /// </summary>
        /// <returns>
        /// The observable result of the Stop method.
        /// </returns>
        private IObservable<Unit> StopAsync()
        {
            return Observable.Start(() => { Stop(); });
        }

        /// <summary>
        /// Updates the status text and color based on the latest status update.
        /// </summary>
        /// <param name="status">
        /// The status to be updated.
        /// </param>
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

        /// <summary>
        /// Adds a log message to the log view.
        /// </summary>
        /// <param name="message">
        /// The log message.
        /// </param>
        private void AddLog((string, LogLevel) message)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.AppendLine($"{ message.Item2.ToString().ToUpper(CultureInfo.CurrentCulture) }:" +
                    $" { message.Item1 }");
            });
        }

        /// <summary>
        /// Resets the log and log view to empty.
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

            foreach ((string, LogLevel) message in LogMessages)
            {
                if (message.Item2 >= _logLevel)
                {
                    AddLog(message);
                }
            }
        }

        /// <summary>
        /// Pushes a raw log entry to the log observable collection.
        /// </summary>
        /// <param name="rawMessage">
        /// The raw message string of the log entry.
        /// </param>
        /// <param name="level">
        /// The logging level of the log entry.
        /// </param>
        private void PushToLog(LogLevel level, string rawMessage)
        {
            LogMessages.Add((level, rawMessage));
        }

        /// <summary>
        /// Returns whether the URIString value is a valid URI to be accepted by 
        /// the web socket library.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the URIString value is valid.
        /// </returns>
        private bool CanCreateWebSocketUri()
        {
            var uri = URIString.ToUri();

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
        public void Save(string path)
        {
            using StreamWriter file = new StreamWriter(path);
            foreach ((string, LogLevel) message in LogMessages)
                file.WriteLine(message.Item2.ToString().ToUpper(CultureInfo.CurrentCulture) + ": " + message.Item1);
        }
    }
}