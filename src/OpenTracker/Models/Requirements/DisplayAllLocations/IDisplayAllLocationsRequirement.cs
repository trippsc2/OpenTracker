namespace OpenTracker.Models.Requirements.DisplayAllLocations;

/// <summary>
///     This interface contains display all locations setting requirement data.
/// </summary>
public interface IDisplayAllLocationsRequirement : IRequirement
{
    /// <summary>
    ///     A factory for creating new display all locations requirements.
    /// </summary>
    /// <param name="expectedValue">
    ///     A boolean representing the expected display all locations value.
    /// </param>
    /// <returns>
    ///     A new display all locations requirement.
    /// </returns>
    delegate IDisplayAllLocationsRequirement Factory(bool expectedValue);
}