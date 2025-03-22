using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    /// This interface contains the end of key layout data.
    /// </summary>
    public interface IEndKeyLayout : IKeyLayout
    {
        /// <summary>
        /// A factory for creating a new <see cref="IEndKeyLayout"/> objects.
        /// </summary>
        /// <param name="requirement">
        ///     The <see cref="IRequirement"/> for this key layout to be valid.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEndKeyLayout"/> object.
        /// </returns>
        delegate IEndKeyLayout Factory(IRequirement? requirement = null);
    }
}