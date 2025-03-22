using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container of <see cref="IDungeonNode"/>
    /// indexed by <see cref="DungeonNodeID"/>.
    /// </summary>
    public interface IDungeonNodeDictionary : IDictionary<DungeonNodeID, IDungeonNode>
    {
        /// <summary>
        /// The event raised when a new <see cref="IDungeonNode"/> is created.
        /// </summary>
        event EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>> ItemCreated;

        /// <summary>
        /// A factory for creating new <see cref="IDungeonNodeDictionary"/> objects.
        /// </summary>
        /// <param name="dungeonData">
        ///     The <see cref="IMutableDungeon"/> parent class.
        /// </param>
        /// <returns>
        ///     A new <see cref="IDungeonNodeDictionary"/> object.
        /// </returns>
        delegate IDungeonNodeDictionary Factory(IMutableDungeon dungeonData);

        /// <summary>
        /// Calls the indexer for each node in the dungeon, so that it is initialized.
        /// </summary>
        /// <param name="dungeonNodes">
        ///     A <see cref="IList{T}"/> of <see cref="DungeonNodeID"/> for which to call.
        /// </param>
        void PopulateNodes(IList<DungeonNodeID> dungeonNodes);
    }
}