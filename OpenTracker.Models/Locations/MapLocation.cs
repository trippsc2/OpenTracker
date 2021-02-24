using OpenTracker.Models.Requirements;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the class containing data for a location's placement on the map.
    /// </summary>
    public class MapLocation : IMapLocation
    {
        public ILocation Location { get; }
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
            MapID map, double x, double y, ILocation location, IRequirement requirement)
        {
            Map = map;
            X = x;
            Y = y;
            Location = location;
            Requirement = requirement;
        }
    }
}
