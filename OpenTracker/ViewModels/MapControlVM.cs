using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class MapControlVM : ViewModelBase
    {
        private readonly Game _game;
        private readonly MapID _iD;

        public ObservableCollection<MapLocationControlVM> MapLocations { get; }

        public string ImageSource 
        {
            get
            {
                WorldState worldState;

                if (_game.Mode.WorldState == WorldState.Inverted)
                    worldState = WorldState.Inverted;
                else
                    worldState = WorldState.StandardOpen;

                return "avares://OpenTracker/Assets/Images/Maps/" +
                    worldState.ToString().ToLowerInvariant() + "_" +
                    _iD.ToString().ToLowerInvariant() + ".png";
            }
        }

        public MapControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, MainWindowVM mainWindow, MapID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _iD = iD;

            game.Mode.PropertyChanged += OnModeChanged;

            MapLocations = new ObservableCollection<MapLocationControlVM>();

            foreach (Location location in game.Locations.Values)
            {
                foreach (MapLocation mapLocation in location.MapLocations)
                {
                    if (mapLocation.Map == iD)
                    {
                        MapLocations.Add(new MapLocationControlVM(undoRedoManager, appSettings,
                            game, mainWindow, mapLocation));
                    }
                }
            }
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
                this.RaisePropertyChanged(nameof(ImageSource));
        }
    }
}
