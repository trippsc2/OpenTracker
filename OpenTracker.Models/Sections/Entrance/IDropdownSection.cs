using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Entrance
{
    /// <summary>
    ///     This interface contains dropdown section data.
    /// </summary>
    public interface IDropdownSection : ISection
    {
        /// <summary>
        ///     A factory for creating new dropdown sections.
        /// </summary>
        /// <param name="exitNode">
        ///     The node to which the exit belongs.
        /// </param>
        /// <param name="holeNode">
        ///     The node to which the hole belongs.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        /// <returns>
        ///     A new dropdown section.
        /// </returns>
        delegate IDropdownSection Factory(INode exitNode, INode holeNode, IRequirement requirement);
    }
}