using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains map location data.
    /// </summary>
    public interface IMapLocation : IReactiveObject
    {
        ILocation Location { get; }
        MapID Map { get; }
        IRequirement Requirement { get; }
        double X { get; }
        double Y { get; }
        bool RequirementMet { get; }
        bool Visible { get; }

        delegate IMapLocation Factory(
            MapID map, double x, double y, ILocation location, IRequirement requirement);
    }
}