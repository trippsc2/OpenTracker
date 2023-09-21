using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Entrance;

/// <summary>
/// This interface contains dropdown section data.
/// </summary>
public interface IDropdownSection : ISection
{
    /// <summary>
    /// A factory for creating new <see cref="IDropdownSection"/> objects.
    /// </summary>
    /// <param name="exitNode">
    ///     The <see cref="INode"/> to which the exit belongs.
    /// </param>
    /// <param name="holeNode">
    ///     The <see cref="INode"/> to which the hole belongs.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the section to be active.
    /// </param>
    /// <returns>
    ///     A new <see cref="IDropdownSection"/> object.
    /// </returns>
    delegate IDropdownSection Factory(INode exitNode, INode holeNode, IRequirement requirement);
}