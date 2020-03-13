using OpenTracker.Models;
using OpenTracker.Models.Enums;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels
{
    public class MapControlVM : ViewModelBase
    {
        public string ImageSource { get; }
        public ObservableCollection<MapLocationControlVM> MapLocations { get; }

        public MapControlVM(AppSettingsVM appSettings, Game game,
            MainWindowVM mainWindow, MapID iD)
        {
            ImageSource = "avares://OpenTracker/Assets/Images/Maps/" + iD.ToString().ToLower() + ".png";

            MapLocations = new ObservableCollection<MapLocationControlVM>();

            foreach (Location location in game.Locations.Values)
            {
                foreach (MapLocation mapLocation in location.MapLocations)
                {
                    if (mapLocation.Map == iD)
                        MapLocations.Add(new MapLocationControlVM(appSettings, game, mainWindow, mapLocation));
                }
            }
        }
    }
}
