using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements.Static
{
    /// <summary>
    ///     This interface contains unchanging requirement data.
    /// </summary>
    public interface IStaticRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new static requirements.
        /// </summary>
        /// <param name="accessibility">
        ///     The accessibility level of the requirement.
        /// </param>
        /// <returns>
        ///     A new static requirement.
        /// </returns>
        delegate IStaticRequirement Factory(AccessibilityLevel accessibility);
    }
}