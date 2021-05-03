namespace OpenTracker.Models.Requirements.TakeAnyLocations
{
    /// <summary>
    ///     This interface contains take any locations requirement data.
    /// </summary>
    public interface ITakeAnyLocationsRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new take any locations requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean expected take any locations value.
        /// </param>
        /// <returns>
        ///     A new take any locations requirement.
        /// </returns>
        delegate ITakeAnyLocationsRequirement Factory(bool expectedValue);
    }
}