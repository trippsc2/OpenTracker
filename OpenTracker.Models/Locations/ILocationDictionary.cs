using OpenTracker.Models.SaveLoad;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains the dictionary container for location data.
    /// </summary>
    public interface ILocationDictionary : IDictionary<LocationID, ILocation>,
        ISaveable<Dictionary<LocationID, LocationSaveData>>
    {
        event EventHandler<KeyValuePair<LocationID, ILocation>>? ItemCreated;

        void Reset();
    }
}