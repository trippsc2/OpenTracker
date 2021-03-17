using System;
using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.ViewModels.MapLocations;
using OpenTracker.ViewModels.Maps;

namespace OpenTracker.ViewModels.Areas
{
    /// <summary>
    /// This is the class for creating map area control ViewModel classes.
    /// </summary>
    public class MapAreaFactory : IMapAreaFactory
    {
        private readonly ILocationDictionary _locations;
        private readonly IMapLocationVMFactory _locationFactory;
        private readonly IMapVM.Factory _mapFactory;

        public MapAreaFactory(
            ILocationDictionary locations, IMapLocationVMFactory locationFactory, IMapVM.Factory mapFactory)
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

            for (var i = 0; i < Enum.GetValues(typeof(MapID)).Length; i++)
            {
                maps.Add(_mapFactory((MapID)i));
            }

            return maps;
        }

        /// <summary>
        /// Returns an observable collection of map location control ViewModel instances.
        /// </summary>
        /// <returns>
        /// An observable collection of map location control ViewModel instances.
        /// </returns>
        public List<IMapLocationVM> GetMapLocationControlVMs()
        {
            var mapLocations = new List<IMapLocationVM>();

            // TODO - Convert to foreach in .NET 5
            for (var i = 0; i < Enum.GetValues(typeof(LocationID)).Length; i++)
            {
                foreach (var mapLocation in _locations[(LocationID)i].MapLocations)
                {
                    mapLocations.Add(_locationFactory.GetMapLocation(mapLocation));
                }
            }

            return mapLocations;
        }
    }
}
