using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the class containing data for a connection between
    /// dungeon nodes.
    /// </summary>
    public class DungeonNodeConnection
    {
        public DungeonNodeID FromNode { get; }
        public IRequirement Requirement { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fromNode">
        /// The identity of the connecting node.
        /// </param>
        /// <param name="requirement">
        /// The requirement for the connection.
        /// </param>
        public DungeonNodeConnection(
            DungeonNodeID fromNode, IRequirement requirement = null)
        {
            FromNode = fromNode;
            Requirement = requirement ?? RequirementDictionary.Instance[RequirementType.NoRequirement];
        }
    }
}
