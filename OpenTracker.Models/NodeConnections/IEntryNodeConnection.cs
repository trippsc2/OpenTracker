using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.NodeConnections
{
    public interface IEntryNodeConnection : INodeConnection
    {
        delegate IEntryNodeConnection Factory(INode fromNode);
    }
}