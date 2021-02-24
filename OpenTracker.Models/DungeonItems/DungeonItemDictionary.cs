using OpenTracker.Models.Dungeons;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the class containing the dictionary of dungeon items.
    /// </summary>
    public class DungeonItemDictionary : LazyDictionary<DungeonItemID, IDungeonItem>,
        IDungeonItemDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly IDungeonItemFactory.Factory _factory;

        public delegate IDungeonItemDictionary Factory(IMutableDungeon dungeonData);

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
            _factory = factory;
        }

        protected override IDungeonItem Create(DungeonItemID key)
        {
            return _factory().GetDungeonItem(_dungeonData, key);
        }
    }
}
