using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class MapControlVM : ViewModelBase
    {
        private readonly Game _game;
        private readonly MapID _iD;

        public ObservableCollection<MapLocationControlVM> MapLocations { get; }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            private set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public MapControlVM(UndoRedoManager undoRedoManager, AppSettingsVM appSettings,
            Game game, MainWindowVM mainWindow, MapID iD)
        {
            _game = game;
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

            UpdateMap();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
                UpdateMap();
        }

        private void UpdateMap()
        {
            ImageSource = "avares://OpenTracker/Assets/Images/Maps/" +
                _game.Mode.WorldState.Value.ToString().ToLower() + "_" +
                _iD.ToString().ToLower() + ".png";
        }
    }
}
