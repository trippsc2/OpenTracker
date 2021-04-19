using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boolean
{
    /// <summary>
    ///     This interface contains shop section data.
    /// </summary>
    public interface IShopSection : ISection
    {
        /// <summary>
        ///     A factory for creating new shop sections.
        /// </summary>
        /// <param name="node">
        ///     The node to which this take any section belongs.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this section to be active.
        /// </param>
        /// <returns>
        ///     A new shop section.
        /// </returns>
        delegate IShopSection Factory(INode node, IRequirement requirement);
    }
}
