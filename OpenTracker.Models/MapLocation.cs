using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class MapLocation
    {
        public Location Location { get; }
        public Mode VisibilityMode { get; }
        public MapID Map { get; }
        public double X { get; }
        public double Y { get; }

        public MapLocation(Location location, MapID map,
            double x, double y, Mode mode)
        {
            Location = location;
            VisibilityMode = mode;
            Map = map;
            X = x;
            Y = y;
        }
    }
}
