using OpenTracker.Enums;
using OpenTracker.Interfaces;

namespace OpenTracker.Models
{
    public class LocationPlacement
    {
        public ILocation Location { get; }
        public Mode VisibilityMode { get; }
        public MapID Map { get; }
        public double X { get; }
        public double Y { get; }

        public LocationPlacement(ILocation location, MapID map,
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
