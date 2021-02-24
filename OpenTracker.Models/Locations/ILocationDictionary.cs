using OpenTracker.Models.SaveLoad;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    public interface ILocationDictionary : IDictionary<LocationID, ILocation>,
        ICollection<KeyValuePair<LocationID, ILocation>>,
        ISaveable<Dictionary<LocationID, LocationSaveData>>
    {
        event EventHandler<KeyValuePair<LocationID, ILocation>>? ItemCreated;

        void Reset();
    }
}