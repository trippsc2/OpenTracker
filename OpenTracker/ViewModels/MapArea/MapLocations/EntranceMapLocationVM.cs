using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.ViewModels.UIPanels.LocationsPanel;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.MapArea.MapLocations
{
    /// <summary>
    /// This is the ViewModel of the map location control representing a entrance map location.
    /// </summary>
    public class EntranceMapLocationVM : MapLocationVMBase, IClickHandler,
        IConnectLocation, IDoubleClickHandler, IPointerOver
    {
        private readonly MapLocation _mapLocation;
        private readonly MapAreaControlVM _mapArea;
        private readonly ObservableCollection<PinnedLocationVM> _pinnedLocations;
        private PinnedLocationVM _pinnedLocation;

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
                    Dock.Left => _mapLocation.X - 84,
                    Dock.Right => _mapLocation.X,
                    _ => _mapLocation.X - 28,
                };

                if (_mapArea.MapPanelOrientation == Orientation.Vertical)
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
                    Dock.Bottom => _mapLocation.Y,
                    Dock.Top => _mapLocation.Y - 84,
                    _ => _mapLocation.Y - 28,
                };

                if (_mapArea.MapPanelOrientation == Orientation.Vertical)
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

        public bool Visible =>
            _mapLocation.Requirement.Met && (AppSettings.Instance.DisplayAllLocations ||
                (_mapLocation.Location.Sections[0] is IMarkableSection markableSection &&
                markableSection.Marking != null) ||
                _mapLocation.Location.Accessibility != AccessibilityLevel.Cleared);

        public MarkingMapLocationVM Marking { get; }
        public List<Point> Points { get; }

        public string Color =>
            AppSettings.Instance.AccessibilityColors[_mapLocation.Location.Accessibility];
        public string BorderColor =>
            Highlighted ? "#FFFFFFFF" : "#FF000000";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapLocation">
        /// The map location to be represented.
        /// </param>
        /// <param name="mapArea">
        /// The map area ViewModel parent class.
        /// </param>
        /// <param name="pinnedLocations">
        /// The observable collection of pinned locations.
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
            MapLocation mapLocation, MapAreaControlVM mapArea,
            ObservableCollection<PinnedLocationVM> pinnedLocations,
            MarkingMapLocationVM marking, Dock markingDock, List<Point> points)
        {
            _mapArea = mapArea ?? throw new ArgumentNullException(nameof(mapArea));
            _mapLocation = mapLocation ?? throw new ArgumentNullException(nameof(mapLocation));
            _pinnedLocations = pinnedLocations ?? throw new ArgumentNullException(nameof(pinnedLocations));
            Marking = marking ?? throw new ArgumentNullException(nameof(marking));
            MarkingDock = markingDock;
            Points = points ?? throw new ArgumentNullException(nameof(points));

            foreach (var section in _mapLocation.Location.Sections)
            {
                section.PropertyChanged += OnSectionChanged;
            }

            PropertyChanged += OnPropertyChanged;
            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
            AppSettings.Instance.AccessibilityColors.PropertyChanged += OnColorChanged;
            _mapArea.PropertyChanged += OnMapAreaChanged;
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
        /// Subscribes to the PropertyChanged event on the MapAreaControlVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMapAreaChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MapAreaControlVM.MapPanelOrientation))
            {
                this.RaisePropertyChanged(nameof(CanvasX));
                this.RaisePropertyChanged(nameof(CanvasY));
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
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.DisplayAllLocations))
            {
                UpdateVisibility();
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
        /// Subscribes to the PropertyChanged event on the Location class.
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
                UpdateVisibility();
                UpdateColor();
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
            if (_pinnedLocation == null)
            {
                _pinnedLocation = PinnedLocationVMFactory.GetLocationControlVM(
                    _mapLocation.Location, _pinnedLocations);
            }

            UndoRedoManager.Instance.Execute(new PinLocation(_pinnedLocation, _pinnedLocations));
        }

        /// <summary>
        /// Handles pointer entering the control and highlights the control.
        /// </summary>
        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Handles pointer exiting the control and unhighlights the control.
        /// </summary>
        public void OnPointerLeave()
        {
            Highlighted = false;
        }

        /// <summary>
        /// Connects this entrance location to the specified location.
        /// </summary>
        /// <param name="location">
        /// The location to which this location is connected.
        /// </param>
        public void ConnectLocation(IConnectLocation location)
        {
            if (location == null)
            {
                return;
            }

            if (location is EntranceMapLocationVM entrance)
            {
                UndoRedoManager.Instance.Execute(new AddConnection(new Connection(
                    entrance._mapLocation, _mapLocation)));
            }
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
