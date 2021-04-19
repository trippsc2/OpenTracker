using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boolean
{
    /// <summary>
    ///     This interface contains take any section data.
    /// </summary>
    public interface ITakeAnySection : ISection
    {
        /// <summary>
        ///     A factory for creating new take any sections.
        /// </summary>
        /// <param name="node">
        ///     The node to which this take any section belongs.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this section to be active.
        /// </param>
        /// <returns>
        ///     A new take any section.
        /// </returns>
        delegate ITakeAnySection Factory(INode node, IRequirement requirement);
    }
}
