using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    ///     This interface contains the dungeon requirement node data.
    /// </summary>
    public interface IDungeonNode : INode
    {
        /// <summary>
        ///     A list of node connections to this node.
        /// </summary>
        IList<INodeConnection> Connections { get; }

        /// <summary>
        ///     A factory for creating dungeon nodes.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data parent class.
        /// </param>'
        /// <returns>
        ///     A new dungeon node.
        /// </returns>
        delegate IDungeonNode Factory(IMutableDungeon dungeonData);
    }
}