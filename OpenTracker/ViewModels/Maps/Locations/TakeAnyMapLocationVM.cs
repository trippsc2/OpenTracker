using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.Maps.Locations
{
    /// <summary>
    /// This class contains take any map location control ViewModel data.
    /// </summary>
    public class TakeAnyMapLocationVM : ViewModelBase, IMapLocationVMBase
    {
        private readonly IAppSettings _appSettings;
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
                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    return _mapLocation.X + 3;
                }

                if (_mapLocation.Map == MapID.DarkWorld)
                {
                    return _mapLocation.X + 2026;
                }

                return _mapLocation.X - 7;
            }
        }
        public double CanvasY
        {
            get
            {
                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    if (_mapLocation.Map == MapID.DarkWorld)
                    {
                        return _mapLocation.Y + 2026;
                    }

                    return _mapLocation.Y - 7;
                }

                return _mapLocation.Y + 3;
            }
        }
        public bool Visible =>
            _mapLocation.Requirement.Met && (_appSettings.Tracker.DisplayAllLocations ||
            (_mapLocation.Location!.Accessibility != AccessibilityLevel.Cleared &&
            _mapLocation.Location.Accessibility != AccessibilityLevel.None));
        public string Color =>
            _appSettings.Colors.AccessibilityColors[_mapLocation.Location!.Accessibility];
        public string BorderColor =>
            Highlighted ? "#ffffffff" : "#ff000000";
        public string ToolTipText =>
            _mapLocation.Location!.Name;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }
        public ReactiveCommand<RoutedEventArgs, Unit> HandleDoubleClickCommand { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

        public delegate TakeAnyMapLocationVM Factory(IMapLocation mapLocation);

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
        /// <param name="mapLocation">
        /// The map location being represented.
        /// </param>
        public TakeAnyMapLocationVM(
            IAppSettings appSettings, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, IMapLocation mapLocation)
        {
            _appSettings = appSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _mapLocation = mapLocation;

            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
            HandleDoubleClickCommand = ReactiveCommand.Create<RoutedEventArgs>(HandleDoubleClick);
            HandlePointerEnterCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerEnter);
            HandlePointerLeaveCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerLeave);

            PropertyChanged += OnPropertyChanged;

            _appSettings.Tracker.PropertyChanged += OnTrackerSettingsChanged;
            _appSettings.Layout.PropertyChanged += OnLayoutChanged;
            _appSettings.Colors.AccessibilityColors.PropertyChanged += OnColorChanged;
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
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Highlighted))
            {
                this.RaisePropertyChanged(nameof(BorderColor));
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
        private void OnLocationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILocation.Accessibility))
            {
                UpdateColor();
                UpdateVisibility();
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
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateVisibility();
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
        private void OnTrackerSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TrackerSettings.DisplayAllLocations))
            {
                UpdateVisibility();
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
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.CurrentMapOrientation))
            {
                this.RaisePropertyChanged(nameof(CanvasX));
                this.RaisePropertyChanged(nameof(CanvasY));
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
        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateColor();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Visible property.
        /// </summary>
        private void UpdateVisibility()
        {
            this.RaisePropertyChanged(nameof(Visible));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Color property.
        /// </summary>
        private void UpdateColor()
        {
            this.RaisePropertyChanged(nameof(Color));
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
