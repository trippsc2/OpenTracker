using OpenTracker.Models.Enums;

namespace OpenTracker.Models
{
    public class ModeRequirement
    {
        public bool? MapCompassShuffle { get; }
        public bool? SmallKeyShuffle { get; }
        public bool? BigKeyShuffle { get; }

        public ItemPlacement? ItemPlacement { get; }
        public DungeonItemShuffle? DungeonItemShuffle { get; }
        public WorldState? WorldState { get; }
        public bool? EntranceShuffle { get; }
        public bool? BossShuffle { get; }
        public bool? EnemyShuffle { get; }

        public ModeRequirement(bool? mapCompassShuffle = null, bool? smallKeyShuffle = null,
            bool? bigKeyShuffle = null, ItemPlacement? itemPlacement = null,
            DungeonItemShuffle? dungeonItemShuffle = null, WorldState? worldState = null,
            bool? entranceShuffle = null, bool? bossShuffle = null, bool? enemyShuffle = null)
        {
            MapCompassShuffle = mapCompassShuffle;
            SmallKeyShuffle = smallKeyShuffle;
            BigKeyShuffle = bigKeyShuffle;

            ItemPlacement = itemPlacement;
            DungeonItemShuffle = dungeonItemShuffle;
            WorldState = worldState;
            EntranceShuffle = entranceShuffle;
            BossShuffle = bossShuffle;
            EnemyShuffle = enemyShuffle;
        }
    }
}
