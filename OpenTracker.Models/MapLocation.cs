using OpenTracker.Models.Enums;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class containing data for a location's placement on the map.
    /// </summary>
    public class MapLocation
    {
        public Location Location { get; }
        public ModeRequirement ModeRequirement { get; }
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
        /// <param name="modeRequirement">
        /// The mode requirement for displaying this map location.
        /// </param>
        public MapLocation(Location location, MapID map,
            double x, double y, ModeRequirement modeRequirement)
        {
            Location = location;
            ModeRequirement = modeRequirement;
            Map = map;
            X = x;
            Y = y;
        }
    }
}
