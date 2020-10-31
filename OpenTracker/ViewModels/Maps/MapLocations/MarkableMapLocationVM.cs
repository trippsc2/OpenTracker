using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
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
    /// This is the ViewModel for the map location control representing a markable location.
    /// </summary>
    public class MarkableMapLocationVM : MapLocationVMBase, IClickHandler, IDoubleClickHandler,
        IPointerOver
    {
        private readonly MapLocation _mapLocation;
        private readonly Dock _entranceMarkingDock;
        private readonly Dock _nonEntranceMarkingDock;
        private PinnedLocationVM _pinnedLocation;

        private Dock _markingDock;
        public Dock MarkingDock
        {
            get => _markingDock;
            set => this.RaiseAndSetIfChanged(ref _markingDock, value);
        }

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
                    Dock.Left => _mapLocation.X - (Size / 2) - 55,
                    Dock.Right => _mapLocation.X - (Size / 2),
                    _ => Math.Min(_mapLocation.X - (Size / 2), _mapLocation.X - 27.5)
                };

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
                double y = MarkingDock switch
                {
                    Dock.Bottom => _mapLocation.Y - (Size / 2),
                    Dock.Top => _mapLocation.Y - (Size / 2) - 55,
                    _ => Math.Min(_mapLocation.Y - (Size / 2), _mapLocation.Y - 27.5)
                };

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
                    switch (_mapLocation.Location.ID)
                    {
                        case LocationID.EasternPalace:
                        case LocationID.DesertPalace:
                        case LocationID.TowerOfHera:
                        case LocationID.PalaceOfDarkness:
                        case LocationID.SwampPalace:
                        case LocationID.SkullWoods:
                        case LocationID.ThievesTown:
                        case LocationID.IcePalace:
                        case LocationID.MiseryMire:
                        case LocationID.TurtleRock:
                        case LocationID.GanonsTower:
                            {
                                return 130.0;
                            }
                        default:
                            {
                                return 90.0;
                            }
                    }
                }

                return 70.0;
            }
        }
        public bool Visible =>
            _mapLocation.Requirement.Met && (AppSettings.Instance.Tracker.DisplayAllLocations ||
            (_mapLocation.Location.Accessibility != AccessibilityLevel.Cleared &&
            _mapLocation.Location.Accessibility != AccessibilityLevel.None));

        public MarkingMapLocationVM Marking { get; }
        public MapLocationToolTipVM ToolTip { get; }

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
        /// <param name="marking">
        /// The marking ViewModel.
        /// </param>
        /// <param name="entranceMarkingDock">
        /// The dock direction when entrance shuffle is enabled.
        /// </param>
        /// <param name="nonEntranceMarkingDock">
        /// The dock direction when entrance shuffle is disabled.
        /// </param>
        public MarkableMapLocationVM(
            MapLocation mapLocation, MarkingMapLocationVM marking, Dock entranceMarkingDock,
            Dock nonEntranceMarkingDock)
        {
            _mapLocation = mapLocation ?? throw new ArgumentNullException(nameof(mapLocation));
            Marking = marking ?? throw new ArgumentNullException(nameof(marking));
            ToolTip = new MapLocationToolTipVM(_mapLocation.Location);
            _entranceMarkingDock = entranceMarkingDock;
            _nonEntranceMarkingDock = nonEntranceMarkingDock;

            PropertyChanged += OnPropertyChanged;

            foreach (var section in _mapLocation.Location.Sections)
            {
                section.PropertyChanged += OnSectionChanged;
            }

            AppSettings.Instance.Tracker.PropertyChanged += OnTrackerSettingsChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
            AppSettings.Instance.Colors.AccessibilityColors.PropertyChanged += OnColorChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
            _mapLocation.Location.PropertyChanged += OnLocationChanged;
            _mapLocation.Requirement.PropertyChanged += OnRequirementChanged;

            UpdateMarkingDock();
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

            if (e.PropertyName == nameof(MarkingDock))
            {
                UpdateSize();
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
                UpdateMarkingDock();
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
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarkableSection.Marking))
            {
                UpdateVisibility();
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
        /// Updates the ImageDock property based on whether entrance shuffle is on.
        /// </summary>
        private void UpdateMarkingDock()
        {
            if (Mode.Instance.EntranceShuffle == EntranceShuffle.All)
            {
                MarkingDock = _entranceMarkingDock;
            }
            else
            {
                MarkingDock = _nonEntranceMarkingDock;
            }
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
        /// Handles the pointer entering the control and highlights it.
        /// </summary>
        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Handles the pointer exiting the control and unhighlights it.
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
