using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Locations
{
    public interface IMapLocation
    {
        ILocation Location { get; }
        MapID Map { get; }
        IRequirement Requirement { get; }
        double X { get; }
        double Y { get; }

        delegate IMapLocation Factory(
            MapID map, double x, double y, ILocation location, IRequirement requirement);
    }
}