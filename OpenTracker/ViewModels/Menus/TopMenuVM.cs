using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Layout;
using Newtonsoft.Json;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
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
    private readonly IThemeManager _themeManager;

    private readonly AutoTrackerDialogVM _autoTrackerDialog;
    private readonly SequenceBreakDialogVM _sequenceBreakDialog;
    private readonly ColorSelectDialogVM _colorSelectDialog;
    private readonly AboutDialogVM _aboutDialog;

    private IUndoRedoManager UndoRedoManager { get; }

    public List<MenuItemVM> Items { get; }

    public Interaction<Unit, string?> OpenFileDialogInteraction { get; } = new(RxApp.MainThreadScheduler);
    public Interaction<Unit, string?> SaveFileDialogInteraction { get; } = new(RxApp.MainThreadScheduler);
    public Interaction<ErrorBoxDialogVM, Unit> OpenErrorBoxInteraction { get; } = new(RxApp.MainThreadScheduler);
    public Interaction<MessageBoxDialogVM, bool> OpenMessageBoxInteraction { get; } = new(RxApp.MainThreadScheduler);
    public Interaction<ViewModel, Unit> OpenDialogInteraction { get; } = new(RxApp.MainThreadScheduler);
    public Interaction<Unit, Unit> RequestCloseInteraction { get; } = new(RxApp.MainThreadScheduler);

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveAsCommand { get; }
    public ReactiveCommand<Unit, Unit> ResetCommand { get; }

    public ReactiveCommand<Unit, Unit> UndoCommand { get; }
    public ReactiveCommand<Unit, Unit> RedoCommand { get; }

    public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public TopMenuVM(
        IAppSettings appSettings,
        IResetManager resetManager,
        ISaveLoadManager saveLoadManager,
        IUndoRedoManager undoRedoManager,
        IThemeManager themeManager,
        AutoTrackerDialogVM autoTrackerDialog,
        ColorSelectDialogVM colorSelectDialog,
        SequenceBreakDialogVM sequenceBreakDialog,
        AboutDialogVM aboutDialog,
        IMenuItemFactory menuItemFactory)
    {
        _appSettings = appSettings;
        _resetManager = resetManager;
        _saveLoadManager = saveLoadManager;
        UndoRedoManager = undoRedoManager;

        _themeManager = themeManager;

        _autoTrackerDialog = autoTrackerDialog;
        _colorSelectDialog = colorSelectDialog;
        _sequenceBreakDialog = sequenceBreakDialog;
        _aboutDialog = aboutDialog;

        Items = menuItemFactory.GetMenuItems();

        var fileMenuItem = Items.First(x => x.Header is "File");

        var openMenuItem = fileMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Open..." });
        var saveMenuItem = fileMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Save..." });
        var saveAsMenuItem = fileMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Save As..." });
        var resetMenuItem = fileMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Reset..." });
        var closeMenuItem = fileMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Close" });

        var trackerMenuItem = Items.First(x => x.Header is "Tracker");

        var undoMenuItem = trackerMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Undo" });
        var redoMenuItem = trackerMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Redo" });
        var autoTrackerMenuItem = trackerMenuItem.Items
            .First(x => x.Header is "Auto-Tracker...");
        var sequenceBreaksMenuItem = trackerMenuItem.Items
            .First(x => x.Header is "Sequence Breaks...");

        var viewMenuItem = Items.First(x => x.Header is "View");

        var themeMenuItems = viewMenuItem.Items
            .First(x => x.Header is "Theme")
            .Items;
        var displayAllLocationsMenuItem = viewMenuItem.Items
            .First(x => x.Header is MenuHotkeyHeaderVM { Header: "Display All Locations" });
        var showItemCountsOnMapMenuItem = viewMenuItem.Items
            .First(x => x.Header is "Show Item Counts on Map");
        var displayMapsCompassesMenuItem = viewMenuItem.Items
            .First(x => x.Header is "Display Maps/Compass");
        var alwaysDisplayDungeonItemsMenuItem = viewMenuItem.Items
            .First(x => x.Header is "Always Display Dungeon Items");
        var changeColorsMenuItem = viewMenuItem.Items
            .First(x => x.Header is "Change Colors...");
        var layoutOrientationMenuItems = viewMenuItem.Items
            .First(x => x.Header is "Layout Orientation")
            .Items;
        var horizontalOrientationMenuItem = viewMenuItem.Items
            .First(x => x.Header is "Horizontal Orientation");
        var horizontalUIPanelPlacementMenuItems = horizontalOrientationMenuItem.Items
            .First(x => x.Header is "UI Panel Placement")
            .Items;
        var horizontalItemsPanelPlacementMenuItems = horizontalOrientationMenuItem.Items
            .First(x => x.Header is "Items Panel Placement")
            .Items;
        var verticalOrientationMenuItem = viewMenuItem.Items
            .First(x => x.Header is "Vertical Orientation");
        var verticalUIPanelPlacementMenuItems = verticalOrientationMenuItem.Items
            .First(x => x.Header is "UI Panel Placement")
            .Items;
        var verticalItemsPanelPlacementMenuItems = verticalOrientationMenuItem.Items
            .First(x => x.Header is "Items Panel Placement")
            .Items;
        var mapOrientationMenuItems = viewMenuItem.Items
            .First(x => x.Header is "Map Orientation")
            .Items;
        var uiScaleMenuItems = viewMenuItem.Items
            .First(x => x.Header is "UI Scale")
            .Items;
        var aboutMenuItem = viewMenuItem.Items
            .First(x => x.Header is "About...");

        OpenCommand = ReactiveCommand.CreateFromTask(OpenAsync);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
        SaveAsCommand = ReactiveCommand.CreateFromTask(SaveAsAsync);
        ResetCommand = ReactiveCommand.CreateFromTask(ResetAsync);
        var closeCommand = ReactiveCommand.CreateFromTask(CloseAsync);

        var canUndo = this.WhenAnyValue(x => x.UndoRedoManager.CanUndo);
        UndoCommand = ReactiveCommand.Create(UndoRedoManager.Undo, canUndo);
        var canRedo = this.WhenAnyValue(x => x.UndoRedoManager.CanRedo);
        RedoCommand = ReactiveCommand.Create(UndoRedoManager.Redo, canRedo);
        var autoTrackerCommand = ReactiveCommand.CreateFromTask(AutoTrackerAsync);
        var sequenceBreaksCommand = ReactiveCommand.CreateFromTask(SequenceBreaksAsync);

        var changeThemeCommand = ReactiveCommand.Create<Theme>(ChangeTheme);
        ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);
        var toggleShowItemCountsOnMapCommand = ReactiveCommand.Create(ToggleShowItemCountsOnMap);
        var toggleDisplayMapsCompassesCommand = ReactiveCommand.Create(ToggleDisplayMapsCompasses);
        var toggleAlwaysDisplayDungeonItemsCommand = ReactiveCommand.Create(ToggleAlwaysDisplayDungeonItems);
        var changeColorsCommand = ReactiveCommand.CreateFromTask(ChangeColorsAsync);
        var changeLayoutOrientationCommand = ReactiveCommand.Create<Orientation?>(ChangeLayoutOrientation);
        var changeHorizontalUIPanelPlacementCommand = ReactiveCommand.Create<Dock>(ChangeHorizontalUIPanelPlacement);
        var changeHorizontalItemsPlacementCommand = ReactiveCommand.Create<Dock>(ChangeHorizontalItemsPlacement);
        var changeVerticalUIPanelPlacementCommand = ReactiveCommand.Create<Dock>(ChangeVerticalUIPanelPlacement);
        var changeVerticalItemsPlacementCommand = ReactiveCommand.Create<Dock>(ChangeVerticalItemsPlacement);
        var changeMapOrientationCommand = ReactiveCommand.Create<Orientation?>(ChangeMapOrientation);
        var changeUIScaleCommand = ReactiveCommand.Create<double>(ChangeUIScale);
        var aboutCommand = ReactiveCommand.CreateFromTask(AboutAsync);

        openMenuItem.Command = OpenCommand;
        saveMenuItem.Command = SaveCommand;
        saveAsMenuItem.Command = SaveAsCommand;
        resetMenuItem.Command = ResetCommand;
        closeMenuItem.Command = closeCommand;

        undoMenuItem.Command = UndoCommand;
        redoMenuItem.Command = RedoCommand;
        autoTrackerMenuItem.Command = autoTrackerCommand;
        sequenceBreaksMenuItem.Command = sequenceBreaksCommand;

        foreach (var themeMenuItem in themeMenuItems)
        {
            themeMenuItem.Command = changeThemeCommand;
        }

        displayAllLocationsMenuItem.Command = ToggleDisplayAllLocationsCommand;
        showItemCountsOnMapMenuItem.Command = toggleShowItemCountsOnMapCommand;
        displayMapsCompassesMenuItem.Command = toggleDisplayMapsCompassesCommand;
        alwaysDisplayDungeonItemsMenuItem.Command = toggleAlwaysDisplayDungeonItemsCommand;
        changeColorsMenuItem.Command = changeColorsCommand;

        foreach (var layoutOrientationMenuItem in layoutOrientationMenuItems)
        {
            layoutOrientationMenuItem.Command = changeLayoutOrientationCommand;
        }

        foreach (var horizontalUIPanelPlacementMenuItem in horizontalUIPanelPlacementMenuItems)
        {
            horizontalUIPanelPlacementMenuItem.Command = changeHorizontalUIPanelPlacementCommand;
        }

        foreach (var horizontalItemsPanelPlacementMenuItem in horizontalItemsPanelPlacementMenuItems)
        {
            horizontalItemsPanelPlacementMenuItem.Command = changeHorizontalItemsPlacementCommand;
        }

        foreach (var verticalUIPanelPlacementMenuItem in verticalUIPanelPlacementMenuItems)
        {
            verticalUIPanelPlacementMenuItem.Command = changeVerticalUIPanelPlacementCommand;
        }

        foreach (var verticalItemsPanelPlacementMenuItem in verticalItemsPanelPlacementMenuItems)
        {
            verticalItemsPanelPlacementMenuItem.Command = changeVerticalItemsPlacementCommand;
        }

        foreach (var mapOrientationMenuItem in mapOrientationMenuItems)
        {
            mapOrientationMenuItem.Command = changeMapOrientationCommand;
        }

        foreach (var uiScaleMenuItem in uiScaleMenuItems)
        {
            uiScaleMenuItem.Command = changeUIScaleCommand;
        }

        aboutMenuItem.Command = aboutCommand;
    }


    private async Task OpenAsync()
    {
        var dialogResult = await OpenFileDialogInteraction.Handle(Unit.Default);

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
            var message = ex switch
            {
                JsonReaderException _ => "The selected file is not a valid JSON file.",
                UnauthorizedAccessException _ =>
                    "The file cannot be read.  Check the permissions on the selected file.",
                _ => ex.Message
            };

            await OpenErrorBoxAsync(message).ConfigureAwait(true);
        }
    }
    
    private async Task SaveAsync()
    {
        if (_saveLoadManager.CurrentFilePath is null)
        {
            await SaveAsAsync().ConfigureAwait(true);
            return;
        }
        
        await SaveWithErrorHandling(_saveLoadManager.CurrentFilePath).ConfigureAwait(false);
    }

    private async Task SaveAsAsync()
    {
        var result = await SaveFileDialogInteraction.Handle(Unit.Default); 

        if (result is null)
        {
            return;
        }

        await SaveWithErrorHandling(result).ConfigureAwait(true);
    }

    private async Task ResetAsync()
    {
        var result = await OpenMessageBoxInteraction.Handle(
            new MessageBoxDialogVM("Warning",
                "Resetting the tracker will set all items and locations back to their " +
                "starting values. This cannot be undone.\n\nDo you wish to proceed?"));

        if (result)
        {
            _resetManager.ResetAsync();
        }
    }

    private async Task CloseAsync()
    {
        await RequestCloseInteraction.Handle(Unit.Default);
    }

    private async Task AutoTrackerAsync()
    {
        await OpenDialogInteraction.Handle(_autoTrackerDialog);
    }

    private async Task SequenceBreaksAsync()
    {
        await OpenDialogInteraction.Handle(_sequenceBreakDialog);
    }

    private void ChangeTheme(Theme theme)
    {
        _themeManager.SelectedTheme = theme;
    }

    private void ToggleDisplayAllLocations()
    {
        _appSettings.Tracker.DisplayAllLocations = !_appSettings.Tracker.DisplayAllLocations;
    }

    private void ToggleShowItemCountsOnMap()
    {
        _appSettings.Tracker.ShowItemCountsOnMap = !_appSettings.Tracker.ShowItemCountsOnMap;
    }

    private void ChangeLayoutOrientation(Orientation? newValue)
    {
        _appSettings.Layout.LayoutOrientation = newValue;
    }

    private void ChangeMapOrientation(Orientation? newValue)
    {
        _appSettings.Layout.MapOrientation = newValue;
    }

    private void ChangeHorizontalUIPanelPlacement(Dock newValue)
    {
        _appSettings.Layout.HorizontalUIPanelPlacement = newValue;
    }

    private void ChangeVerticalUIPanelPlacement(Dock newValue)
    {
        _appSettings.Layout.VerticalUIPanelPlacement = newValue;
    }

    private void ChangeHorizontalItemsPlacement(Dock newValue)
    {
        _appSettings.Layout.HorizontalItemsPlacement = newValue;
    }

    private void ChangeVerticalItemsPlacement(Dock newValue) 
    {
        _appSettings.Layout.VerticalItemsPlacement = newValue;
    }

    private void ChangeUIScale(double newValue)
    {
        _appSettings.Layout.UIScale = newValue;
    }
    
    private void ToggleDisplayMapsCompasses()
    {
        _appSettings.Layout.DisplayMapsCompasses = !_appSettings.Layout.DisplayMapsCompasses;
    }

    private void ToggleAlwaysDisplayDungeonItems()
    {
        _appSettings.Layout.AlwaysDisplayDungeonItems = !_appSettings.Layout.AlwaysDisplayDungeonItems;
    }

    private async Task ChangeColorsAsync()
    {
        await OpenDialogInteraction.Handle(_colorSelectDialog);
    }

    private async Task AboutAsync()
    {
        await OpenDialogInteraction.Handle(_aboutDialog);
    }

    private async Task SaveWithErrorHandling(string path)
    {
        try
        {
            _saveLoadManager.Save(path);
        }
        catch (Exception ex)
        {
            var message = ex switch
            {
                UnauthorizedAccessException _ =>
                    "Unable to save to the selected directory.  Check the file permissions and try again.",
                _ => ex.Message
            };
                
            await OpenErrorBoxAsync(message);
        }
    }
    
    private async Task OpenErrorBoxAsync(string message)
    {
        await OpenErrorBoxInteraction.Handle(new ErrorBoxDialogVM("Error", message));
    }
}