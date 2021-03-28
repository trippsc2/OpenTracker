using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This class contains the dictionary container for location data.
    /// </summary>
    public class LocationDictionary : LazyDictionary<LocationID, ILocation>,
        ILocationDictionary
    {
        private readonly ILocation.Factory _locationFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="locationFactory">
        /// The location factory.
        /// </param>
        public LocationDictionary(ILocation.Factory locationFactory) : base(new Dictionary<LocationID, ILocation>())
        {
            _locationFactory = locationFactory;
        }

        protected override ILocation Create(LocationID key)
        { 
            return _locationFactory(key);
        }

        /// <summary>
        /// Resets all locations to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (var location in Values)
            {
                location.Reset();
            }
        }

        /// <summary>
        /// Returns a dictionary of location save data.
        /// </summary>
        /// <returns>
        /// A dictionary of location save data.
        /// </returns>
        public Dictionary<LocationID, LocationSaveData> Save()
        {
            Dictionary<LocationID, LocationSaveData> locations = new();

            foreach (var id in Keys)
            {
                locations.Add(id, this[id].Save());
            }

            return locations;
        }

        /// <summary>
        /// Loads a dictionary of location save data.
        /// </summary>
        public void Load(Dictionary<LocationID, LocationSaveData>? saveData)
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
    }
}
