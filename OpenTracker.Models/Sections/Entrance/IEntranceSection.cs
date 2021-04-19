using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Entrance
{
    /// <summary>
    ///     This interface contains entrance section data.
    /// </summary>
    public interface IEntranceSection : ISection
    {
        /// <summary>
        ///     A factory for creating new entrance sections.
        /// </summary>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="entranceShuffleLevel">
        ///     The minimum entrance shuffle level.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        /// <param name="node">
        ///     The node to which this section belongs.
        /// </param>
        /// <param name="exitProvided">
        ///     The overworld node exit that this section provides.
        /// </param>
        /// <returns>
        ///     A new entrance section.
        /// </returns>
        delegate IEntranceSection Factory(
            string name, EntranceShuffle entranceShuffleLevel, IRequirement requirement, INode node,
            IOverworldNode? exitProvided = null);
    }
}
