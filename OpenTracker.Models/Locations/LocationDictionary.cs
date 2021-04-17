using System;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;
using System.Linq;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    ///     This class contains the dictionary container for location data.
    /// </summary>
    public class LocationDictionary : LazyDictionary<LocationID, ILocation>, ILocationDictionary
    {
        private readonly Lazy<ILocationFactory> _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating the location factory.
        /// </param>
        public LocationDictionary(ILocationFactory.Factory factory) : base(new Dictionary<LocationID, ILocation>())
        {
            _factory = new Lazy<ILocationFactory>(() => factory());
        }

        /// <summary>
        ///     Resets all locations to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (var location in Values)
            {
                location.Reset();
            }
        }

        /// <summary>
        ///     Returns a dictionary of location save data.
        /// </summary>
        /// <returns>
        ///     A dictionary of location save data.
        /// </returns>
        public IDictionary<LocationID, LocationSaveData> Save()
        {
            return Keys.ToDictionary(id => id, id => this[id].Save());
        }

        /// <summary>
        ///     Loads a dictionary of location save data.
        /// </summary>
        public void Load(IDictionary<LocationID, LocationSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            foreach (var id in saveData.Keys)
            {
                this[id].Load(saveData[id]);
            }
        }

        protected override ILocation Create(LocationID key)
        { 
            return _factory.Value.GetLocation(key);
        }
    }
}
