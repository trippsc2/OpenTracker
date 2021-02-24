using OpenTracker.Models.Dungeons;
using OpenTracker.Utils;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the class is the dictionary container of dungeon items.
    /// </summary>
    public class DungeonItemDictionary : LazyDictionary<DungeonItemID, IDungeonItem>,
        IDungeonItemDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly Lazy<IDungeonItemFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        public DungeonItemDictionary(
            IDungeonItemFactory.Factory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<DungeonItemID, IDungeonItem>())
        {
            _dungeonData = dungeonData;
            _factory = new Lazy<IDungeonItemFactory>(() => factory());
        }

        protected override IDungeonItem Create(DungeonItemID key)
        {
            return _factory.Value.GetDungeonItem(_dungeonData, key);
        }
    }
}
