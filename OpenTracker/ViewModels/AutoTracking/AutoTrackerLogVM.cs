using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using OpenTracker.Autofac;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;
using LogLevel = OpenTracker.Models.Logging.LogLevel;

namespace OpenTracker.ViewModels.AutoTracking;

/// <summary>
/// This class contains the auto-tracker log control ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public class AutoTrackerLogVM : ViewModel, IAutoTrackerLogVM
{
    private readonly IAutoTrackerLogService _logService;

    private readonly IFileDialogService _fileDialogService;

    public ObservableCollection<string> LogLevelOptions { get; } = new();

    private bool _logVisible;
    public bool LogVisible
    {
        get => _logVisible;
        set => this.RaiseAndSetIfChanged(ref _logVisible, value);
    }
    
    public Interaction<ErrorBoxDialogVM, Unit> OpenErrorBoxInteraction { get; } = new(RxApp.MainThreadScheduler);

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
    /// <param name="fileDialogService">
    /// The file dialog service.
    /// </param>
    public AutoTrackerLogVM(IAutoTrackerLogService logService, IFileDialogService fileDialogService)
    {
        _logService = logService;

        _fileDialogService = fileDialogService;

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
    }

    /// <summary>
    /// Resets the log collection and log view to empty.
    /// </summary>
    private async Task ResetLog()
    {
        _logService.LogCollection.Clear();
            
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
        });
    }
        
    /// <summary>
    /// Opens an error box with the specified message.
    /// </summary>
    /// <param name="message">
    /// The message to be contained in the error box.
    /// </param>
    private async Task OpenErrorBoxAsync(string message)
    {
        await OpenErrorBoxInteraction.Handle(new ErrorBoxDialogVM("Error", message));
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
                    message.Content);
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

            await OpenErrorBoxAsync(message);
        }
    }
}