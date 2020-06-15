using Avalonia;
using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using ReactiveUI;
using SharpDX.DirectWrite;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
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

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Highlighted))
                UpdateColor();
        }

        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.MapPanelOrientation))
            {
                this.RaisePropertyChanged(nameof(Start));
                this.RaisePropertyChanged(nameof(End));
            }
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
                this.RaisePropertyChanged(nameof(Visible));
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            this.RaisePropertyChanged(nameof(Color));
        }

        private void RemoveConnector()
        {
            _undoRedoManager.Execute(new RemoveConnection(_game, Connection));
        }

        public void OnLeftClick(bool force = false)
        {
        }

        public void OnRightClick(bool force = false)
        {
            RemoveConnector();
        }

        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        public void OnPointerLeave()
        {
            Highlighted = false;
        }
    }
}
