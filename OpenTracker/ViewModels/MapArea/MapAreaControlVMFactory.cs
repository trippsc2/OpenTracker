using OpenTracker.Models.Locations;
using OpenTracker.ViewModels.MapArea.MapLocations;
using OpenTracker.ViewModels.UIPanels.LocationsPanel;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.MapArea
{
    /// <summary>
    /// This is the class for creating map area control ViewModel classes.
    /// </summary>
    public static class MapAreaControlVMFactory
    {
        /// <summary>
        /// Returns an observable collection of map control ViewModel instances.
        /// </summary>
        /// <param name="mapArea">
        /// The map area ViewModel parent class.
        /// </param>
        /// <returns>
        /// An observable collection of map control ViewModel instances.
        /// </returns>
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

        /// <summary>
        /// Returns an observable collection of map location control ViewModel instances.
        /// </summary>
        /// <param name="mapArea">
        /// The map area ViewModel parent class.
        /// </param>
        /// <param name="pinnedLocations">
        /// An observable collection of pinned location control ViewModel instances.
        /// </param>
        /// <returns>
        /// An observable collection of map location control ViewModel instances.
        /// </returns>
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
