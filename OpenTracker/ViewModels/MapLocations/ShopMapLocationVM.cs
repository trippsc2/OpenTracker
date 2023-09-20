using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.MapLocations;

/// <summary>
/// This class contains the shop map location control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class ShopMapLocationVM : ViewModel, IShapedMapLocationVMBase
{
    private readonly IUndoRedoManager _undoRedoManager;
    private readonly IMapLocation _mapLocation;
    
    private IMapLocationColorProvider ColorProvider { get; }

    public double OffsetX => -20.0;
    public double OffsetY => -20.0;
    [ObservableAsProperty]
    public SolidColorBrush BorderColor => ColorProvider.BorderColor;
    [ObservableAsProperty]
    public SolidColorBrush Color => ColorProvider.Color;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }
    public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClickCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

    public delegate ShopMapLocationVM Factory(IMapLocation mapLocation);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="colorProvider">
    /// The map location control color provider.
    /// </param>
    /// <param name="mapLocation">
    /// The map location data.
    /// </param>
    public ShopMapLocationVM(
        IUndoRedoManager undoRedoManager, IMapLocationColorProvider.Factory colorProvider, IMapLocation mapLocation)
    {
        _undoRedoManager = undoRedoManager;
        _mapLocation = mapLocation;
        ColorProvider = colorProvider(mapLocation);
        
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
        _undoRedoManager.NewAction(_mapLocation.Location.CreateClearLocationAction(force));
    }

    private void PinLocation()
    {
        _undoRedoManager.NewAction(_mapLocation.Location.CreatePinLocationAction());
    }
}