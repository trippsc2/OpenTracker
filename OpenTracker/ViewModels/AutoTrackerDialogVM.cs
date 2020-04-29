using Avalonia.Media;
using OpenTracker.Models;
using OpenTracker.Models.AutotrackerConnectors;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class AutoTrackerDialogVM : ViewModelBase
    {
        private readonly AutoTracker _autoTracker;

        public ReactiveCommand<Unit, Unit> StartCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDebugLogCommand { get; }

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

        private IBrush _statusTextColor;
        public IBrush StatusTextColor
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

        private bool _visibleDebugLog;
        public bool VisibleDebugLog
        {
            get => _visibleDebugLog;
            set => this.RaiseAndSetIfChanged(ref _visibleDebugLog, value);
        }

        private string _debugText;
        public string DebugText
        {
            get => _debugText;
            private set => this.RaiseAndSetIfChanged(ref _debugText, value);
        }

        public AutoTrackerDialogVM(AutoTracker autoTracker)
        {
            _autoTracker = autoTracker;

            DebugText = "";
            StartCommand = ReactiveCommand.Create(Start, this.WhenAnyValue(x => x.CanStart));
            StopCommand = ReactiveCommand.Create(Stop, this.WhenAnyValue(x => x.CanStop));
            ToggleDebugLogCommand = ReactiveCommand.Create(ToggleDebugLog);

            _autoTracker.PropertyChanging += OnAutoTrackerChanging;
            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            UpdateCanStart();
            UpdateCanStop();
            UpdateStatusText();
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
            if (_autoTracker.Connector == null)
            {
                StatusTextColor = SolidColorBrush.Parse("#f5f5f5");
                StatusText = "NOT STARTED";
            }
            else if (_autoTracker.Connector.Connected)
            {
                StatusTextColor = SolidColorBrush.Parse("#00ff00");
                StatusText = "CONNECTED";
            }
            else
            {
                StatusTextColor = SolidColorBrush.Parse("#ff3030");
                StatusText = "ERROR";
            }
        }

        private void Start()
        {
            if (_autoTracker.Connector != null)
                Stop();

            DebugText = "";

            _autoTracker.Start(PushToDebugLog);
        }

        private void Stop()
        {
            _autoTracker.Stop();
        }

        private void ToggleDebugLog()
        {
            VisibleDebugLog = !VisibleDebugLog;
        }

        private void PushToDebugLog(string rawMessage)
        {
            DebugText += rawMessage + "\n";
        }
    }
}