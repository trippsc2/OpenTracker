using System.Collections.Generic;
using OpenTracker.Models.NodeConnections;

namespace OpenTracker.Models.Nodes
{
    /// <summary>
    /// This interface contains creation logic for requirement node data.
    /// </summary>
    public interface IOverworldNodeFactory
    {
        delegate IOverworldNodeFactory Factory();

        INode GetOverworldNode(OverworldNodeID id);
        IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node);
    }
}