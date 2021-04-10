using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This interface contains item placement setting requirement data.
    /// </summary>
    public interface IItemPlacementRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new item placement requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The expected item placement value.
        /// </param>
        /// <returns>
        ///     A new item placement requirement.
        /// </returns>
        delegate IItemPlacementRequirement Factory(ItemPlacement expectedValue);
    }
}