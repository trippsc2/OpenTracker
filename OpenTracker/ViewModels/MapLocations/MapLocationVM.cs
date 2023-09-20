using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.ToolTips;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.MapLocations;

/// <summary>
/// This class contains the map location control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MapLocationVM : ViewModel, IMapLocationVM
{
    private IAppSettings AppSettings { get; }
    private IMapLocation MapLocation { get; }
    private IRequirement? DockRequirement { get; }

    public IMapLocationMarkingVM? Marking { get; }
    public IShapedMapLocationVMBase Location { get; }
    public IMapLocationToolTipVM ToolTip { get; }

    [ObservableAsProperty]
    public bool Visible { get; }
    [ObservableAsProperty]
    public Dock MarkingDock { get; }
    [ObservableAsProperty]
    private double BaseX { get; }
    [ObservableAsProperty]
    private double BaseY { get; }
    [ObservableAsProperty]
    private double MarkingOffsetX { get; }
    [ObservableAsProperty]
    private double MarkingOffsetY { get; }
    [ObservableAsProperty]
    public double CanvasX { get; }
    [ObservableAsProperty]
    public double CanvasY { get; }

    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="appSettings">
    ///     The app settings data.
    /// </param>
    /// <param name="mapLocation">
    ///     The map location data to be represented.
    /// </param>
    /// <param name="dockRequirement">
    ///     The requirement to be met to change the dock direction.
    /// </param>
    /// <param name="marking">
    ///     The map location marking control.
    /// </param>
    /// <param name="location">
    ///     The location shape control.
    /// </param>
    /// <param name="toolTip">
    ///     The tooltip control.
    /// </param>
    /// <param name="metDock">
    ///     The marking dock direction when the requirement is met.
    /// </param>
    /// <param name="unmetDock">
    ///     The default marking dock direction.
    /// </param>
    public MapLocationVM(
        IAppSettings appSettings,
        IMapLocation mapLocation,
        IRequirement? dockRequirement,
        IMapLocationMarkingVM? marking,
        IShapedMapLocationVMBase location,
        IMapLocationToolTipVM toolTip,
        Dock metDock,
        Dock unmetDock)
    {
        AppSettings = appSettings;
        MapLocation = mapLocation;
        DockRequirement = dockRequirement;

        Marking = marking;
        Location = location;
        ToolTip = toolTip;

        HandlePointerEnterCommand = Location.HandlePointerEnterCommand;
        HandlePointerLeaveCommand = Location.HandlePointerLeaveCommand;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.MapLocation.IsActive,
                    x => x.AppSettings.Tracker.DisplayAllLocations,
                    x => x.MapLocation.ShouldBeDisplayed,
                    (isActive, displayAllLocations, shouldBeDisplayed) =>
                        isActive && (displayAllLocations || shouldBeDisplayed))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Visible)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.DockRequirement,
                    x => x.DockRequirement!.Met,
                    (requirement, _) => requirement?.Met ?? false ? metDock : unmetDock)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.MarkingDock, initialValue: unmetDock)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.AppSettings.Layout.CurrentMapOrientation,
                    x => x.MapLocation.Map,
                    x => x.MapLocation.X,
                    (orientation, map, x) =>
                        orientation == Orientation.Vertical
                            ? x + 23.0
                            : map == MapID.DarkWorld
                                ? x + 2046.0
                                : x + 13.0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BaseX)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.AppSettings.Layout.CurrentMapOrientation,
                    x => x.MapLocation.Map,
                    x => x.MapLocation.Y,
                    (orientation, map, y) =>
                        orientation == Orientation.Horizontal
                            ? y + 23.0
                            : map == MapID.DarkWorld
                                ? y + 2046.0
                                : y + 13.0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BaseY)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Marking,
                    x => x.MarkingDock,
                    x => x.Location.OffsetX,
                    (mark, dock, offsetX) =>
                        mark is null
                            ? 0.0
                            : dock == Dock.Right
                                ? 0.0
                                : dock == Dock.Left
                                    ? -56.0
                                    : Math.Min(0.0, -28.0 - offsetX))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.MarkingOffsetX)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Marking,
                    x => x.MarkingDock,
                    x => x.Location.OffsetY,
                    (mark, dock, offsetY) =>
                        mark is null
                            ? 0.0
                            : dock == Dock.Bottom
                                ? 0.0
                                : dock == Dock.Top
                                    ? -56.0
                                    : Math.Min(0.0, -28.0 - offsetY))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.MarkingOffsetY)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.BaseX,
                    x => x.Location.OffsetX,
                    x => x.MarkingOffsetX,
                    (baseX, offsetX, markingOffsetX) => baseX + offsetX + markingOffsetX)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.CanvasX)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.BaseY,
                    x => x.Location.OffsetY,
                    x => x.MarkingOffsetY,
                    (baseY, offsetY, markingOffsetY) => baseY + offsetY + markingOffsetY)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.CanvasY)
                .DisposeWith(disposables);
        });
    }
}