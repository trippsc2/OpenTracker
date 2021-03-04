using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Maps.Locations.Tooltip;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.Maps.Locations
{
    /// <summary>
    /// This class contains the entrance map location control ViewModel data.
    /// </summary>
    public class EntranceMapLocationVM : ViewModelBase, IEntranceMapLocationVM
    {
        private readonly IAppSettings _appSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;
        private readonly IConnection.Factory _connectionFactory;

        public IMapLocation MapLocation { get; }

        public Dock MarkingDock { get; }

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
                double x = MarkingDock switch
                {
                    Dock.Left => MapLocation.X - 84,
                    Dock.Right => MapLocation.X,
                    _ => MapLocation.X - 28,
                };

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    return x + 23;
                }
                
                if (MapLocation.Map == MapID.DarkWorld)
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
                double y = MarkingDock switch
                {
                    Dock.Bottom => MapLocation.Y,
                    Dock.Top => MapLocation.Y - 84,
                    _ => MapLocation.Y - 28,
                };

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    if (MapLocation.Map == MapID.DarkWorld)
                    {
                        return y + 2046;
                    }
                    
                    return y + 13;
                }
                
                return y + 23;
            }
        }
        public bool Visible =>
            MapLocation.Requirement.Met && (_appSettings.Tracker.DisplayAllLocations ||
            (MapLocation.Location!.Sections[0] is IMarkableSection markableSection &&
            markableSection.Marking.Mark != MarkType.Unknown) ||
            MapLocation.Location.Accessibility != AccessibilityLevel.Cleared);

        public IMarkingMapLocationVM Marking { get; }
        public List<Point> Points { get; }

        public string Color =>
            _appSettings.Colors.AccessibilityColors[MapLocation.Location!.Accessibility];
        public string BorderColor =>
            Highlighted ? "#FFFFFFFF" : "#FF000000";

        public IMapLocationToolTipVM ToolTip { get; }

        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }
        public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClickCommand { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

        public delegate EntranceMapLocationVM Factory(
            IMapLocation mapLocation, IMarkingMapLocationVM marking, Dock markingDock,
            List<Point> points);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings">
        /// The app settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="tooltipFactory">
        /// An Autofac factory for creating tooltips.
        /// </param>
        /// <param name="connectionFactory">
        /// An Autofac factory for creating map connections.
        /// </param>
        /// <param name="mapLocation">
        /// The map location to be represented.
        /// </param>
        /// <param name="marking">
        /// The marking ViewModel.
        /// </param>
        /// <param name="markingDock">
        /// The marking dock.
        /// </param>
        /// <param name="points">
        /// The list of points for the triangle polygon.
        /// </param>
        public EntranceMapLocationVM(
            IAppSettings appSettings, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory,  IMapLocationToolTipVM.Factory tooltipFactory,
            IConnection.Factory connectionFactory, IMapLocation mapLocation,
            IMarkingMapLocationVM marking, Dock markingDock, List<Point> points)
        {
            _appSettings = appSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;
            _connectionFactory = connectionFactory;

            MapLocation = mapLocation;

            Marking = marking;
            MarkingDock = markingDock;
            Points = points;

            ToolTip = tooltipFactory(MapLocation.Location!);

            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
            HandleDoubleClickCommand = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClick);
            HandlePointerEnterCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerEnter);
            HandlePointerLeaveCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerLeave);

            foreach (var section in MapLocation.Location!.Sections)
            {
                ((IMarkableSection)section).Marking.PropertyChanged += OnMarkingChanged;
                section.PropertyChanged += OnSectionChanged;
            }

            PropertyChanged += OnPropertyChanged;
            _appSettings.Tracker.PropertyChanged += OnTrackerSettingsChanged;
            _appSettings.Layout.PropertyChanged += OnLayoutChanged;
            _appSettings.Colors.AccessibilityColors.PropertyChanged += OnColorChanged;
            MapLocation.Location.PropertyChanged += OnLocationChanged;
            MapLocation.Requirement.PropertyChanged += OnRequirementChanged;
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
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    this.RaisePropertyChanged(nameof(CanvasX));
                    this.RaisePropertyChanged(nameof(CanvasY));
                });
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnMarkingChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
            {
                await UpdateVisibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarkableSection.Marking))
            {
                await UpdateVisibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Location class.
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
                await UpdateVisibility();
                await UpdateColor();
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
        /// Creates an undoable action to create a connection to the specified location and sends it to the undo/redo
        /// manager.
        /// </summary>
        /// <param name="mapLocation">
        /// The map location to which this location is connected.
        /// </param>
        public void ConnectLocation(IMapLocation mapLocation)
        {
            _undoRedoManager.NewAction(_undoableFactory.GetAddConnection(_connectionFactory(mapLocation, MapLocation)));
        }

        /// <summary>
        /// Creates an undoable action to clear the location and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        private void ClearLocation(bool force)
        {
            _undoRedoManager.NewAction(_undoableFactory.GetClearLocation(MapLocation.Location!, force));
        }

        /// <summary>
        /// Creates an undoable action to pin the location and sends it to the undo/redo manager.
        /// </summary>
        private void PinLocation()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetPinLocation(MapLocation.Location!));
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
        private void HandleClick(PointerReleasedEventArgs e)
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
        private void HandleDoubleClick(RoutedEventArgs e)
        {
            PinLocation();
        }

        /// <summary>
        /// Handles pointer entering the control.
        /// </summary>
        /// <param name="e">
        /// The PointerEnter event args.
        /// </param>
        private void HandlePointerEnter(PointerEventArgs e)
        {
            Highlight();
        }

        /// <summary>
        /// Handles pointer leaving the control.
        /// </summary>
        /// <param name="e">
        /// The PointerLeave event args.
        /// </param>
        private void HandlePointerLeave(PointerEventArgs e)
        {
            Unhighlight();
        }
    }
}
