using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations
{
    /// <summary>
    /// This class contains the shop map location control ViewModel data.
    /// </summary>
    public class ShopMapLocationVM : ViewModelBase, IShapedMapLocationVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;
        
        private readonly IMapLocationColorProvider _colorProvider;
        private readonly IMapLocation _mapLocation;

        public double OffsetX { get; } = -20.0;
        public double OffsetY { get; } = -20.0;
        public string BorderColor => _colorProvider.BorderColor;
        public string Color => _colorProvider.Color;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
        public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClick { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnter { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeave { get; }

        public delegate ShopMapLocationVM Factory(IMapLocation mapLocation);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="colorProvider">
        /// The map location control color provider.
        /// </param>
        /// <param name="mapLocation">
        /// The map location data.
        /// </param>
        public ShopMapLocationVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IMapLocationColorProvider.Factory colorProvider, IMapLocation mapLocation)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;
            
            _colorProvider = colorProvider(mapLocation);
            _mapLocation = mapLocation;

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
        /// Creates an undoable action to clear the location and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        private void ClearLocation(bool force)
        {
            _undoRedoManager.NewAction(_undoableFactory.GetClearLocation(_mapLocation.Location!, force));
        }

        /// <summary>
        /// Creates an undoable action to pin the location and sends it to the undo/redo manager.
        /// </summary>
        private void PinLocation()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetPinLocation(_mapLocation.Location!));
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
}