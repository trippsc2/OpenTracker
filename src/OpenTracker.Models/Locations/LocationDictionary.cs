using System;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;
using System.Linq;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="ILocation"/> objects
    /// indexed by <see cref="LocationID"/>.
    /// </summary>
    public class LocationDictionary : LazyDictionary<LocationID, ILocation>, ILocationDictionary
    {
        private readonly Lazy<ILocationFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating the <see cref="ILocationFactory"/> object.
        /// </param>
        public LocationDictionary(ILocationFactory.Factory factory) : base(new Dictionary<LocationID, ILocation>())
        {
            _factory = new Lazy<ILocationFactory>(() => factory());
        }

        public void Reset()
        {
            foreach (var location in Values)
            {
                location.Reset();
            }
        }

        public IDictionary<LocationID, LocationSaveData> Save()
        {
            return Keys.ToDictionary(id => id, id => this[id].Save());
        }

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
