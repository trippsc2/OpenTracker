using OpenTracker.Models.NodeConnections;
using System.Collections.Generic;

namespace OpenTracker.Models.RequirementNodes
{
    public interface IRequirementNodeFactory
    {
        delegate IRequirementNodeFactory Factory();

        IRequirementNode GetRequirementNode(RequirementNodeID id);
        void PopulateNodeConnections(RequirementNodeID id, IRequirementNode node, List<INodeConnection> connections);
    }
}