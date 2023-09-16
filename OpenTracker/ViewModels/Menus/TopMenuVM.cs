using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Autofac;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;
using OpenTracker.Utils.Themes;
using OpenTracker.ViewModels.ColorSelect;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;

namespace OpenTracker.ViewModels.Menus;

/// <summary>
/// This class contains the top menu control ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class TopMenuVM : ViewModel, ITopMenuVM
{
    private readonly IAppSettings _appSettings;
    private readonly IResetManager _resetManager;
    private readonly ISaveLoadManager _saveLoadManager;
    private readonly IUndoRedoManager _undoRedoManager;

    private readonly IDialogService _dialogService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IThemeManager _themeManager;

    private readonly AutoTrackerDialogVM _autoTrackerDialog;
    private readonly SequenceBreakDialogVM _sequenceBreakDialog;
    private readonly ColorSelectDialogVM _colorSelectDialog;
    private readonly AboutDialogVM _aboutDialog;

    public List<IMenuItemVM> Items { get; }
    
    public ReactiveCommand<Unit, Unit> Open { get; }
    public ReactiveCommand<Unit, Unit> Save { get; }
    public ReactiveCommand<Unit, Unit> SaveAs { get; }
    public ReactiveCommand<Unit, Unit> Reset { get; }
    public ReactiveCommand<Unit, Unit> Close { get; }

    private readonly ObservableAsPropertyHelper<bool> _isOpening;
    private bool IsOpening => _isOpening.Value;

    private readonly ObservableAsPropertyHelper<bool> _isSaving;
    private bool IsSaving => _isSaving.Value;

    private readonly ObservableAsPropertyHelper<bool> _isSavingAs;
    private bool IsSavingAs => _isSavingAs.Value;

    private readonly ObservableAsPropertyHelper<bool> _isResetting;
    private bool IsResetting => _isResetting.Value;

    public bool CanUndo => _undoRedoManager.CanUndo;
    public bool CanRedo => _undoRedoManager.CanRedo;

    public ReactiveCommand<Unit, Unit> Undo { get; }
    public ReactiveCommand<Unit, Unit> Redo { get; }
    public ReactiveCommand<Unit, Unit> AutoTracker { get; }
    public ReactiveCommand<Unit, Unit> SequenceBreaks { get; }

    private readonly ObservableAsPropertyHelper<bool> _isUndoing;
    private bool IsUndoing => _isUndoing.Value;

    private readonly ObservableAsPropertyHelper<bool> _isRedoing;
    private bool IsRedoing => _isRedoing.Value;

    private readonly ObservableAsPropertyHelper<bool> _isOpeningAutoTracker;
    private bool IsOpeningAutoTracker => _isOpeningAutoTracker.Value;

    private readonly ObservableAsPropertyHelper<bool> _isOpeningSequenceBreak;
    private bool IsOpeningSequenceBreak => _isOpeningSequenceBreak.Value;
        
    public ReactiveCommand<ITheme, Unit> ChangeTheme { get; }
        
    public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocations { get; }
    public ReactiveCommand<Unit, Unit> ToggleShowItemCountsOnMap { get; }
    public ReactiveCommand<Unit, Unit> ToggleDisplayMapsCompasses { get; }
    public ReactiveCommand<Unit, Unit> ToggleAlwaysDisplayDungeonItems { get; }

    public ReactiveCommand<Unit, Unit> ColorSelect { get; }

    private readonly ObservableAsPropertyHelper<bool> _isOpeningColorSelect;
    private bool IsOpeningColorSelect => _isOpeningColorSelect.Value;

    public ReactiveCommand<Orientation?, Unit> ChangeLayoutOrientation { get; }
    public ReactiveCommand<Orientation?, Unit> ChangeMapOrientation { get; }
    public ReactiveCommand<Dock, Unit> ChangeHorizontalUIPanelPlacement { get; }
    public ReactiveCommand<Dock, Unit> ChangeVerticalUIPanelPlacement { get; }
    public ReactiveCommand<Dock, Unit> ChangeHorizontalItemsPlacement { get; }
    public ReactiveCommand<Dock, Unit> ChangeVerticalItemsPlacement { get; }
    public ReactiveCommand<double, Unit> ChangeUIScale { get; }

    public ReactiveCommand<Unit, Unit> About { get; }

    private readonly ObservableAsPropertyHelper<bool> _isOpeningAbout;
    private bool IsOpeningAbout => _isOpeningAbout.Value;

    /// <summary>
    /// Constructor
    /// </summary>
    public TopMenuVM(
        IAppSettings appSettings, IResetManager resetManager,
        ISaveLoadManager saveLoadManager, IUndoRedoManager undoRedoManager, IDialogService dialogService,
        IFileDialogService fileDialogService, IThemeManager themeManager, AutoTrackerDialogVM autoTrackerDialog,
        ColorSelectDialogVM colorSelectDialog,
        SequenceBreakDialogVM sequenceBreakDialog, AboutDialogVM aboutDialog,
        IMenuItemFactory factory, ReactiveCommand<Unit, Unit> closeCommand)
    {
        _appSettings = appSettings;
        _resetManager = resetManager;
        _saveLoadManager = saveLoadManager;
        _undoRedoManager = undoRedoManager;

        _dialogService = dialogService;
        _fileDialogService = fileDialogService;
        _themeManager = themeManager;

        _autoTrackerDialog = autoTrackerDialog;
        _colorSelectDialog = colorSelectDialog;
        _sequenceBreakDialog = sequenceBreakDialog;
        _aboutDialog = aboutDialog;

        Open = ReactiveCommand.CreateFromTask(OpenImpl);
        Open.IsExecuting.ToProperty(this, x => x.IsOpening, out _isOpening);

        Save = ReactiveCommand.CreateFromTask(SaveImpl);
        Save.IsExecuting.ToProperty(this, x => x.IsSaving, out _isSaving);

        SaveAs = ReactiveCommand.CreateFromTask(SaveAsImpl);
        SaveAs.IsExecuting.ToProperty(this, x => x.IsSavingAs, out _isSavingAs);

        Reset = ReactiveCommand.CreateFromTask(ResetImpl);
        Reset.IsExecuting.ToProperty(this, x => x.IsResetting, out _isResetting);
            
        Close = closeCommand;

        Undo = ReactiveCommand.CreateFromTask(UndoImpl, this.WhenAnyValue(x => x.CanUndo));
        Undo.IsExecuting.ToProperty(this, x => x.IsUndoing, out _isUndoing);

        Redo = ReactiveCommand.CreateFromTask(RedoImpl, this.WhenAnyValue(x => x.CanRedo));
        Redo.IsExecuting.ToProperty(this, x => x.IsRedoing, out _isRedoing);

        AutoTracker = ReactiveCommand.CreateFromTask(AutoTrackerImpl);
        AutoTracker.IsExecuting.ToProperty(
            this, x => x.IsOpeningAutoTracker, out _isOpeningAutoTracker);

        SequenceBreaks = ReactiveCommand.CreateFromTask(SequenceBreaksImpl);
        SequenceBreaks.IsExecuting.ToProperty(
            this, x => x.IsOpeningSequenceBreak, out _isOpeningSequenceBreak);

        ChangeTheme = ReactiveCommand.Create<ITheme>(ChangeThemeImpl);
            
        ToggleDisplayAllLocations = ReactiveCommand.Create(ToggleDisplayAllLocationsImpl);
        ToggleShowItemCountsOnMap = ReactiveCommand.Create(ToggleShowItemCountsOnMapImpl);
        ToggleDisplayMapsCompasses = ReactiveCommand.Create(ToggleDisplayMapsCompassesImpl);
        ToggleAlwaysDisplayDungeonItems = ReactiveCommand.Create(ToggleAlwaysDisplayDungeonItemsImpl);

        ColorSelect = ReactiveCommand.CreateFromTask(ColorSelectImpl);
        ColorSelect.IsExecuting.ToProperty(
            this, x => x.IsOpeningColorSelect, out _isOpeningColorSelect);

        ChangeLayoutOrientation = ReactiveCommand.Create<Orientation?>(ChangeLayoutOrientationImpl);
        ChangeMapOrientation = ReactiveCommand.Create<Orientation?>(ChangeMapOrientationImpl);
        ChangeHorizontalUIPanelPlacement = ReactiveCommand.Create<Dock>(ChangeHorizontalUIPanelPlacementImpl);
        ChangeVerticalUIPanelPlacement = ReactiveCommand.Create<Dock>(ChangeVerticalUIPanelPlacementImpl);
        ChangeHorizontalItemsPlacement = ReactiveCommand.Create<Dock>(ChangeHorizontalItemsPlacementImpl);
        ChangeVerticalItemsPlacement = ReactiveCommand.Create<Dock>(ChangeVerticalItemsPlacementImpl);
        ChangeUIScale = ReactiveCommand.Create<double>(ChangeUIScaleImpl);

        About = ReactiveCommand.CreateFromTask(AboutImpl);
        About.IsExecuting.ToProperty(
            this, x => x.IsOpeningAbout, out _isOpeningAbout);

        Items = factory.GetMenuItems(
            Open, Save, SaveAs, Reset, Close, Undo, Redo, AutoTracker, SequenceBreaks, ChangeTheme,
            ToggleDisplayAllLocations, ToggleShowItemCountsOnMap, ToggleDisplayMapsCompasses,
            ToggleAlwaysDisplayDungeonItems, ColorSelect, ChangeLayoutOrientation, ChangeHorizontalUIPanelPlacement,
            ChangeHorizontalItemsPlacement, ChangeVerticalUIPanelPlacement, ChangeVerticalItemsPlacement,
            ChangeMapOrientation, ChangeUIScale, About);
            
        _undoRedoManager.PropertyChanged += OnUndoRedoManagerChanged;
    }

    /// <summary>
    /// Subscribes to the CollectionChanged event on the observable stack of undoable actions.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnUndoRedoManagerChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(UndoRedoManager.CanUndo):
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(CanUndo)));
                break;
            case nameof(UndoRedoManager.CanRedo):
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(CanRedo)));
                break;
        }
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
            await _dialogService.ShowDialogAsync(new ErrorBoxDialogVM("Error", message)));
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
    /// Save the data to the file and handles errors by opening an error box.
    /// </summary>
    /// <param name="path">
    /// A string representing the file path.
    /// </param>
    private async Task SaveWithErrorHandling(string path)
    {
        try
        {
            _saveLoadManager.Save(path);
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

    /// <summary>
    /// Opens a file with saved data.
    /// </summary>
    private async Task OpenImpl()
    {
        var dialogResult = await _fileDialogService.ShowOpenDialogAsync();

        if (dialogResult is null)
        {
            return;
        }

        try
        {
            _saveLoadManager.Open(dialogResult);
        }
        catch (Exception ex)
        {
            string message = ex switch
            {
                JsonReaderException _ => "The selected file is not a valid JSON file.",
                UnauthorizedAccessException _ =>
                    "The file cannot be read.  Check the permissions on the selected file.",
                _ => ex.Message
            };

            await OpenErrorBox(message);
        }
    }

    /// <summary>
    /// If the file is already saved, save the current data to the existing path.  Otherwise,
    /// open a save file dialog window and save to a new path.
    /// </summary>
    /// <returns>
    /// An observable representing the progress of the command.
    /// </returns>
    private async Task SaveImpl()
    {
        var path = _saveLoadManager.CurrentFilePath ?? await OpenSaveFileDialog();

        if (!(path is null))
        {
            await SaveWithErrorHandling(path);
        }
    }

    /// <summary>
    /// Opens a save file dialog window and saves to a new path.
    /// </summary>
    private async Task SaveAsImpl()
    {
        var dialogResult = await OpenSaveFileDialog();

        if (dialogResult is null)
        {
            return;
        }
            
        await SaveWithErrorHandling(dialogResult);
    }

    /// <summary>
    /// Resets the tracker to default values.
    /// </summary>
    private async Task ResetImpl()
    {
        var result = await _dialogService.ShowDialogAsync<MessageBoxDialogVM, bool>(
            new MessageBoxDialogVM("Warning",
                "Resetting the tracker will set all items and locations back to their " +
                "starting values. This cannot be undone.\n\nDo you wish to proceed?"));

        if (result)
        {
            _resetManager.ResetAsync();
        }
    }

    /// <summary>
    /// Undoes the last action.
    /// </summary>
    private async Task UndoImpl()
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _undoRedoManager.Undo();
        });
    }

    /// <summary>
    /// Redoes the last action.
    /// </summary>
    private async Task RedoImpl()
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _undoRedoManager.Redo();
        });
    }

    /// <summary>
    /// Opens the AutoTracker dialog window.
    /// </summary>
    private async Task AutoTrackerImpl()
    {
        await Dispatcher.UIThread.InvokeAsync(async () =>
            await _dialogService.ShowDialogAsync(_autoTrackerDialog, false));
    }

    /// <summary>
    /// Opens the Sequence Break dialog window.
    /// </summary>
    private async Task SequenceBreaksImpl()
    {
        await Dispatcher.UIThread.InvokeAsync(async () =>
            await _dialogService.ShowDialogAsync(_sequenceBreakDialog, false));
    }

    /// <summary>
    /// Changes the currently selected theme.
    /// </summary>
    /// <param name="theme">
    /// The theme to be selected.
    /// </param>
    private void ChangeThemeImpl(ITheme theme)
    {
        _themeManager.SelectedTheme = theme;
    }

    /// <summary>
    /// Toggles whether to show the item counts on the map.
    /// </summary>
    private void ToggleShowItemCountsOnMapImpl()
    {
        _appSettings.Tracker.ShowItemCountsOnMap = !_appSettings.Tracker.ShowItemCountsOnMap;
    }

    /// <summary>
    /// Sets the layout orientation to the specified value.
    /// </summary>
    /// <param name="newValue">
    /// The new orientation value.
    /// </param>
    private void ChangeLayoutOrientationImpl(Orientation? newValue)
    {
        _appSettings.Layout.LayoutOrientation = newValue;
    }

    /// <summary>
    /// Sets the map orientation to the specified value.
    /// </summary>
    /// <param name="newValue">
    /// The new orientation value.
    /// </param>
    private void ChangeMapOrientationImpl(Orientation? newValue)
    {
        _appSettings.Layout.MapOrientation = newValue;
    }

    /// <summary>
    /// Sets the horizontal UI panel orientation to the specified value.
    /// </summary>
    /// <param name="newValue">
    /// The new dock value.
    /// </param>
    private void ChangeHorizontalUIPanelPlacementImpl(Dock newValue)
    {
        _appSettings.Layout.HorizontalUIPanelPlacement = newValue;
    }

    /// <summary>
    /// Sets the vertical UI panel orientation to the specified value.
    /// </summary>
    /// <param name="newValue">
    /// The new dock value.
    /// </param>
    private void ChangeVerticalUIPanelPlacementImpl(Dock newValue)
    {
        _appSettings.Layout.VerticalUIPanelPlacement = newValue;
    }

    /// <summary>
    /// Sets the horizontal items placement orientation to the specified value.
    /// </summary>
    /// <param name="newValue">
    /// The new dock value.
    /// </param>
    private void ChangeHorizontalItemsPlacementImpl(Dock newValue)
    {
        _appSettings.Layout.HorizontalItemsPlacement = newValue;
    }

    /// <summary>
    /// Sets the vertical items placement orientation to the specified value.
    /// </summary>
    /// <param name="newValue">
    /// The new dock value.
    /// </param>
    private void ChangeVerticalItemsPlacementImpl(Dock newValue) 
    {
        _appSettings.Layout.VerticalItemsPlacement = newValue;
    }

    /// <summary>
    /// Sets the UI scale to the specified value.
    /// </summary>
    /// <param name="newValue">
    /// A floating point number representing the UI scale value.
    /// </param>
    private void ChangeUIScaleImpl(double newValue)
    {
        _appSettings.Layout.UIScale = newValue;
    }

    /// <summary>
    /// Toggles whether to display all locations on the map.
    /// </summary>
    private void ToggleDisplayAllLocationsImpl()
    {
        _appSettings.Tracker.DisplayAllLocations = !_appSettings.Tracker.DisplayAllLocations;
    }

    /// <summary>
    /// Toggles whether to display maps and compasses.
    /// </summary>
    private void ToggleDisplayMapsCompassesImpl()
    {
        _appSettings.Layout.DisplayMapsCompasses = !_appSettings.Layout.DisplayMapsCompasses;
    }

    /// <summary>
    /// Toggles whether to always display dungeon items.
    /// </summary>
    private void ToggleAlwaysDisplayDungeonItemsImpl()
    {
        _appSettings.Layout.AlwaysDisplayDungeonItems = !_appSettings.Layout.AlwaysDisplayDungeonItems;
    }

    /// <summary>
    /// Opens the Color Select dialog window.
    /// </summary>
    private async Task ColorSelectImpl()
    {
        await Dispatcher.UIThread.InvokeAsync(async () =>
            await _dialogService.ShowDialogAsync(_colorSelectDialog, false));
    }

    /// <summary>
    /// Opens the About dialog window.
    /// </summary>
    private async Task AboutImpl()
    {
        await Dispatcher.UIThread.InvokeAsync(async () =>
            await _dialogService.ShowDialogAsync(_aboutDialog, false));
    }
}