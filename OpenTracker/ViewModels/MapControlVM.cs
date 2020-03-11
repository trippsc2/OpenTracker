using OpenTracker.Enums;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels
{
    public class MapControlVM : ViewModelBase
    {
        private readonly MapID _iD;
        private readonly AppSettingsVM _appSettings;
        private readonly Game _game;

        public string ImageSource { get; }
        public ObservableCollection<MapLocationControlVM> MapLocations { get; }

        public MapControlVM(AppSettingsVM appSettings, Game game, MapID iD)
        {
            _appSettings = appSettings;
            _game = game;
            _iD = iD;
            ImageSource = "avares://OpenTracker/Assets/Images/Maps/" + _iD.ToString().ToLower() + ".png";

            MapLocations = new ObservableCollection<MapLocationControlVM>();

            foreach (ILocation location in _game.Locations.Values)
            {
                foreach (LocationPlacement coord in location.Placements)
                {
                    if (coord.Map == _iD)
                    {
                        MapLocations.Add(new MapLocationControlVM(_appSettings, _game, coord));
                    }
                }
            }
        }
    }
}
