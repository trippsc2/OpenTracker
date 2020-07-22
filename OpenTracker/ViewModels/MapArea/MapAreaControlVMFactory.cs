using OpenTracker.Models.Locations;
using OpenTracker.ViewModels.MapArea.MapLocations;
using OpenTracker.ViewModels.UIPanels.LocationsPanel.PinnedLocations;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.MapArea
{
    public static class MapAreaControlVMFactory
    {
        public static ObservableCollection<MapVM> GetMapControlVMs(MapAreaControlVM mapArea)
        {
            if (mapArea == null)
            {
                throw new ArgumentNullException(nameof(mapArea));
            }

            ObservableCollection<MapVM> maps = new ObservableCollection<MapVM>();

            foreach (MapID map in Enum.GetValues(typeof(MapID)))
            {
                maps.Add(new MapVM(map, mapArea));
            }

            return maps;
        }

        public static ObservableCollection<MapLocationVMBase> GetMapLocationControlVMs(
            MapAreaControlVM mapArea, ObservableCollection<PinnedLocationVM> pinnedLocations)
        {
            if (mapArea == null)
            {
                throw new ArgumentNullException(nameof(mapArea));
            }

            if (pinnedLocations == null)
            {
                throw new ArgumentNullException(nameof(pinnedLocations));
            }

            ObservableCollection<MapLocationVMBase> mapLocations =
                new ObservableCollection<MapLocationVMBase>();

            foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
            {
                var location = LocationDictionary.Instance[id];

                foreach (var mapLocation in location.MapLocations)
                {
                    mapLocations.Add(MapLocationVMFactory.GetMapLocationControlVM(
                        mapLocation, mapArea, pinnedLocations));
                }
            }

            return mapLocations;
        }
    }
}
