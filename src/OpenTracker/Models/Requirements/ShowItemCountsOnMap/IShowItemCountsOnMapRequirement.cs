namespace OpenTracker.Models.Requirements.ShowItemCountsOnMap
{
    /// <summary>
    ///     This interface contains show item counts on map setting requirement data.
    /// </summary>
    public interface IShowItemCountsOnMapRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new show item counts on map requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean representing the expected value.
        /// </param>
        /// <returns>
        ///     A new show item counts on map requirement.
        /// </returns>
        delegate IShowItemCountsOnMapRequirement Factory(bool expectedValue);
    }
}