using OpenTracker.Models.Dungeons;
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
        private readonly IDungeon.Factory _dungeonFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="locationFactory">
        /// The location factory.
        /// </param>
        /// <param name="dungeonFactory">
        /// The dungeon factory.
        /// </param>
        public LocationDictionary(
            ILocation.Factory locationFactory, IDungeon.Factory dungeonFactory)
            : base(new Dictionary<LocationID, ILocation>())
        {
            _locationFactory = locationFactory;
            _dungeonFactory = dungeonFactory;
        }

        protected override ILocation Create(LocationID key)
        {
            switch (key)
            {
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower:
                case LocationID.EasternPalace:
                case LocationID.DesertPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                case LocationID.GanonsTower:
                    {
                        return _dungeonFactory(key);
                    }
                default:
                    {
                        return _locationFactory(key);
                    }
            }
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
            Dictionary<LocationID, LocationSaveData> locations =
                new();

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
