using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements.Static;

/// <summary>
/// This interface contains unchanging <see cref="IRequirement"/> data.
/// </summary>
public interface IStaticRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IStaticRequirement"/> objects.
    /// </summary>
    /// <param name="accessibility">
    ///     The <see cref="AccessibilityLevel"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IStaticRequirement"/> object.
    /// </returns>
    delegate IStaticRequirement Factory(AccessibilityLevel accessibility);
}