using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the class containing data for a location's placement on the map.
    /// </summary>
    public class MapLocation
    {
        private readonly LocationID _locationID;

        public ILocation Location { get; private set; }
        public IRequirement Requirement { get; }
        public MapID Map { get; }
        public double X { get; }
        public double Y { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location parent class.
        /// </param>
        /// <param name="map">
        /// The map identity on which the location is represented.
        /// </param>
        /// <param name="x">
        /// The X coordinate of the map location.
        /// </param>
        /// <param name="y">
        /// The Y coordinate of the map location.
        /// </param>
        /// <param name="requirement">
        /// The mode requirement for displaying this map location.
        /// </param>
        public MapLocation(
            LocationID locationID, MapID map, double x, double y, IRequirement requirement = null)
        {
            _locationID = locationID;
            Map = map;
            X = x;
            Y = y;
            Requirement = requirement ?? RequirementDictionary.Instance[RequirementType.NoRequirement];

            LocationDictionary.Instance.LocationCreated += OnLocationCreated;
        }

        /// <summary>
        /// Subscribes to the LocationCreated event on the LocationDictionary class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the LocationCreated event.
        /// </param>
        private void OnLocationCreated(object sender, LocationID id)
        {
            if (id == _locationID)
            {
                Location = LocationDictionary.Instance[_locationID];
            }

            if (Location != null)
            {
                LocationDictionary.Instance.LocationCreated -= OnLocationCreated;
            }
        }
    }
}
