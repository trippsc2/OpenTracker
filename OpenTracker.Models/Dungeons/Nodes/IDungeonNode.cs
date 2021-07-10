using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    /// This interface contains the dungeon requirement node data.
    /// </summary>
    public interface IDungeonNode : INode
    {
        /// <summary>
        /// A <see cref="IList{T}"/> of <see cref="INodeConnection"/> to this node.
        /// </summary>
        IList<INodeConnection> Connections { get; }

        /// <summary>
        /// A factory for creating new <see cref="IDungeonNode"/> objects.
        /// </summary>
        /// <param name="dungeonData">
        ///     The <see cref="IMutableDungeon"/> parent class.
        /// </param>'
        /// <returns>
        ///     A new <see cref="IDungeonNode"/> objects.
        /// </returns>
        delegate IDungeonNode Factory(IMutableDungeon dungeonData);
    }
}