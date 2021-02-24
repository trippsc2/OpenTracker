using OpenTracker.Models.Dungeons;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the class containing the dictionary of dungeon nodes.
    /// </summary>
    public class DungeonNodeDictionary : LazyDictionary<DungeonNodeID, IDungeonNode>,
        IDungeonNodeDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly IDungeonNode.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The factory for creating new dungeon nodes.
        /// </param>
        public DungeonNodeDictionary(
            IDungeonNode.Factory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<DungeonNodeID, IDungeonNode>())
        {
            _factory = factory;

            _dungeonData = dungeonData;
        }

        protected override IDungeonNode Create(DungeonNodeID key)
        {
            return _factory(_dungeonData, key);
        }
    }
}
