using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations
{
    /// <summary>
    /// This class contains the standard (square) map location control ViewModel data.
    /// </summary>
    public class StandardMapLocationVM : ViewModelBase, IShapedMapLocationVMBase
    {
        private readonly ITrackerSettings _trackerSettings;
        private readonly IMode _mode;
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IMapLocationColorProvider _colorProvider;
        private readonly IMapLocation _mapLocation;
        
        private readonly bool _dungeon;

        public double OffsetX => -(Size / 2);
        public double OffsetY => -(Size / 2);

        public double Size
        {
            get
            {
                if (_mode.EntranceShuffle > EntranceShuffle.Dungeon ||
                    _dungeon && _mode.EntranceShuffle == EntranceShuffle.Dungeon)
                {
                    return 40.0;
                }

                if (_mapLocation.Location.Total > 1)
                {
                    return _dungeon ? 130.0 : 90.0;
                }

                return 70.0;
            }
        }

        public string BorderColor => _colorProvider.BorderColor;
        public string Color => _colorProvider.Color;

        public Thickness BorderSize => Size > 40.0 ? new Thickness(9) : new Thickness(5);
        
        public string? Label
        {
            get
            {
                if (_mapLocation.Location.Available == 0)
                {
                    return null;
                }
                
                if (_mapLocation.Location.Available == _mapLocation.Location.Accessible ||
                    _mapLocation.Location.Accessible == 0)
                {
                    return _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
                }

                return $"{ _mapLocation.Location.Accessible.ToString(CultureInfo.InvariantCulture) }/" +
                       _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
            }
        }

        public bool LabelVisible => _trackerSettings.ShowItemCountsOnMap && Size > 70.0 && Label is not null;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
        public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClick { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnter { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeave { get; }

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
            ITrackerSettings trackerSettings, IMode mode, IUndoRedoManager undoRedoManager,
            IMapLocationColorProvider.Factory colorProvider, IMapLocation mapLocation)
        {
            _trackerSettings = trackerSettings;
            _mode = mode;
            _undoRedoManager = undoRedoManager;

            _colorProvider = colorProvider(mapLocation);
            _mapLocation = mapLocation;

            _dungeon = _mapLocation.Location.Sections[0] is IDungeonItemSection;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
            HandleDoubleClick = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClickImpl);
            HandlePointerEnter = _colorProvider.HandlePointerEnter;
            HandlePointerLeave = _colorProvider.HandlePointerLeave;

            PropertyChanged += OnPropertyChanged;
            _trackerSettings.PropertyChanged += OnTrackerSettingsChanged;
            _mode.PropertyChanged += OnModeChanged;
            _colorProvider.PropertyChanged += OnColorProviderChanged;
            _mapLocation.Location.PropertyChanged += OnLocationChanged;
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Size):
                {
                    await UpdateOffsets();
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        this.RaisePropertyChanged(nameof(OffsetX));
                        this.RaisePropertyChanged(nameof(OffsetY));
                        this.RaisePropertyChanged(nameof(BorderSize));
                        this.RaisePropertyChanged(nameof(LabelVisible));
                    });
                }
                    break;
                case nameof(Label):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(LabelVisible)));
                    break;
            }
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
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(LabelVisible)));
                    break;
                case nameof(IMapLocationColorProvider.Color):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Color)));
                    break;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ITrackerSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnTrackerSettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ITrackerSettings.ShowItemCountsOnMap))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(LabelVisible)));
            }
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IMode.EntranceShuffle))
            {
                return;
            }

            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Size)));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILocation interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLocationChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ILocation.Accessible):
                case nameof(ILocation.Available):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Label)));
                    break;
                case nameof(ILocation.Total):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Size)));
                    break;
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the OffsetX and OffsetY properties.
        /// </summary>
        private async Task UpdateOffsets()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                this.RaisePropertyChanged(nameof(OffsetX));
                this.RaisePropertyChanged(nameof(OffsetY));
            });
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