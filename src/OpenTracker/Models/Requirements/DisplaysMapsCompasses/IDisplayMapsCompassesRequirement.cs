namespace OpenTracker.Models.Requirements.DisplaysMapsCompasses
{
    /// <summary>
    ///     This interface contains display maps/compasses setting requirement data.
    /// </summary>
    public interface IDisplayMapsCompassesRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new display maps and compasses requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean representing the expected value.
        /// </param>
        /// <returns>
        ///     A new display maps and compasses requirement.
        /// </returns>
        delegate IDisplayMapsCompassesRequirement Factory(bool expectedValue);
    }
}