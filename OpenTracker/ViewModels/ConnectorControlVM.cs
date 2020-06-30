using Avalonia;
using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model of the connection control between entrance locations.
    /// </summary>
    public class ConnectorControlVM : ViewModelBase, IClickHandler, IPointerOver
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Game _game;
        private readonly MainWindowVM _mainWindow;
        private readonly AppSettings _appSettings;

        private bool _highlighted;
        public bool Highlighted
        {
            get => _highlighted;
            private set => this.RaiseAndSetIfChanged(ref _highlighted, value);
        }

        public bool Visible =>
            _game.Mode.EntranceShuffle;

        public (MapLocation, MapLocation) Connection { get; }

        public Point Start
        {
            get
            {
                return _mainWindow.MapPanelOrientation switch
                {
                    Orientation.Vertical =>
                        Connection.Item1.Map == MapID.DarkWorld ?
                        new Point(Connection.Item1.X + 23, Connection.Item1.Y + 2046) :
                        new Point(Connection.Item1.X + 23, Connection.Item1.Y + 13),
                    _ => Connection.Item1.Map == MapID.DarkWorld ?
                        new Point(Connection.Item1.X + 2046, Connection.Item1.Y + 23) :
                        new Point(Connection.Item1.X + 13, Connection.Item1.Y + 23),
                };
            }
        }

        public Point End
        {
            get
            {
                return _mainWindow.MapPanelOrientation switch
                {
                    Orientation.Vertical =>
                        Connection.Item2.Map == MapID.DarkWorld ?
                        new Point(Connection.Item2.X + 23, Connection.Item2.Y + 2046) :
                        new Point(Connection.Item2.X + 23, Connection.Item2.Y + 13),
                    _ => Connection.Item2.Map == MapID.DarkWorld ?
                        new Point(Connection.Item2.X + 2046, Connection.Item2.Y + 23) :
                        new Point(Connection.Item2.X + 13, Connection.Item2.Y + 23),
                };
            }
        }

        public string Color
        {
            get
            {
                if (Highlighted)
                    return "#ffffffff";
                else
                    return _appSettings.ConnectorColor;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="mainWindow">
        /// The main window view-model parent class.
        /// </param>
        /// <param name="appSettings">
        /// The app settings data.
        /// </param>
        /// <param name="connection">
        /// The connection data.
        /// </param>
        public ConnectorControlVM(UndoRedoManager undoRedoManager, Game game, MainWindowVM mainWindow,
            AppSettings appSettings, (MapLocation, MapLocation) connection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

            Connection = connection;

            PropertyChanged += OnPropertyChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            _game.Mode.PropertyChanged += OnModeChanged;
            _appSettings.PropertyChanged += OnAppSettingsChanged;
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
        /// Subscribes to the PropertyChanged event on the MainWindowVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.MapPanelOrientation))
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
            UpdateColor();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Color property.
        /// </summary>
        private void UpdateColor()
        {
            this.RaisePropertyChanged(nameof(Color));
        }

        /// <summary>
        /// Removes the connector from the collection.
        /// </summary>
        private void RemoveConnector()
        {
            _undoRedoManager.Execute(new RemoveConnection(_game, Connection));
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            RemoveConnector();
        }

        /// <summary>
        /// Handler for when the pointer enters the control.
        /// </summary>
        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Handler for when the pointer exits the control.
        /// </summary>
        public void OnPointerLeave()
        {
            Highlighted = false;
        }
    }
}
