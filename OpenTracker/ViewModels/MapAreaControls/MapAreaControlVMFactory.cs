using OpenTracker.Models.Locations;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.MapAreaControls
{
    public static class MapAreaControlVMFactory
    {
        public static ObservableCollection<MapControlVM> GetMapControlVMs(MapAreaControlVM mapArea)
        {
            if (mapArea == null)
            {
                throw new ArgumentNullException(nameof(mapArea));
            }

            ObservableCollection<MapControlVM> maps = new ObservableCollection<MapControlVM>();

            foreach (MapID map in Enum.GetValues(typeof(MapID)))
            {
                maps.Add(new MapControlVM(map, mapArea));
            }

            return maps;
        }

        public static ObservableCollection<MapLocationControlVMBase> GetMapLocationControlVMs(
            MapAreaControlVM mapArea, ObservableCollection<PinnedLocationControlVM> pinnedLocations)
        {
            if (mapArea == null)
            {
                throw new ArgumentNullException(nameof(mapArea));
            }

            if (pinnedLocations == null)
            {
                throw new ArgumentNullException(nameof(pinnedLocations));
            }

            ObservableCollection<MapLocationControlVMBase> mapLocations =
                new ObservableCollection<MapLocationControlVMBase>();

            foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
            {
                var location = LocationDictionary.Instance[id];

                foreach (var mapLocation in location.MapLocations)
                {
                    mapLocations.Add(MapLocationControlVMFactory.GetMapLocationControlVM(
                        mapLocation, mapArea, pinnedLocations));
                }
            }

            return mapLocations;
        }
    }
}
