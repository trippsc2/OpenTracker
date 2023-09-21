using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.MapLocations;

/// <summary>
/// This class contains the entrance map location control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class EntranceMapLocationVM : ViewModel, IEntranceMapLocationVM
{
    private readonly IMapConnectionCollection _connections;
    private readonly IUndoRedoManager _undoRedoManager;

    private IMapLocationColorProvider ColorProvider { get; }

    public IMapLocation MapLocation { get; }
    public double OffsetX { get; }
    public double OffsetY { get; }
    public List<Point> Points { get; }

    [ObservableAsProperty]
    public SolidColorBrush BorderColor { get; } = default!;
    [ObservableAsProperty]
    public SolidColorBrush Color { get; } = default!;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }
    public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClickCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

    public delegate EntranceMapLocationVM Factory(
        IMapLocation mapLocation,
        double offsetX,
        double offsetY,
        List<Point> points);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="connections">
    /// The connection collection.
    /// </param>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="colorProvider">
    /// The map location control color provider.
    /// </param>
    /// <param name="mapLocation">
    /// The map location data.
    /// </param>
    /// <param name="offsetX">
    /// The pixel offset on the X axis for the control.
    /// </param>
    /// <param name="offsetY">
    /// The pixel offset on the Y axes for the control.
    /// </param>
    /// <param name="points">
    /// The list of points for the polygon control.
    /// </param>
    public EntranceMapLocationVM(
        IMapConnectionCollection connections,
        IUndoRedoManager undoRedoManager,
        IMapLocationColorProvider.Factory colorProvider,
        IMapLocation mapLocation,
        double offsetX,
        double offsetY,
        List<Point> points)
    {
        _connections = connections;
        _undoRedoManager = undoRedoManager;
        ColorProvider = colorProvider(mapLocation);

        MapLocation = mapLocation;
        OffsetX = offsetX;
        OffsetY = offsetY;
        Points = points;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        HandleDoubleClickCommand = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClick);
        HandlePointerEnterCommand = ColorProvider.HandlePointerEnterCommand;
        HandlePointerLeaveCommand = ColorProvider.HandlePointerLeaveCommand;

        this.WhenActivated(disposables =>
        {
            ColorProvider.Activator
                .Activate()
                .DisposeWith(disposables);
            
            this.WhenAnyValue(x => x.ColorProvider.BorderColor)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BorderColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.ColorProvider.Color)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Color)
                .DisposeWith(disposables);
        });
    }

    /// <summary>
    /// Creates an undoable action to create a connection to the specified location and sends it to the undo/redo
    /// manager.
    /// </summary>
    /// <param name="mapLocation">
    /// The map location to which this location is connected.
    /// </param>
    public void ConnectLocation(IMapLocation mapLocation)
    {
        _undoRedoManager.NewAction(_connections.AddConnection(mapLocation, MapLocation));
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