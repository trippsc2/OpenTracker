using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.Items
{
    /// <summary>
    ///     This class contains the dictionary container of dungeon items.
    /// </summary>
    public class DungeonItemDictionary : LazyDictionary<DungeonItemID, IDungeonItem>, IDungeonItemDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly Lazy<IDungeonItemFactory> _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     A factory for creating dungeon items.
        /// </param>
        /// <param name="dungeonData">
        ///     The mutable dungeon data.
        /// </param>
        public DungeonItemDictionary(IDungeonItemFactory.Factory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<DungeonItemID, IDungeonItem>())
        {
            _dungeonData = dungeonData;
            _factory = new Lazy<IDungeonItemFactory>(() => factory());
        }

        /// <summary>
        ///     Calls the indexer for each item in the specified list, so that it is initialized.
        /// </summary>
        /// <param name="items">
        ///     A list of dungeon item IDs for which to call.
        /// </param>
        public void PopulateItems(IList<DungeonItemID> items)
        {
            foreach (var item in items)
            {
                _ = this[item];
            }
        }

        protected override IDungeonItem Create(DungeonItemID key)
        {
            return _factory.Value.GetDungeonItem(_dungeonData, key);
        }
    }
}
