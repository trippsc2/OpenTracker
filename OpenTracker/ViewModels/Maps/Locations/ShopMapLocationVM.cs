using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Maps.Locations.Tooltip;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps.Locations
{
    public class ShopMapLocationVM : ViewModelBase, IMapLocationVMBase, IClickHandler,
        IDoubleClickHandler, IPointerOver
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

        public IMapLocationToolTipVM ToolTip { get; }

        public delegate ShopMapLocationVM Factory(IMapLocation mapLocation);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapLocation">
        /// The map location being represented.
        /// </param>
        public ShopMapLocationVM(
            IAppSettings appSettings, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, IMapLocationToolTipVM.Factory tooltipFactory,
            IMapLocation mapLocation)
        {
            _appSettings = appSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _mapLocation = mapLocation;

            ToolTip = tooltipFactory(_mapLocation.Location!);

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
        /// Handles double clicks and pins the location.
        /// </summary>
        public void OnDoubleClick()
        {
            _undoRedoManager.Execute(_undoableFactory.GetPinLocation(_mapLocation.Location!));
        }

        /// <summary>
        /// Handles the pointer entering the control and highlights it.
        /// </summary>
        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Handles the pointer exiting and unhighlights it.
        /// </summary>
        public void OnPointerLeave()
        {
            Highlighted = false;
        }

        /// <summary>
        /// Handles left clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
        }

        /// <summary>
        /// Handles right clicks and clears the location.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            _undoRedoManager.Execute(_undoableFactory.GetClearLocation(
                _mapLocation.Location!, force));
        }
    }
}
