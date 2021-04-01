using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    /// This interface contains the creation logic for dungeon nodes.
    /// </summary>
    public interface IDungeonNodeFactory
    {
        void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, IRequirementNode node, IList<INodeConnection> connections);
    }
}
