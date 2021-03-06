using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Maps.Locations.Tooltip;
using ReactiveUI;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.Maps.Locations
{
    /// <summary>
    /// This class contains the basic map location control ViewModel data.
    /// </summary>
    public class MapLocationVM : ViewModelBase, IMapLocationVMBase
    {
        private readonly IAppSettings _appSettings;
        private readonly IMode _mode;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IMapLocation _mapLocation;

        private bool _highlighted;
        public bool Highlighted
        {
            get => _highlighted;
            private set => this.RaiseAndSetIfChanged(ref _highlighted, value);
        }

        public double CanvasX
        {
            get
            {
                var x = _mapLocation.X - (Size / 2);

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    return x + 23;
                }

                if (_mapLocation.Map == MapID.DarkWorld)
                {
                    return x + 2046;
                }

                return x + 13;
            }
        }
        public double CanvasY
        {
            get
            {
                var y = _mapLocation.Y - (Size / 2);

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Horizontal)
                {
                    return y + 23;
                }
                
                if (_mapLocation.Map == MapID.DarkWorld)
                {
                    return y + 2046;
                }

                return y + 13;

            }
        }
        public double Size
        {
            get
            {
                if (_mode.EntranceShuffle >= EntranceShuffle.All)
                {
                    return 40.0;
                }

                if (_mapLocation.Location!.Total > 1)
                {
                    return 90.0;
                }
                
                return 70.0;
            }
        }
        public bool Visible =>
            _mapLocation.Requirement.Met && (_appSettings.Tracker.DisplayAllLocations ||
            (_mapLocation.Location!.Accessibility != AccessibilityLevel.Cleared &&
            _mapLocation.Location.Accessibility != AccessibilityLevel.None));
        public string Color => _appSettings.Colors.AccessibilityColors[_mapLocation.Location!.Accessibility];
        public Thickness BorderSize =>
            _mode.EntranceShuffle >= EntranceShuffle.All ? new Thickness(5) : new Thickness(9);
        public string BorderColor =>
            Highlighted ? "#ffffffff" : "#ff000000";
        public bool TextVisible =>
            _mode.EntranceShuffle < EntranceShuffle.All && _appSettings.Tracker.ShowItemCountsOnMap &&
            _mapLocation.Location!.Available != 0 && _mapLocation.Location.Total > 1;
        public string? Text
        {
            get
            {
                if (_mode.EntranceShuffle == EntranceShuffle.All || !_appSettings.Tracker.ShowItemCountsOnMap ||
                    _mapLocation.Location!.Available == 0 || _mapLocation.Location.Total <= 1)
                {
                    return null;
                }

                if (_mapLocation.Location.Available == _mapLocation.Location.Accessible)
                {
                    return _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
                }
                
                if (_mapLocation.Location.Accessible == 0)
                {
                    return _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
                }
                
                return $"{ _mapLocation.Location.Accessible.ToString(CultureInfo.InvariantCulture) }/" +
                    _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
            }
        }

        public IMapLocationToolTipVM ToolTip { get; }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
        public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClick { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnter { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeave { get; }

        public delegate MapLocationVM Factory(IMapLocation mapLocation);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings">
        /// The app settings data.
        /// </param>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="tooltipFactory">
        /// An Autofac factory to create tooltips.
        /// </param>
        /// <param name="mapLocation">
        /// The map location being represented.
        /// </param>
        public MapLocationVM(
            IAppSettings appSettings, IMode mode, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IMapLocationToolTipVM.Factory tooltipFactory, IMapLocation mapLocation)
        {
            _appSettings = appSettings;
            _mode = mode;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _mapLocation = mapLocation;

            ToolTip = tooltipFactory(_mapLocation.Location!);

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
            HandleDoubleClick = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClickImpl);
            HandlePointerEnter = ReactiveCommand.Create<PointerEventArgs>(HandlePointerEnterImpl);
            HandlePointerLeave = ReactiveCommand.Create<PointerEventArgs>(HandlePointerLeaveImpl);

            PropertyChanged += OnPropertyChanged;
            _appSettings.Tracker.PropertyChanged += OnTrackerSettingsChanged;
            _appSettings.Layout.PropertyChanged += OnLayoutChanged;
            _appSettings.Colors.AccessibilityColors.PropertyChanged += OnColorChanged;
            _mode.PropertyChanged += OnModeChanged;
            _mapLocation.Location!.PropertyChanged += OnLocationChanged;
            _mapLocation.Requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on itself.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Highlighted))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(BorderColor)));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                await UpdateSize();
                await UpdateText();
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(BorderSize)));
            }
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
        private async void OnLocationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILocation.Accessibility))
            {
                await UpdateColor();
                await UpdateVisibility();
            }

            if (e.PropertyName == nameof(ILocation.Accessible) ||
                e.PropertyName == nameof(ILocation.Available))
            {
                await UpdateText();
            }

            if (e.PropertyName == nameof(ILocation.Total))
            {
                await UpdateSize();
                await UpdateText();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                await UpdateVisibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the TrackerSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnTrackerSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TrackerSettings.DisplayAllLocations))
            {
                await UpdateVisibility();
            }

            if (e.PropertyName == nameof(TrackerSettings.ShowItemCountsOnMap))
            {
                await UpdateText();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.CurrentMapOrientation))
            {
                await UpdatePosition();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection for the
        /// accessibility colors.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            await UpdateColor();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the CanvasX and CanvasY properties.
        /// </summary>
        private async Task UpdatePosition()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                this.RaisePropertyChanged(nameof(CanvasX));
                this.RaisePropertyChanged(nameof(CanvasY));
            });
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Size, CanvasX, and CanvasY properties.
        /// </summary>
        private async Task UpdateSize()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Size)));
            await UpdatePosition();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Visible property.
        /// </summary>
        private async Task UpdateVisibility()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Color property.
        /// </summary>
        private async Task UpdateColor()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Color)));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextVisible and Text properties.
        /// </summary>
        private async Task UpdateText()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                this.RaisePropertyChanged(nameof(TextVisible));
                this.RaisePropertyChanged(nameof(Text));
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
        /// Highlights the control.
        /// </summary>
        private void Highlight()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Un-highlights the control.
        /// </summary>
        private void Unhighlight()
        {
            Highlighted = false;
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

        /// <summary>
        /// Handles pointer entering the control.
        /// </summary>
        /// <param name="e">
        /// The PointerEnter event args.
        /// </param>
        private void HandlePointerEnterImpl(PointerEventArgs e)
        {
            Highlight();
        }

        /// <summary>
        /// Handles pointer leaving the control.
        /// </summary>
        /// <param name="e">
        /// The PointerLeave event args.
        /// </param>
        private void HandlePointerLeaveImpl(PointerEventArgs e)
        {
            Unhighlight();
        }
    }
}
