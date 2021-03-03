using Avalonia.Threading;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reactive;
using LogLevel = OpenTracker.Models.AutoTracking.Logging.LogLevel;

namespace OpenTracker.ViewModels.AutoTracking
{
    /// <summary>
    /// This class contains the auto-tracker log control ViewModel data.
    /// </summary>
    public class AutoTrackerLogVM : ViewModelBase, IAutoTrackerLogVM
    {
        private readonly IAutoTrackerLogService _logService;

        public ObservableCollection<string> LogLevelOptions { get; } =
            new ObservableCollection<string>();
        public ObservableStringBuilder LogText { get; } =
            new ObservableStringBuilder();

        private LogLevel _logLevel = Models.AutoTracking.Logging.LogLevel.Info;
        public string LogLevel
        {
            get => _logLevel.ToString();
            set => this.RaiseAndSetIfChanged(ref _logLevel, Enum.Parse<LogLevel>(value));
        }

        private bool _logVisible;
        public bool LogVisible
        {
            get => _logVisible;
            set => this.RaiseAndSetIfChanged(ref _logVisible, value);
        }

        public ReactiveCommand<Unit, Unit> ResetLogCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logService">
        /// The auto-tracking log service.
        /// </param>
        public AutoTrackerLogVM(IAutoTrackerLogService logService)
        {
            _logService = logService;

            foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
            {
                LogLevelOptions.Add(level.ToString());
            }

            ResetLogCommand = ReactiveCommand.Create(ResetLog);

            PropertyChanged += OnPropertyChanged;
            _logService.LogCollection.CollectionChanged += OnLogMessageChanged;
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
            if (e.PropertyName == nameof(LogLevel))
            {
                RefreshLog();
            }
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
        private void OnLogMessageChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }

            if (e.NewItems is null)
            {
                throw new NullReferenceException();
            }

            foreach (var item in e.NewItems)
            {
                if (item != null && item is ILogMessage message &&
                    message.LogLevel >= _logLevel)
                {
                    AddLog(message);
                }
            }
        }

        /// <summary>
        /// Adds a log message to the log view.
        /// </summary>
        /// <param name="message">
        /// The log message.
        /// </param>
        private void AddLog(ILogMessage message)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.AppendLine(
                    $"{message.LogLevel.ToString().ToUpper(CultureInfo.CurrentCulture)}:" +
                    message.Message);
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

            foreach (var message in _logService.LogCollection)
            {
                if (message.LogLevel >= _logLevel)
                {
                    AddLog(message);
                }
            }
        }

        /// <summary>
        /// Resets the log collection and log view to empty.
        /// </summary>
        private void ResetLog()
        {
            _logService.LogCollection.Clear();
            
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.Clear();
            });
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

            foreach (var message in _logService.LogCollection)
            {
                file.WriteLine(
                    $"{message.LogLevel.ToString().ToUpper(CultureInfo.CurrentCulture)}: " +
                    message.Message);
            }
        }
    }
}
