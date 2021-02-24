using OpenTracker.Models.Dungeons;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
    public interface IDungeonNodeFactory
    {
        void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, IRequirementNode node,
            List<INodeConnection> connections);
    }
}
