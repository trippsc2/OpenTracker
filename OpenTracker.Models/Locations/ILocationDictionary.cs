using System;
using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

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