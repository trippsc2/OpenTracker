using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.AutoTracking;
using OpenTracker.ViewModels.ColorSelect;
using OpenTracker.ViewModels.SequenceBreaks;
using ReactiveUI;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel class for the top menu control.
    /// </summary>
    public class TopMenuVM : ViewModelBase, ITopMenuVM
    {
        private readonly IAppSettings _appSettings;
        private readonly IResetManager _resetManager;
        private readonly ISaveLoadManager _saveLoadManager;
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IDialogService _dialogService;
        private readonly IFileDialogService _fileDialogService;

        private readonly IAutoTrackerDialogVM _autoTrackerDialog;
        private readonly ISequenceBreakDialogVM _sequenceBreakDialog;
        private readonly IColorSelectDialogVM _colorSelectDialog;
        private readonly IAboutDialogVM _aboutDialog;

        private readonly IErrorBoxDialogVM.Factory _errorBoxFactory;
        private readonly IMessageBoxDialogVM.Factory _messageBoxFactory;

        public bool DisplayAllLocations =>
            _appSettings.Tracker.DisplayAllLocations;
        public bool ShowItemCountsOnMap =>
            _appSettings.Tracker.ShowItemCountsOnMap;

        public bool DisplayMapsCompasses =>
            _appSettings.Layout.DisplayMapsCompasses;
        public bool AlwaysDisplayDungeonItems =>
            _appSettings.Layout.AlwaysDisplayDungeonItems;

        public bool DynamicLayoutOrientation =>
            _appSettings.Layout.LayoutOrientation == null;
        public bool HorizontalLayoutOrientation =>
            _appSettings.Layout.LayoutOrientation == Orientation.Horizontal;
        public bool VerticalLayoutOrientation =>
            _appSettings.Layout.LayoutOrientation == Orientation.Vertical;

        public bool DynamicMapOrientation =>
            _appSettings.Layout.MapOrientation == null;
        public bool HorizontalMapOrientation =>
            _appSettings.Layout.MapOrientation == Orientation.Horizontal;
        public bool VerticalMapOrientation =>
            _appSettings.Layout.MapOrientation == Orientation.Vertical;

        public bool TopHorizontalUIPanelPlacement =>
            _appSettings.Layout.HorizontalUIPanelPlacement == Dock.Top;
        public bool BottomHorizontalUIPanelPlacement =>
            _appSettings.Layout.HorizontalUIPanelPlacement == Dock.Bottom;

        public bool LeftVerticalUIPanelPlacement =>
            _appSettings.Layout.VerticalUIPanelPlacement == Dock.Left;
        public bool RightVerticalUIPanelPlacement =>
            _appSettings.Layout.VerticalUIPanelPlacement == Dock.Right;

        public bool LeftHorizontalItemsPlacement =>
            _appSettings.Layout.HorizontalItemsPlacement == Dock.Left;
        public bool RightHorizontalItemsPlacement =>
            _appSettings.Layout.HorizontalItemsPlacement == Dock.Right;

        public bool TopVerticalItemsPlacement =>
            _appSettings.Layout.VerticalItemsPlacement == Dock.Top;
        public bool BottomVerticalItemsPlacement =>
            _appSettings.Layout.VerticalItemsPlacement == Dock.Bottom;

        public bool OneHundredPercentUIScale =>
            _appSettings.Layout.UIScale == 1.0;
        public bool OneHundredTwentyFivePercentUIScale =>
            _appSettings.Layout.UIScale == 1.25;
        public bool OneHundredFiftyPercentUIScale =>
            _appSettings.Layout.UIScale == 1.50;
        public bool OneHundredSeventyFivePercentUIScale =>
            _appSettings.Layout.UIScale == 1.75;
        public bool TwoHundredPercentUIScale =>
            _appSettings.Layout.UIScale == 2.0;

        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveAsCommand { get; }
        public ReactiveCommand<Unit, Unit> ResetCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isOpening;
        public bool IsOpening =>
            _isOpening.Value;
        private readonly ObservableAsPropertyHelper<bool> _isSaving;
        public bool IsSaving =>
            _isSaving.Value;
        private readonly ObservableAsPropertyHelper<bool> _isSavingAs;
        public bool IsSavingAs =>
            _isSavingAs.Value;
        private readonly ObservableAsPropertyHelper<bool> _isResetting;
        public bool IsResetting =>
            _isResetting.Value;

        public bool CanUndo =>
            _undoRedoManager.CanUndo;
        public bool CanRedo =>
            _undoRedoManager.CanRedo;

        public ReactiveCommand<Unit, Unit> UndoCommand { get; }
        public ReactiveCommand<Unit, Unit> RedoCommand { get; }
        public ReactiveCommand<Unit, Unit> AutoTrackerCommand { get; }
        public ReactiveCommand<Unit, Unit> SequenceBreaksCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isUndoing;
        public bool IsUndoing =>
            _isUndoing.Value;
        private readonly ObservableAsPropertyHelper<bool> _isRedoing;
        public bool IsRedoing =>
            _isRedoing.Value;
        private readonly ObservableAsPropertyHelper<bool> _isOpeningAutoTracker;
        public bool IsOpeningAutoTracker =>
            _isOpeningAutoTracker.Value;
        private readonly ObservableAsPropertyHelper<bool> _isOpeningSequenceBreak;
        public bool IsOpeningSequenceBreak =>
            _isOpeningSequenceBreak.Value;

        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleShowItemCountsOnMapCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayMapsCompassesCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleAlwaysDisplayDungeonItemsCommand { get; }

        public ReactiveCommand<Unit, Unit> ColorSelectCommand { get; }
        private readonly ObservableAsPropertyHelper<bool> _isOpeningColorSelect;
        public bool IsOpeningColorSelect =>
            _isOpeningColorSelect.Value;

        public ReactiveCommand<string, Unit> SetLayoutOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetMapOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalItemsPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalItemsPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetUIScaleCommand { get; }

        public ReactiveCommand<Unit, Unit> AboutCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isOpeningAbout;
        public bool IsOpeningAbout =>
            _isOpeningAbout.Value;

        /// <summary>
        /// Constructor
        /// </summary>
        public TopMenuVM(
            IAppSettings appSettings, IResetManager resetManager, ISaveLoadManager saveLoadManager,
            IUndoRedoManager undoRedoManager, IDialogService dialogService,
            IFileDialogService fileDialogService, IAutoTrackerDialogVM autoTrackerDialog,
            IColorSelectDialogVM colorSelectDialog, ISequenceBreakDialogVM sequenceBreakDialog,
            IAboutDialogVM aboutDialog, IErrorBoxDialogVM.Factory errorBoxFactory,
            IMessageBoxDialogVM.Factory messageBoxFactory)
        {
            _appSettings = appSettings;
            _resetManager = resetManager;
            _saveLoadManager = saveLoadManager;
            _undoRedoManager = undoRedoManager;

            _dialogService = dialogService;
            _fileDialogService = fileDialogService;

            _autoTrackerDialog = autoTrackerDialog;
            _colorSelectDialog = colorSelectDialog;
            _sequenceBreakDialog = sequenceBreakDialog;
            _aboutDialog = aboutDialog;

            _errorBoxFactory = errorBoxFactory;
            _messageBoxFactory = messageBoxFactory;

            OpenCommand = ReactiveCommand.CreateFromTask(Open);
            OpenCommand.IsExecuting.ToProperty(this, x => x.IsOpening, out _isOpening);

            SaveCommand = ReactiveCommand.CreateFromTask(Save);
            SaveCommand.IsExecuting.ToProperty(this, x => x.IsSaving, out _isSaving);

            SaveAsCommand = ReactiveCommand.CreateFromTask(SaveAs);
            SaveAsCommand.IsExecuting.ToProperty(this, x => x.IsSavingAs, out _isSavingAs);

            ResetCommand = ReactiveCommand.CreateFromTask(Reset);
            ResetCommand.IsExecuting.ToProperty(this, x => x.IsResetting, out _isResetting);

            UndoCommand = ReactiveCommand.CreateFromTask(Undo, this.WhenAnyValue(x => x.CanUndo));
            UndoCommand.IsExecuting.ToProperty(this, x => x.IsUndoing, out _isUndoing);

            RedoCommand = ReactiveCommand.CreateFromTask(Redo, this.WhenAnyValue(x => x.CanRedo));
            RedoCommand.IsExecuting.ToProperty(this, x => x.IsRedoing, out _isRedoing);

            AutoTrackerCommand = ReactiveCommand.CreateFromTask(AutoTracker);
            AutoTrackerCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningAutoTracker, out _isOpeningAutoTracker);

            SequenceBreaksCommand = ReactiveCommand.CreateFromTask(SequencesBreak);
            SequenceBreaksCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningSequenceBreak, out _isOpeningSequenceBreak);

            ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);
            ToggleShowItemCountsOnMapCommand = ReactiveCommand.Create(ToggleShowItemCountsOnMap);
            ToggleDisplayMapsCompassesCommand = ReactiveCommand.Create(ToggleDisplayMapsCompasses);
            ToggleAlwaysDisplayDungeonItemsCommand = ReactiveCommand.Create(ToggleAlwaysDisplayDungeonItems);

            ColorSelectCommand = ReactiveCommand.CreateFromTask(ColorSelect);
            ColorSelectCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningColorSelect, out _isOpeningColorSelect);

            SetLayoutOrientationCommand = ReactiveCommand.Create<string>(SetLayoutOrientation);
            SetMapOrientationCommand = ReactiveCommand.Create<string>(SetMapOrientation);
            SetHorizontalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalUIPanelPlacement);
            SetVerticalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetVerticalUIPanelPlacement);
            SetHorizontalItemsPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalItemsPlacement);
            SetVerticalItemsPlacementCommand = ReactiveCommand.Create<string>(SetVerticalItemsPlacement);
            SetUIScaleCommand = ReactiveCommand.Create<string>(SetUIScale);

            AboutCommand = ReactiveCommand.CreateFromTask(About);
            AboutCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningAbout, out _isOpeningAbout);

            _undoRedoManager.PropertyChanged += OnUndoRedoManagerChanged;
            _appSettings.Tracker.PropertyChanged += OnTrackerSettingsChanged;
            _appSettings.Layout.PropertyChanged += OnLayoutChanged;
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
        private void OnUndoRedoManagerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UndoRedoManager.CanUndo))
            {
                this.RaisePropertyChanged(nameof(CanUndo));
            }

            if (e.PropertyName == nameof(UndoRedoManager.CanRedo))
            {
                this.RaisePropertyChanged(nameof(CanRedo));

            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the TrackerSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnTrackerSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TrackerSettings.DisplayAllLocations))
            {
                this.RaisePropertyChanged(nameof(DisplayAllLocations));
            }

            if (e.PropertyName == nameof(TrackerSettings.ShowItemCountsOnMap))
            {
                this.RaisePropertyChanged(nameof(ShowItemCountsOnMap));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.DisplayMapsCompasses))
            {
                this.RaisePropertyChanged(nameof(DisplayMapsCompasses));
            }

            if (e.PropertyName == nameof(LayoutSettings.AlwaysDisplayDungeonItems))
            {
                this.RaisePropertyChanged(nameof(AlwaysDisplayDungeonItems));
            }

            if (e.PropertyName == nameof(LayoutSettings.LayoutOrientation))
            {
                this.RaisePropertyChanged(nameof(DynamicLayoutOrientation));
                this.RaisePropertyChanged(nameof(HorizontalLayoutOrientation));
                this.RaisePropertyChanged(nameof(VerticalLayoutOrientation));
            }

            if (e.PropertyName == nameof(LayoutSettings.MapOrientation))
            {
                this.RaisePropertyChanged(nameof(DynamicMapOrientation));
                this.RaisePropertyChanged(nameof(HorizontalMapOrientation));
                this.RaisePropertyChanged(nameof(VerticalMapOrientation));
            }

            if (e.PropertyName == nameof(LayoutSettings.HorizontalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(TopHorizontalUIPanelPlacement));
                this.RaisePropertyChanged(nameof(BottomHorizontalUIPanelPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.VerticalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(LeftVerticalUIPanelPlacement));
                this.RaisePropertyChanged(nameof(RightVerticalUIPanelPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.HorizontalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(LeftHorizontalItemsPlacement));
                this.RaisePropertyChanged(nameof(RightHorizontalItemsPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.VerticalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(TopVerticalItemsPlacement));
                this.RaisePropertyChanged(nameof(BottomVerticalItemsPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(OneHundredPercentUIScale));
                this.RaisePropertyChanged(nameof(OneHundredTwentyFivePercentUIScale));
                this.RaisePropertyChanged(nameof(OneHundredFiftyPercentUIScale));
                this.RaisePropertyChanged(nameof(OneHundredSeventyFivePercentUIScale));
                this.RaisePropertyChanged(nameof(TwoHundredPercentUIScale));
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
            return await _fileDialogService.ShowSaveDialogAsync();
        }

        /// <summary>
        /// Save the data to the file and handles errors by opening an error box.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        private async Task SaveWithErrorHandling(string path)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                try
                {
                    _saveLoadManager.Save(path);
                }
                catch (Exception ex)
                {
                    string message;

                    switch (ex)
                    {
                        case UnauthorizedAccessException uaex:
                            {
                                message = "Unable to save to the selected directory.  Check the file permissions and try again.";
                            }
                            break;
                        default:
                            {
                                message = ex.Message;
                            }
                            break;
                    }

                    OpenErrorBox(message).Wait();
                }
            });
        }

        /// <summary>
        /// Opens a file with saved data.
        /// </summary>
        public async Task Open()
        {
            var dialogResult = await _fileDialogService.ShowOpenDialogAsync();

            if (dialogResult != null)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    try
                    {
                        _saveLoadManager.Open(dialogResult);
                    }
                    catch (Exception ex)
                    {
                        string message;

                        switch (ex)
                        {
                            case JsonReaderException jex:
                                {
                                    message = "The selected file is not a valid JSON file.";
                                }
                                break;
                            case UnauthorizedAccessException uaex:
                                {
                                    message = "The file cannot be read.  Check the permissions on the selected file.";
                                }
                                break;
                            default:
                                {
                                    message = ex.Message;
                                }
                                break;
                        }

                        OpenErrorBox(message).Wait();
                    }
                });
            }
        }

        /// <summary>
        /// If the file is already saved, save the current data to the existing path.  Otherwise,
        /// open a save file dialog window and save to a new path.
        /// </summary>
        /// <returns>
        /// An observable representing the progress of the command.
        /// </returns>
        public async Task Save()
        {
            var path = _saveLoadManager.CurrentFilePath ??
                await OpenSaveFileDialog();

            if (path != null)
            {
                await SaveWithErrorHandling(path);
            }
        }

        /// <summary>
        /// Opens a save file dialog window and saves to a new path.
        /// </summary>
        public async Task SaveAs()
        {
            var dialogResult = await OpenSaveFileDialog();

            if (dialogResult != null)
            {
                await SaveWithErrorHandling(dialogResult);
            }
        }

        /// <summary>
        /// Returns the observable result of the OpenResetDialog method.
        /// </summary>
        private async Task Reset()
        {
            var result = await _dialogService.ShowDialogAsync<bool>(
                _messageBoxFactory("Warning",
                "Resetting the tracker will set all items and locations back to their " +
                "starting values. This cannot be undone.\n\nDo you wish to proceed?"));

            if (result)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _resetManager.Reset();
                });
            }
        }

        /// <summary>
        /// Undoes the last action.
        /// </summary>
        private async Task Undo()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _undoRedoManager.Undo();
            });
        }

        /// <summary>
        /// Redoes the last action.
        /// </summary>
        private async Task Redo()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _undoRedoManager.Redo();
            });
        }

        /// <summary>
        /// Opens the AutoTracker dialog window.
        /// </summary>
        private async Task AutoTracker()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _dialogService.ShowDialogAsync(_autoTrackerDialog, false).Wait();
            });
        }

        /// <summary>
        /// Opens the Sequence Break dialog window.
        /// </summary>
        private async Task SequencesBreak()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _dialogService.ShowDialogAsync(_sequenceBreakDialog, false).Wait();
            });
        }

        /// <summary>
        /// Toggles whether to show the item counts on the map.
        /// </summary>
        private void ToggleShowItemCountsOnMap()
        {
            _appSettings.Tracker.ShowItemCountsOnMap =
                !_appSettings.Tracker.ShowItemCountsOnMap;
        }

        /// <summary>
        /// Sets the layout orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new layout orientation value.
        /// </param>
        private void SetLayoutOrientation(string orientationString)
        {
            if (orientationString == "Dynamic")
            {
                _appSettings.Layout.LayoutOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                _appSettings.Layout.LayoutOrientation = orientation;
            }
        }

        /// <summary>
        /// Sets the map orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new map orientation value.
        /// </param>
        private void SetMapOrientation(string orientationString)
        {
            if (orientationString == "Dynamic")
            {
                _appSettings.Layout.MapOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                _appSettings.Layout.MapOrientation = orientation;
            }
        }

        /// <summary>
        /// Sets the horizontal UI panel orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new horizontal UI panel orientation value.
        /// </param>
        private void SetHorizontalUIPanelPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                _appSettings.Layout.HorizontalUIPanelPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the vertical UI panel orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new vertical UI panel orientation value.
        /// </param>
        private void SetVerticalUIPanelPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                _appSettings.Layout.VerticalUIPanelPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the horizontal items placement orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new horizontal items placement orientation value.
        /// </param>
        private void SetHorizontalItemsPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                _appSettings.Layout.HorizontalItemsPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the vertical items placement orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new vertical items placement orientation value.
        /// </param>
        private void SetVerticalItemsPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                _appSettings.Layout.VerticalItemsPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the UI scale to the specified value.
        /// </summary>
        /// <param name="uiScaleValue">
        /// A floating point number representing the UI scale value.
        /// </param>
        private void SetUIScale(string uiScaleValue)
        {
            _appSettings.Layout.UIScale = double.Parse(uiScaleValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Toggles whether to display all locations on the map.
        /// </summary>
        private void ToggleDisplayAllLocations()
        {
            _appSettings.Tracker.DisplayAllLocations =
                !_appSettings.Tracker.DisplayAllLocations;
        }

        /// <summary>
        /// Toggles whether to display maps and compasses.
        /// </summary>
        private void ToggleDisplayMapsCompasses()
        {
            _appSettings.Layout.DisplayMapsCompasses =
                !_appSettings.Layout.DisplayMapsCompasses;
        }

        /// <summary>
        /// Toggles whether to always display dungeon items.
        /// </summary>
        private void ToggleAlwaysDisplayDungeonItems()
        {
            _appSettings.Layout.AlwaysDisplayDungeonItems =
                !_appSettings.Layout.AlwaysDisplayDungeonItems;
        }

        /// <summary>
        /// Opens the Color Select dialog window.
        /// </summary>
        private async Task ColorSelect()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _dialogService.ShowDialogAsync(_colorSelectDialog, false).Wait();
            });
        }

        /// <summary>
        /// Opens the About dialog window.
        /// </summary>
        private async Task About()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _dialogService.ShowDialogAsync(_aboutDialog, false).Wait();
            });
        }
    }
}
