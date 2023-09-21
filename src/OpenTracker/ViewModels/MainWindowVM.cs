using System.Reactive;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.Areas;
using OpenTracker.ViewModels.Menus;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels;

/// <summary>
/// Represents main window view model.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class MainWindowVM : ViewModel
{
    private const string TitlePrefix = "OpenTracker - ";
    private const string DefaultTitlePrefix = "OpenTracker - Unsaved";
    
    private readonly AppSettings _appSettings;
    private readonly BoundsSettings _boundsSettings;
    private readonly IJsonConverter _jsonConverter;
    
    private LayoutSettings LayoutSettings { get; }
    private ISaveLoadManager SaveLoadManager { get; }

    [ObservableAsProperty]
    public string Title { get; } = DefaultTitlePrefix;
    
    public bool? Maximized
    {
        get => _boundsSettings.Maximized;
        set => _boundsSettings.Maximized = value;
    }
    public double? X
    {
        get => _boundsSettings.X;
        set => _boundsSettings.X = value;
    }
    public double? Y
    {
        get => _boundsSettings.Y;
        set => _boundsSettings.Y = value;
    }
    public double? Width
    {
        get => _boundsSettings.Width;
        set => _boundsSettings.Width = value;
    }
    public double? Height
    {
        get => _boundsSettings.Height;
        set => _boundsSettings.Height = value;
    }

    [ObservableAsProperty]
    public Dock UIDock { get; }

    public ITopMenuVM TopMenu { get; }
    public StatusBarVM StatusBar { get; }
    public IUIPanelAreaVM UIPanel { get; }
    public IMapAreaVM MapArea { get; }
    
    public Interaction<Unit, Unit> RequestCloseInteraction { get; } = new(RxApp.MainThreadScheduler);

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveAsCommand { get; }
    public ReactiveCommand<Unit, Unit> ResetCommand { get; }
    public ReactiveCommand<Unit, Unit> UndoCommand { get; }
    public ReactiveCommand<Unit, Unit> RedoCommand { get; }
    public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
    public ReactiveCommand<Rect, Unit> ChangeLayoutCommand { get; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="appSettings">
    /// The app settings data.
    /// </param>
    /// <param name="boundsSettings">
    /// The bounds settings data.
    /// </param>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="jsonConverter">
    ///     The JSON converter.
    /// </param>
    /// <param name="saveLoadManager">
    /// The save/load manager.
    /// </param>
    /// <param name="topMenu">
    /// The top menu control.
    /// </param>
    /// <param name="statusBar">
    /// The status bar control.
    /// </param>
    /// <param name="uiPanel">
    /// The UI panel control.
    /// </param>
    /// <param name="mapArea">
    /// The map area control.
    /// </param>
    public MainWindowVM(
        AppSettings appSettings,
        BoundsSettings boundsSettings,
        LayoutSettings layoutSettings,
        IJsonConverter jsonConverter,
        ISaveLoadManager saveLoadManager,
        ITopMenuVM topMenu,
        StatusBarVM statusBar,
        IUIPanelAreaVM uiPanel,
        IMapAreaVM mapArea)
    {
        _appSettings = appSettings;
        _boundsSettings = boundsSettings;
        _jsonConverter = jsonConverter;

        LayoutSettings = layoutSettings;
        SaveLoadManager = saveLoadManager;
        
        TopMenu = topMenu;
        StatusBar = statusBar;
        UIPanel = uiPanel;
        MapArea = mapArea;

        OpenCommand = TopMenu.OpenCommand;
        SaveCommand = TopMenu.SaveCommand;
        SaveAsCommand = TopMenu.SaveAsCommand;
        ResetCommand = TopMenu.ResetCommand;
        UndoCommand = TopMenu.UndoCommand;
        RedoCommand = TopMenu.RedoCommand;
        ToggleDisplayAllLocationsCommand = TopMenu.ToggleDisplayAllLocationsCommand;
        ChangeLayoutCommand = ReactiveCommand.Create<Rect>(ChangeLayout);

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.SaveLoadManager.CurrentFilePath,
                    x => x.SaveLoadManager.Unsaved,
                    (path, unsaved) =>
                        path is null
                            ? DefaultTitlePrefix
                            : TitlePrefix + path + (unsaved ? "*" : ""))
                .ToPropertyEx(this, x => x.Title)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.LayoutSettings.CurrentLayoutOrientation,
                    x => x.LayoutSettings.HorizontalUIPanelPlacement,
                    x => x.LayoutSettings.VerticalUIPanelPlacement,
                    (orientation, horizontal, vertical) =>
                        orientation == Orientation.Horizontal ? horizontal : vertical)
                .ToPropertyEx(this, x => x.UIDock)
                .DisposeWith(disposables);
        });
    }

    private void ChangeLayout(Rect bounds)
    {
        var orientation = bounds.Width > bounds.Height
            ? Orientation.Horizontal
            : Orientation.Vertical;
        
        LayoutSettings.CurrentDynamicOrientation = orientation;
    }

    public void OnClose(bool maximized, Rect bounds, PixelPoint pixelPoint)
    {
        SaveAppSettings(maximized, bounds, pixelPoint);
    }

    private void SaveAppSettings(bool maximized, Rect bounds, PixelPoint pixelPoint)
    {
        _boundsSettings.Maximized = maximized;
        _boundsSettings.X = pixelPoint.X;
        _boundsSettings.Y = pixelPoint.Y;
        _boundsSettings.Width = bounds.Width;
        _boundsSettings.Height = bounds.Height;

        _jsonConverter.Save(_appSettings.Save(), AppPath.AppSettingsFilePath);
    }
}