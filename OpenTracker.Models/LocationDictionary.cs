using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class LocationDictionary : Dictionary<LocationID, Location>
    {
        public LocationDictionary(int capacity) : base(capacity)
        {
        }

        public void Reset()
        {
            foreach (Location location in Values)
                location.Reset();
        }
    }
}
