using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the interface for a dungeon node.
    /// </summary>
    public interface IDungeonNode : IRequirementNode
    {
        List<INodeConnection> Connections { get; }
        int KeysProvided { get; }
    }
}