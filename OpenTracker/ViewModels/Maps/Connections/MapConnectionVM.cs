﻿using Avalonia;
using Avalonia.Layout;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps.Connections
{
    /// <summary>
    /// This is the ViewModel of the control representing a connection between entrance locations.
    /// </summary>
    public class MapConnectionVM : ViewModelBase, IMapConnectionVM
    {
        private readonly IColorSettings _colorSettings;
        private readonly IMode _mode;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IMapAreaVM _mapArea;

        private bool _highlighted;
        public bool Highlighted
        {
            get => _highlighted;
            private set => this.RaiseAndSetIfChanged(ref _highlighted, value);
        }

        public bool Visible =>
            _mode.EntranceShuffle > EntranceShuffle.None;

        public object Model =>
            Connection;

        public IConnection Connection { get; }

        public Point Start =>
            _mapArea.Orientation switch
            {
                Orientation.Vertical => Connection.Location1.Map == MapID.DarkWorld ?
                    new Point(Connection.Location1.X + 23, Connection.Location1.Y + 2046) :
                    new Point(Connection.Location1.X + 23, Connection.Location1.Y + 13),
                _ => Connection.Location1.Map == MapID.DarkWorld ?
                    new Point(Connection.Location1.X + 2046, Connection.Location1.Y + 23) :
                    new Point(Connection.Location1.X + 13, Connection.Location1.Y + 23)
            };
        public Point End =>
            _mapArea.Orientation switch
            {
                Orientation.Vertical => Connection.Location2.Map == MapID.DarkWorld ?
                    new Point(Connection.Location2.X + 23, Connection.Location2.Y + 2046) :
                    new Point(Connection.Location2.X + 23, Connection.Location2.Y + 13),
                _ => Connection.Location2.Map == MapID.DarkWorld ?
                    new Point(Connection.Location2.X + 2046, Connection.Location2.Y + 23) :
                    new Point(Connection.Location2.X + 13, Connection.Location2.Y + 23)
            };
        public string Color =>
            Highlighted ? "#ffffffff" : _colorSettings.ConnectorColor;

        public delegate IMapConnectionVM Factory(IConnection connection);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">
        /// The connection data.
        /// </param>
        /// <param name="mapArea">
        /// The map area ViewModel parent class.
        /// </param>
        public MapConnectionVM(
            IColorSettings colorSettings, IMode mode, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, IMapAreaVM mapArea, IConnection connection)
        {
            _colorSettings = colorSettings;
            _mode = mode;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;
            _mapArea = mapArea;

            Connection = connection;

            PropertyChanged += OnPropertyChanged;
            _mapArea.PropertyChanged += OnMapAreaChanged;
            _mode.PropertyChanged += OnModeChanged;
            _colorSettings.PropertyChanged += OnColorsChanged;
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
                UpdateColor();
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
            if (e.PropertyName == nameof(MapAreaVM.Orientation))
            {
                this.RaisePropertyChanged(nameof(Start));
                this.RaisePropertyChanged(nameof(End));
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
                this.RaisePropertyChanged(nameof(Visible));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ColorSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ColorSettings.ConnectorColor))
            {
                UpdateColor();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Color property.
        /// </summary>
        private void UpdateColor()
        {
            this.RaisePropertyChanged(nameof(Color));
        }

        /// <summary>
        /// Handles left clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
        }

        /// <summary>
        /// Handles right clicks and removes the connection.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetRemoveConnection(Connection));
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
    }
}