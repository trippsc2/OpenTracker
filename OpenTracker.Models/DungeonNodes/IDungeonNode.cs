using OpenTracker.Models.Dungeons;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the interface for dungeon requirement node.
    /// </summary>
    public interface IDungeonNode : IRequirementNode
    {
        List<INodeConnection> Connections { get; }

        new delegate IDungeonNode Factory(IMutableDungeon dungeonData, DungeonNodeID id);
    }
}