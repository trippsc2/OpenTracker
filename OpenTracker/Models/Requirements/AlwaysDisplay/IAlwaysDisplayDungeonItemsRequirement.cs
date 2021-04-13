namespace OpenTracker.Models.Requirements.AlwaysDisplay
{
    /// <summary>
    ///     This interface contains always display dungeon items setting requirement data.
    /// </summary>
    public interface IAlwaysDisplayDungeonItemsRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new always display dungeon items requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean representing the expected value.
        /// </param>
        /// <returns>
        ///     A new always display dungeon items requirement.
        /// </returns>
        delegate IAlwaysDisplayDungeonItemsRequirement Factory(bool expectedValue);
    }
}