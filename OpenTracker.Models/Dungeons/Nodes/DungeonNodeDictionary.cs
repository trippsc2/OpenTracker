using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    /// This class contains the dictionary container for dungeon nodes.
    /// </summary>
    public class DungeonNodeDictionary : LazyDictionary<DungeonNodeID, IDungeonNode>, IDungeonNodeDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly IDungeonNode.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The factory for creating new dungeon nodes.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data.
        /// </param>
        public DungeonNodeDictionary(IDungeonNode.Factory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<DungeonNodeID, IDungeonNode>())
        {
            _factory = factory;

            _dungeonData = dungeonData;
        }

        public void PopulateNodes(IList<DungeonNodeID> dungeonNodes)
        {
            foreach (var node in dungeonNodes)
            {
                _ = this[node];
            }
        }

        protected override IDungeonNode Create(DungeonNodeID key)
        {
            return _factory(_dungeonData, key);
        }
    }
}
