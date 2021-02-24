using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.Locations
{
    public class PinnedLocationCollection : ObservableCollection<ILocation>,
        IPinnedLocationCollection
    {
        private readonly ILocationDictionary _locations;

        public PinnedLocationCollection(ILocationDictionary locations)
        {
            _locations = locations;
        }

        /// <summary>
        /// Returns a list of location IDs to save.
        /// </summary>
        public List<LocationID> Save()
        {
            List<LocationID> pinnedLocations = new List<LocationID>();

            foreach (var pinnedLocation in this)
            {
                pinnedLocations.Add(pinnedLocation.ID);
            }

            return pinnedLocations;
        }

        /// <summary>
        /// Loads a list of location IDs.
        /// </summary>
        /// <param name="saveData">
        /// A list of location IDs to pin.
        /// </param>
        public void Load(List<LocationID>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            Clear();

            foreach (var location in saveData)
            {
                Add(_locations[location]);
            }
        }
    }
}
