using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations
{
    /// <summary>
    /// This class contains the take any map location control ViewModel data.
    /// </summary>
    public class TakeAnyMapLocationVM : ViewModelBase, IShapedMapLocationVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IMapLocationColorProvider _colorProvider;
        private readonly IMapLocation _mapLocation;

        public double OffsetX { get; } = -20.0;
        public double OffsetY { get; } = -20.0;
        public string BorderColor => _colorProvider.BorderColor;
        public string Color => _colorProvider.Color;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
        public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClick { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEntered { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerExited { get; }

        public delegate TakeAnyMapLocationVM Factory(IMapLocation mapLocation);

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
        public TakeAnyMapLocationVM(
            IUndoRedoManager undoRedoManager, IMapLocationColorProvider.Factory colorProvider, IMapLocation mapLocation)
        {
            _undoRedoManager = undoRedoManager;

            _colorProvider = colorProvider(mapLocation);
            _mapLocation = mapLocation;

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
            HandleDoubleClick = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClickImpl);
            HandlePointerEntered = _colorProvider.HandlePointerEntered;
            HandlePointerExited = _colorProvider.HandlePointerExited;

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
            _undoRedoManager.NewAction(_mapLocation.Location.CreateClearLocationAction(force));
        }

        /// <summary>
        /// Creates an undoable action to pin the location and sends it to the undo/redo manager.
        /// </summary>
        private void PinLocation()
        {
            _undoRedoManager.NewAction(_mapLocation.Location.CreatePinLocationAction());
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