using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;
using LogLevel = OpenTracker.Models.AutoTracking.Logging.LogLevel;

namespace OpenTracker.ViewModels.AutoTracking
{
    /// <summary>
    /// This class contains the auto-tracker log control ViewModel data.
    /// </summary>
    public class AutoTrackerLogVM : ViewModelBase, IAutoTrackerLogVM
    {
        private readonly IAutoTrackerLogService _logService;

        private readonly IDialogService _dialogService;
        private readonly IFileDialogService _fileDialogService;

        private readonly IErrorBoxDialogVM.Factory _errorBoxFactory;

        public ObservableCollection<string> LogLevelOptions { get; } =
            new();
        public ObservableStringBuilder LogText { get; } =
            new();

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
        public ReactiveCommand<Unit, Unit> SaveLogCommand { get; }
        
        private readonly ObservableAsPropertyHelper<bool> _isResettingLog;
        private bool IsResettingLog => _isResettingLog.Value;

        private readonly ObservableAsPropertyHelper<bool> _isSavingLog;
        private bool IsSavingLog => _isSavingLog.Value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logService">
        /// The auto-tracking log service.
        /// </param>
        /// <param name="dialogService">
        /// The dialog service.
        /// </param>
        /// <param name="fileDialogService">
        /// The file dialog service.
        /// </param>
        /// <param name="errorBoxFactory">
        /// An Autofac factory for creating error box dialog windows.
        /// </param>
        public AutoTrackerLogVM(
            IAutoTrackerLogService logService, IDialogService dialogService, IFileDialogService fileDialogService,
            IErrorBoxDialogVM.Factory errorBoxFactory)
        {
            _logService = logService;

            _dialogService = dialogService;
            _fileDialogService = fileDialogService;

            _errorBoxFactory = errorBoxFactory;
            
            foreach (LogLevel logLevel in Enum.GetValues(typeof(LogLevel)))
            {
                LogLevelOptions.Add(logLevel.ToString());                
            }

            ResetLogCommand = ReactiveCommand.CreateFromTask(ResetLog);
            ResetLogCommand.IsExecuting.ToProperty(
                this, x => x.IsResettingLog, out _isResettingLog);

            SaveLogCommand = ReactiveCommand.CreateFromTask(SaveLog);
            SaveLogCommand.IsExecuting.ToProperty(
                this, x => x.IsSavingLog, out _isSavingLog);

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
        private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LogLevel))
            {
                await RefreshLog();
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
        private async void OnLogMessageChanged(object? sender, NotifyCollectionChangedEventArgs e)
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
                switch (item)
                {
                    case null:
                        continue;
                    case ILogMessage message when message.Level >= _logLevel:
                        await AddLog(message);
                        break;
                }
            }
        }

        /// <summary>
        /// Adds a log message to the log view.
        /// </summary>
        /// <param name="message">
        /// The log message.
        /// </param>
        private async Task AddLog(ILogMessage message)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.AppendLine(
                    $"{message.Level.ToString().ToUpper(CultureInfo.CurrentCulture)}:" +
                    message.Message);
            });
        }

        /// <summary>
        /// Regenerates the log view from the log messages.
        /// </summary>
        private async Task RefreshLog()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.Clear();
            });

            foreach (var message in _logService.LogCollection)
            {
                if (message.Level >= _logLevel)
                {
                    await AddLog(message);
                }
            }
        }

        /// <summary>
        /// Resets the log collection and log view to empty.
        /// </summary>
        private async Task ResetLog()
        {
            _logService.LogCollection.Clear();
            
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogText.Clear();
            });
        }
        
        /// <summary>
        /// Opens an error box with the specified message.
        /// </summary>
        /// <param name="message">
        /// The message to be contained in the error box.
        /// </param>
        private async Task OpenErrorBox(string message)
        {
            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await _dialogService.ShowDialogAsync(_errorBoxFactory("Error", message));
            });
        }

        /// <summary>
        /// Opens a file save dialog box and returns the result.
        /// </summary>
        /// <returns>
        /// A nullable string representing the result of the dialog box.
        /// </returns>
        private async Task<string?> OpenSaveFileDialog()
        {
            return await Dispatcher.UIThread.InvokeAsync(async () =>
                await _fileDialogService.ShowSaveDialogAsync());
        }

        /// <summary>
        /// Saves the log to the text file specified.
        /// </summary>
        private async Task SaveLog()
        {
            var path = await OpenSaveFileDialog();
            
            if (path is null)
            {
                return;
            }

            try
            {
                await using StreamWriter file = new(path);

                foreach (var message in _logService.LogCollection)
                {
                    await file.WriteLineAsync(
                        $"{message.Level.ToString().ToUpper(CultureInfo.CurrentCulture)}: " +
                        message.Message);
                }
            }
            catch (Exception ex)
            {
                string message = ex switch
                {
                    UnauthorizedAccessException _ =>
                        "Unable to save to the selected directory.  Check the file permissions and try again.",
                    _ => ex.Message
                };

                await OpenErrorBox(message);
            }
        }
    }
}
