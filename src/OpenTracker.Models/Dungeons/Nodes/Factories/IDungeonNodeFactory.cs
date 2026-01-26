using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;

namespace OpenTracker.Models.Dungeons.Nodes.Factories;

/// <summary>
/// This interface contains the creation logic for dungeon nodes.
/// </summary>
public interface IDungeonNodeFactory
{
    /// <summary>
    /// Populates the dungeon node connections.
    /// </summary>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/> parent class.
    /// </param>
    /// <param name="id">
    ///     The <see cref="DungeonNodeID"/> .
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/>.
    /// </param>
    /// <param name="connections">
    ///     The <see cref="IList{T}"/> of <see cref="INodeConnection"/> to be populated.
    /// </param>
    void PopulateNodeConnections(
        IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections);
}