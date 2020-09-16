using Avalonia;
using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.ViewModels.PinnedLocations;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.Maps.MapLocations
{
    /// <summary>
    /// This is the ViewModel for the map location control representing a basic location.
    /// </summary>
    public class MapLocationVM : MapLocationVMBase, IClickHandler, IDoubleClickHandler,
        IPointerOver
    {
        private readonly MapLocation _mapLocation;
        private PinnedLocationVM _pinnedLocation;

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
                double x = _mapLocation.X - (Size / 2);

                if (AppSettings.Instance.Layout.CurrentMapOrientation == Orientation.Vertical)
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
                double y = _mapLocation.Y - (Size / 2);

                if (AppSettings.Instance.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    if (_mapLocation.Map == MapID.DarkWorld)
                    {
                        return y + 2046;
                    }

                    return y + 13;
                }

                return y + 23;
            }
        }
        public double Size
        {
            get
            {
                if (Mode.Instance.EntranceShuffle == EntranceShuffle.All)
                {
                    return 40.0;
                }

                if (_mapLocation.Location.Total > 1)
                {
                    return 90.0;
                }
                
                return 70.0;
            }
        }
        public bool Visible =>
            _mapLocation.Requirement.Met && (AppSettings.Instance.Tracker.DisplayAllLocations ||
            (_mapLocation.Location.Accessibility != AccessibilityLevel.Cleared &&
            _mapLocation.Location.Accessibility != AccessibilityLevel.None));
        public string Color =>
            AppSettings.Instance.Colors.AccessibilityColors[_mapLocation.Location.Accessibility];
        public static Thickness BorderSize =>
            Mode.Instance.EntranceShuffle == EntranceShuffle.All ? new Thickness(5) : new Thickness(9);
        public string BorderColor =>
            Highlighted ? "#ffffffff" : "#ff000000";
        public bool TextVisible =>
            Mode.Instance.EntranceShuffle < EntranceShuffle.All &&
            AppSettings.Instance.Tracker.ShowItemCountsOnMap &&
            _mapLocation.Location.Available != 0 && _mapLocation.Location.Total > 1;
        public string Text
        {
            get
            {
                if (Mode.Instance.EntranceShuffle == EntranceShuffle.All ||
                    !AppSettings.Instance.Tracker.ShowItemCountsOnMap ||
                    _mapLocation.Location.Available == 0 || _mapLocation.Location.Total <= 1)
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapLocation">
        /// The map location being represented.
        /// </param>
        public MapLocationVM(MapLocation mapLocation)
        {
            _mapLocation = mapLocation ?? throw new ArgumentNullException(nameof(mapLocation));

            PropertyChanged += OnPropertyChanged;

            AppSettings.Instance.Tracker.PropertyChanged += OnTrackerSettingsChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
            AppSettings.Instance.Colors.AccessibilityColors.PropertyChanged += OnColorChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
            _mapLocation.Location.PropertyChanged += OnLocationChanged;
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
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                UpdateSize();
                UpdateText();
                this.RaisePropertyChanged(nameof(BorderSize));
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

            if (e.PropertyName == nameof(ILocation.Accessible) ||
                e.PropertyName == nameof(ILocation.Available))
            {
                UpdateText();
            }

            if (e.PropertyName == nameof(ILocation.Total))
            {
                UpdateSize();
                UpdateText();
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

            if (e.PropertyName == nameof(TrackerSettings.ShowItemCountsOnMap))
            {
                UpdateText();
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
                UpdatePosition();
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
        /// Raises the PropertyChanged event for the CanvasX and CanvasY properties.
        /// </summary>
        private void UpdatePosition()
        {
            this.RaisePropertyChanged(nameof(CanvasX));
            this.RaisePropertyChanged(nameof(CanvasY));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Size, CanvasX, and CanvaxY properties.
        /// </summary>
        private void UpdateSize()
        {
            this.RaisePropertyChanged(nameof(Size));
            UpdatePosition();
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
        /// Raises the PropertyChanged event for the TextVisible and Text properties.
        /// </summary>
        private void UpdateText()
        {
            this.RaisePropertyChanged(nameof(TextVisible));
            this.RaisePropertyChanged(nameof(Text));
        }

        /// <summary>
        /// Handles double clicks and pins the location.
        /// </summary>
        public void OnDoubleClick()
        {
            if (_pinnedLocation == null)
            {
                _pinnedLocation = PinnedLocationVMFactory.GetLocationControlVM(
                    _mapLocation.Location);
            }
            
            UndoRedoManager.Instance.Execute(new PinLocation(_pinnedLocation));
        }

        /// <summary>
        /// Handles pointer entering the control and highlights it.
        /// </summary>
        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Handles pointer exiting the control and unhighlights it.
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
            UndoRedoManager.Instance.Execute(new ClearLocation(_mapLocation.Location, force));
        }
    }
}
