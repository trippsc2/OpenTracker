using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.MapLocations;

/// <summary>
/// This class contains the standard (square) map location control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class StandardMapLocationVM : ViewModel, IShapedMapLocationVMBase
{
    private readonly IUndoRedoManager _undoRedoManager;
    
    private IMode Mode { get; }
    private TrackerSettings TrackerSettings { get; }
    private IMapLocationColorProvider ColorProvider { get; }
    private IMapLocation MapLocation { get; }
    private bool IsDungeon { get; }

    [ObservableAsProperty]
    public double Size { get; }
    [ObservableAsProperty]
    public double OffsetX { get; }
    [ObservableAsProperty]
    public double OffsetY { get; }
    [ObservableAsProperty]
    public SolidColorBrush BorderColor { get; } = default!;
    [ObservableAsProperty]
    public SolidColorBrush Color { get; } = default!;
    [ObservableAsProperty]
    public Thickness BorderSize { get; }
    [ObservableAsProperty]
    public string? Label { get; }
    [ObservableAsProperty]
    public bool LabelVisible { get; }

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }
    public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClickCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

    public delegate StandardMapLocationVM Factory(IMapLocation mapLocation);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="trackerSettings">
    /// The tracker settings data.
    /// </param>
    /// <param name="mode">
    /// The mode settings data.
    /// </param>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="colorProvider">
    /// The control color provider.
    /// </param>
    /// <param name="mapLocation">
    /// The map location data.
    /// </param>
    public StandardMapLocationVM(
        TrackerSettings trackerSettings,
        IMode mode,
        IUndoRedoManager undoRedoManager,
        IMapLocationColorProvider.Factory colorProvider,
        IMapLocation mapLocation)
    {
        TrackerSettings = trackerSettings;
        _undoRedoManager = undoRedoManager;

        Mode = mode;
        ColorProvider = colorProvider(mapLocation);
        MapLocation = mapLocation;
        IsDungeon = MapLocation.Location.Sections[0] is IDungeonItemSection;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        HandleDoubleClickCommand = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClick);
        HandlePointerEnterCommand = ColorProvider.HandlePointerEnterCommand;
        HandlePointerLeaveCommand = ColorProvider.HandlePointerLeaveCommand;

        this.WhenActivated(disposables =>
        {
            ColorProvider.Activator
                .Activate()
                .DisposeWith(disposables);
            
            this.WhenAnyValue(
                    x => x.Mode.EntranceShuffle,
                    x => x.IsDungeon,
                    x => x.MapLocation.Location.Total,
                    (entranceShuffle, isDungeon, total) => 
                        entranceShuffle > EntranceShuffle.Dungeon || isDungeon && entranceShuffle == EntranceShuffle.Dungeon
                            ? 40.0
                            : total > 1
                                ? isDungeon
                                    ? 130.0
                                    : 90.0
                                : 70.0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Size)
                .DisposeWith(disposables);
            var offset = this
                .WhenAnyValue(x => x.Size)
                .Select(x => -(x / 2))
                .ObserveOn(RxApp.MainThreadScheduler);

            offset
                .ToPropertyEx(this, x => x.OffsetX)
                .DisposeWith(disposables);
            offset
                .ToPropertyEx(this, x => x.OffsetY)
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.ColorProvider.BorderColor)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BorderColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.ColorProvider.Color)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Color)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Size)
                .Select(x => x > 40.0 ? new Thickness(9) : new Thickness(5))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BorderSize)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.MapLocation.Location.Available,
                    x => x.MapLocation.Location.Accessible,
                    (available, accessible) => available == 0
                        ? null
                        : available == accessible
                            ? available.ToString().ToLowerInvariant()
                            : $"{accessible.ToString().ToLowerInvariant()}/{available.ToString().ToLowerInvariant()}")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Label)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.TrackerSettings.ShowItemCountsOnMap,
                    x => x.Size,
                    x => x.Label,
                    (showItemCountsOnMap, size, label) =>
                        showItemCountsOnMap && size > 70.0 && label is not null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.LabelVisible)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Right)
        {
            ClearLocation((e.KeyModifiers & KeyModifiers.Control) > 0);
        }
    }

    private void HandleDoubleClick(RoutedEventArgs e)
    {
        PinLocation();
    }

    private void ClearLocation(bool force)
    {
        _undoRedoManager.NewAction(MapLocation.Location.CreateClearLocationAction(force));
    }

    private void PinLocation()
    {
        _undoRedoManager.NewAction(MapLocation.Location.CreatePinLocationAction());
    }
}