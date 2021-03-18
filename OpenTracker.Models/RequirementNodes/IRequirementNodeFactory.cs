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
        IEnumerable<INodeConnection> GetNodeConnections(RequirementNodeID id, IRequirementNode node);
    }
}