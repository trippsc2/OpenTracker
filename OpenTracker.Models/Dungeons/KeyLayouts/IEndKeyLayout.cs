using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    ///     This interface contains the end of key layout data.
    /// </summary>
    public interface IEndKeyLayout : IKeyLayout
    {
        /// <summary>
        ///     A factory for creating a new end key layouts.
        /// </summary>
        /// <param name="requirement">
        ///     The requirement for this key layout to be valid.
        /// </param>
        /// <returns>
        ///     A new end key layout.
        /// </returns>
        delegate IEndKeyLayout Factory(IRequirement? requirement = null);
    }
}