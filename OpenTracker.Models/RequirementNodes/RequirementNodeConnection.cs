using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This is the class containing the data for a connection between requirement nodes.
    /// </summary>
    public class RequirementNodeConnection
    {
        public RequirementNodeID FromNode { get; }
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
        public RequirementNodeConnection(
            RequirementNodeID fromNode, IRequirement requirement = null)
        {
            FromNode = fromNode;
            Requirement = requirement ?? RequirementDictionary.Instance[RequirementType.NoRequirement];
        }
    }
}
