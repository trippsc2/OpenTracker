﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Interfaces;
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

namespace OpenTracker.ViewModels.Maps.Locations
{
    /// <summary>
    /// This is the ViewModel of the map location control representing a entrance map location.
    /// </summary>
    public class EntranceMapLocationVM : ViewModelBase, IMapLocationVMBase, IClickHandler,
        IConnectLocation, IDoubleClickHandler, IPointerOver
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

        public delegate EntranceMapLocationVM Factory(
            IMapLocation mapLocation, IMarkingMapLocationVM marking, Dock markingDock,
            List<Point> points);

        /// <summary>
        /// Constructor
        /// </summary>
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
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Highlighted))
            {
                this.RaisePropertyChanged(nameof(BorderColor));
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
        /// Subscribes to the PropertyChanged event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMarkingChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
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
            _undoRedoManager.Execute(_undoableFactory.GetPinLocation(MapLocation.Location!));
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
            _undoRedoManager.Execute(_undoableFactory.GetAddConnection(_connectionFactory(
                    location.MapLocation, MapLocation)));
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
            _undoRedoManager.Execute(new ClearLocation(MapLocation.Location!, force));
        }
    }
}