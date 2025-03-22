using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.Items
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container of <see cref="IDungeonItem"/> objects
    /// indexed by <see cref="DungeonItemID"/>.
    /// </summary>
    public class DungeonItemDictionary : LazyDictionary<DungeonItemID, IDungeonItem>, IDungeonItemDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly Lazy<IDungeonItemFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating the <see cref="IDungeonItemFactory"/> object.
        /// </param>
        /// <param name="dungeonData">
        ///     The <see cref="IMutableDungeon"/> data.
        /// </param>
        public DungeonItemDictionary(IDungeonItemFactory.Factory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<DungeonItemID, IDungeonItem>())
        {
            _dungeonData = dungeonData;
            _factory = new Lazy<IDungeonItemFactory>(() => factory());
        }

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
