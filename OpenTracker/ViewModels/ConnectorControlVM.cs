using Avalonia;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class ConnectorControlVM : ViewModelBase, IClickHandler, IPointerOver
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Game _game;
        private readonly AppSettings _appSettings;

        private bool _highlighted;
        public bool Highlighted
        {
            get => _highlighted;
            private set => this.RaiseAndSetIfChanged(ref _highlighted, value);
        }

        public (MapLocation, MapLocation) Connection { get; }

        public (MapID, Point) Start => (Connection.Item1.Map,
            new Point(Connection.Item1.X, Connection.Item1.Y));

        public (MapID, Point) End => (Connection.Item2.Map,
            new Point(Connection.Item2.X, Connection.Item2.Y));

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

        public ConnectorControlVM(UndoRedoManager undoRedoManager, Game game, AppSettings appSettings,
            (MapLocation, MapLocation) connection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game;
            _appSettings = appSettings;

            Connection = connection;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Highlighted))
                this.RaisePropertyChanged(nameof(Color));
        }

        private void RemoveConnector()
        {
            _undoRedoManager.Execute(new RemoveConnection(_game, Connection));
        }

        public void OnLeftClick()
        {
        }

        public void OnRightClick()
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
