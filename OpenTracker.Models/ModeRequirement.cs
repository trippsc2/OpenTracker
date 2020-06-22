using OpenTracker.Models.Enums;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for mode requirements to be checked against the current
    /// game mode.
    /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapCompassShuffle">
        /// A nullable boolean representing whether the maps and compasses are required
        /// to be shuffled.
        /// </param>
        /// <param name="smallKeyShuffle">
        /// A nullable boolean representing whether the small keys are required to be
        /// shuffled.
        /// </param>
        /// <param name="bigKeyShuffle">
        /// A nullable boolean representing whether the big key is required to be shuffled.
        /// </param>
        /// <param name="itemPlacement">
        /// The Item Placement setting requirement.
        /// </param>
        /// <param name="dungeonItemShuffle">
        /// The Dungeon Item Shuffle setting requirement.
        /// </param>
        /// <param name="worldState">
        /// The World State setting.
        /// </param>
        /// <param name="entranceShuffle">
        /// A nullable boolean representing whether Entrance Shuffle is enabled.
        /// </param>
        /// <param name="bossShuffle">
        /// A nullable boolean representing whether Boss Shuffle is enabled.
        /// </param>
        /// <param name="enemyShuffle">
        /// A nullable boolean representing whether Enemy Shuffle is enabled.
        /// </param>
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
