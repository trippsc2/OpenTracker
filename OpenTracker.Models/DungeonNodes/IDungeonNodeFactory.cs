using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This interface contains the creation logic for dungeon nodes.
    /// </summary>
    public interface IDungeonNodeFactory
    {
        void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, IRequirementNode node, List<INodeConnection> connections);
    }
}
