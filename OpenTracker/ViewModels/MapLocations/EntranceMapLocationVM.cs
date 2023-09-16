using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations;

/// <summary>
/// This class contains the entrance map location control ViewModel data.
/// </summary>
public class EntranceMapLocationVM : ViewModelBase, IEntranceMapLocationVM
{
    private readonly IMapConnectionCollection _connections;
    private readonly IUndoRedoManager _undoRedoManager;

    private readonly IMapLocationColorProvider _colorProvider;

    public IMapLocation MapLocation { get; }
    public double OffsetX { get; }
    public double OffsetY { get; }
    public List<Point> Points { get; } 

    public string BorderColor => _colorProvider.BorderColor;
    public string Color => _colorProvider.Color;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
    public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClick { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnter { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeave { get; }

    public delegate EntranceMapLocationVM Factory(
        IMapLocation mapLocation, double offsetX, double offsetY, List<Point> points);

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
        IMapConnectionCollection connections, IUndoRedoManager undoRedoManager,
        IMapLocationColorProvider.Factory colorProvider, IMapLocation mapLocation, double offsetX, double offsetY,
        List<Point> points)
    {
        _undoRedoManager = undoRedoManager;

        _colorProvider = colorProvider(mapLocation);

        MapLocation = mapLocation;
        OffsetX = offsetX;
        OffsetY = offsetY;
        Points = points;
        _connections = connections;

        HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
        HandleDoubleClick = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClickImpl);
        HandlePointerEnter = _colorProvider.HandlePointerEnter;
        HandlePointerLeave = _colorProvider.HandlePointerLeave;

        _colorProvider.PropertyChanged += OnColorProviderChanged;
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the IMapLocationColorProvider interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnColorProviderChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IMapLocationColorProvider.BorderColor):
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(BorderColor)));
                break;
            case nameof(IMapLocationColorProvider.Color):
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Color)));
                break;
        }
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

    /// <summary>
    /// Creates an undoable action to clear the location and sends it to the undo/redo manager.
    /// </summary>
    /// <param name="force">
    /// A boolean representing whether the logic should be ignored.
    /// </param>
    private void ClearLocation(bool force)
    {
        _undoRedoManager.NewAction(MapLocation.Location.CreateClearLocationAction(force));
    }

    /// <summary>
    /// Creates an undoable action to pin the location and sends it to the undo/redo manager.
    /// </summary>
    private void PinLocation()
    {
        _undoRedoManager.NewAction(MapLocation.Location.CreatePinLocationAction());
    }

    /// <summary>
    /// Handles clicking the control.
    /// </summary>
    /// <param name="e">
    /// The pointer released event args.
    /// </param>
    private void HandleClickImpl(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Right)
        {
            ClearLocation((e.KeyModifiers & KeyModifiers.Control) > 0);
        }
    }

    /// <summary>
    /// Handles double clicking the control.
    /// </summary>
    /// <param name="e">
    /// The pointer released event args.
    /// </param>
    private void HandleDoubleClickImpl(RoutedEventArgs e)
    {
        PinLocation();
    }
}