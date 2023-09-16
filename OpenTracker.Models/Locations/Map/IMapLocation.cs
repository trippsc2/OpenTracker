using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Locations.Map;

/// <summary>
/// This interface contains map location data.
/// </summary>
public interface IMapLocation : IReactiveObject
{
    /// <summary>
    /// The <see cref="ILocation"/> to be represented at this map location.
    /// </summary>
    ILocation Location { get; }
        
    /// <summary>
    /// The <see cref="MapID"/> to which this map location belongs.
    /// </summary>
    MapID Map { get; }
        
    /// <summary>
    /// A <see cref="double"/> representing the X coordinate.
    /// </summary>
    double X { get; }
        
    /// <summary>
    /// A <see cref="double"/> representing the Y coordinate.
    /// </summary>
    double Y { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the map location is active.
    /// </summary>
    bool IsActive { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the map location should be visible under normal circumstances.
    /// </summary>
    bool ShouldBeDisplayed { get; }

    /// <summary>
    /// A factory for creating new <see cref="IMapLocation"/> objects.
    /// </summary>
    /// <param name="map">
    ///     The <see cref="MapID"/> to which this map location belongs.
    /// </param>
    /// <param name="x">
    ///     A <see cref="double"/> representing the X coordinate of the map location.
    /// </param>
    /// <param name="y">
    ///     A <see cref="double"/> representing the Y coordinate of the map location.
    /// </param>
    /// <param name="location">
    ///     The <see cref="ILocation"/> parent class.
    /// </param>
    /// <param name="requirement">
    ///     The nullable <see cref="IRequirement"/> for displaying this map location.
    /// </param>
    /// <returns>
    ///     A new <see cref="IMapLocation"/> object.
    /// </returns>
    delegate IMapLocation Factory(
        MapID map, double x, double y, ILocation location, IRequirement? requirement = null);
}