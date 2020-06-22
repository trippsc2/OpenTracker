using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dictionaries
{
    /// <summary>
    /// This is the dictionary of location data
    /// </summary>
    public class LocationDictionary : Dictionary<LocationID, Location>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public LocationDictionary(Game game) : base()
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            foreach (LocationID iD in Enum.GetValues(typeof(LocationID)))
            {
                Add(iD, new Location(game, iD));
            }
        }

        /// <summary>
        /// Initializes all contained locations.
        /// </summary>
        public void Initialize()
        {
            foreach (var location in Values)
            {
                location.Initialize();
            }
        }

        /// <summary>
        /// Resets all locations to starting values.
        /// </summary>
        public void Reset()
        {
            foreach (Location location in Values)
                location.Reset();
        }
    }
}
