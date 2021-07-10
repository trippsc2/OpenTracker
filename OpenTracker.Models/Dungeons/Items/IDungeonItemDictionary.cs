using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.Items
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container of <see cref="IDungeonItem"/>
    /// objects indexed by <see cref="DungeonItemID"/>.
    /// </summary>
    public interface IDungeonItemDictionary : IDictionary<DungeonItemID, IDungeonItem>
    {
        /// <summary>
        /// An event indicating that a new <see cref="IDungeonItem"/> was created.
        /// </summary>
        event EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>? ItemCreated;

        /// <summary>
        ///     A factory for creating new <see cref="IDungeonItemDictionary"/> objects.
        /// </summary>
        /// <param name="dungeonData">
        ///     The <see cref="IMutableDungeon"/> data.
        /// </param>
        /// <returns>
        ///     A new <see cref="IDungeonItemDictionary"/> object.
        /// </returns>
        delegate IDungeonItemDictionary Factory(IMutableDungeon dungeonData);

        /// <summary>
        /// Calls the indexer for each <see cref="DungeonItemID"/> in the specified <see cref="IList{T}"/>, so that it
        /// is initialized.
        /// </summary>
        /// <param name="items">
        ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> for which to call.
        /// </param>
        void PopulateItems(IList<DungeonItemID> items);
    }
}