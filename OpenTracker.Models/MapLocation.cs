using OpenTracker.Models.Enums;

namespace OpenTracker.Models
{
    public class MapLocation
    {
        public Location Location { get; }
        public ModeRequirement ModeRequirement { get; }
        public MapID Map { get; }
        public double X { get; }
        public double Y { get; }

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
