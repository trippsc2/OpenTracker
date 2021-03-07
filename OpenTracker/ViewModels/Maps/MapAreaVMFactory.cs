using OpenTracker.Models.Locations;
using OpenTracker.ViewModels.Maps.Locations;
using System;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Maps
{
    /// <summary>
    /// This is the class for creating map area control ViewModel classes.
    /// </summary>
    public class MapAreaVMFactory : IMapAreaVMFactory
    {
        private readonly ILocationDictionary _locations;
        private readonly IMapLocationVMFactory _locationFactory;
        private readonly IMapVM.Factory _mapFactory;

        public MapAreaVMFactory(
            ILocationDictionary locations, IMapLocationVMFactory locationFactory,
            IMapVM.Factory mapFactory)
        {
            _locations = locations;
            _locationFactory = locationFactory;
            _mapFactory = mapFactory;
        }

        /// <summary>
        /// Returns an observable collection of map control ViewModel instances.
        /// </summary>
        /// <returns>
        /// An observable collection of map control ViewModel instances.
        /// </returns>
        public List<IMapVM> GetMapControlVMs()
        {
            var maps = new List<IMapVM>();

            foreach (MapID map in Enum.GetValues(typeof(MapID)))
            {
                maps.Add(_mapFactory(map));
            }

            return maps;
        }

        /// <summary>
        /// Returns an observable collection of map location control ViewModel instances.
        /// </summary>
        /// <returns>
        /// An observable collection of map location control ViewModel instances.
        /// </returns>
        public List<IMapLocationVMBase> GetMapLocationControlVMs()
        {
            var mapLocations = new List<IMapLocationVMBase>();

            foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
            {
                foreach (var mapLocation in _locations[id].MapLocations)
                {
                    mapLocations.Add(_locationFactory.GetMapLocationControlVM(mapLocation));
                }
            }

            return mapLocations;
        }
    }
}
