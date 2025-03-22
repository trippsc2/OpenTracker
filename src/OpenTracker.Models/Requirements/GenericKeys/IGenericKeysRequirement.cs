using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.GenericKeys
{
    /// <summary>
    /// This interface contains <see cref="IMode.GenericKeys"/> requirement data.
    /// </summary>
    public interface IGenericKeysRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IGenericKeysRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> representing the expected <see cref="IMode.GenericKeys"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IGenericKeysRequirement"/> object.
        /// </returns>
        delegate IGenericKeysRequirement Factory(bool expectedValue);
    }
}