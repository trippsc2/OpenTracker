using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Boolean;

/// <summary>
/// This interface contains take any section data.
/// </summary>
public interface ITakeAnySection : ISection
{
    /// <summary>
    /// A factory for creating new <see cref="ITakeAnySection"/> objects.
    /// </summary>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this section belongs.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for this section to be active.
    /// </param>
    /// <returns>
    ///     A new <see cref="ITakeAnySection"/> object.
    /// </returns>
    delegate ITakeAnySection Factory(INode node, IRequirement requirement);
}