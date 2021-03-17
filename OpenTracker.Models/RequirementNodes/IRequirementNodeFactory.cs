using System.Collections.Generic;
using OpenTracker.Models.NodeConnections;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This interface contains creation logic for requirement node data.
    /// </summary>
    public interface IRequirementNodeFactory
    {
        delegate IRequirementNodeFactory Factory();

        IRequirementNode GetRequirementNode(RequirementNodeID id);
        void PopulateNodeConnections(RequirementNodeID id, IRequirementNode node, List<INodeConnection> connections);
    }
}