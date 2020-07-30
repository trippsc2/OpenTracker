using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class ModeRequirementTests
    {
        [Theory]
        [MemberData(nameof(WorldState_Data))]
        public void WorldState_AccessibilityTests(
            WorldState worldState, RequirementType type, AccessibilityLevel expected)
        {
            Mode.Instance.WorldState = worldState;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> WorldState_Data =>
            new List<object[]>
            {
                new object[]
                {
                    WorldState.StandardOpen,
                    RequirementType.WorldStateStandardOpen,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    RequirementType.WorldStateStandardOpen,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    RequirementType.WorldStateStandardOpen,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    RequirementType.WorldStateInverted,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    RequirementType.WorldStateInverted,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    RequirementType.WorldStateInverted,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    RequirementType.WorldStateRetro,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    RequirementType.WorldStateRetro,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    RequirementType.WorldStateRetro,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    RequirementType.WorldStateNonInverted,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    RequirementType.WorldStateNonInverted,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    RequirementType.WorldStateNonInverted,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(DungeonItemShuffle_Data))]
        public void DungeonItemShuffle_AccessibilityTests(
            DungeonItemShuffle dungeonItemShuffle, RequirementType type,
            AccessibilityLevel expected)
        {
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> DungeonItemShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonItemShuffle.Standard,
                    RequirementType.DungeonItemShuffleStandard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.DungeonItemShuffleStandard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.DungeonItemShuffleStandard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.Keysanity,
                    RequirementType.DungeonItemShuffleStandard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.Standard,
                    RequirementType.DungeonItemShuffleMapsCompasses,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.DungeonItemShuffleMapsCompasses,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.DungeonItemShuffleMapsCompasses,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.Keysanity,
                    RequirementType.DungeonItemShuffleMapsCompasses,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.Standard,
                    RequirementType.DungeonItemShuffleMapsCompassesSmallKeys,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.DungeonItemShuffleMapsCompassesSmallKeys,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.DungeonItemShuffleMapsCompassesSmallKeys,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonItemShuffle.Keysanity,
                    RequirementType.DungeonItemShuffleMapsCompassesSmallKeys,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.Standard,
                    RequirementType.DungeonItemShuffleKeysanity,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.DungeonItemShuffleKeysanity,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.DungeonItemShuffleKeysanity,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonItemShuffle.Keysanity,
                    RequirementType.DungeonItemShuffleKeysanity,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(ItemPlacement_Data))]
        public void ItemPlacement_AccessibilityTests(
            ItemPlacement itemPlacement, RequirementType type, AccessibilityLevel expected)
        {
            Mode.Instance.ItemPlacement = itemPlacement;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> ItemPlacement_Data =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Basic,
                    RequirementType.ItemPlacementBasic,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    RequirementType.ItemPlacementBasic,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    RequirementType.ItemPlacementAdvanced,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    RequirementType.ItemPlacementAdvanced,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(BossShuffle_Data))]
        public void BossShuffle_AccessibilityTests(
            bool bossShuffle, RequirementType type, AccessibilityLevel expected)
        {
            Mode.Instance.BossShuffle = bossShuffle;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> BossShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    false,
                    RequirementType.BossShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    true,
                    RequirementType.BossShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    false,
                    RequirementType.BossShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    true,
                    RequirementType.BossShuffleOn,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(EnemyShuffle_Data))]
        public void EnemyShuffle_AccessibilityTests(
            bool enemyShuffle, RequirementType type, AccessibilityLevel expected)
        {
            Mode.Instance.EnemyShuffle = enemyShuffle;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> EnemyShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    false,
                    RequirementType.EnemyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    true,
                    RequirementType.EnemyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    false,
                    RequirementType.EnemyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    true,
                    RequirementType.EnemyShuffleOn,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(GuaranteedBossItems_Data))]
        public void GuaranteedBossItems_AccessibilityTests(
            bool guaranteedBossItems, RequirementType type, AccessibilityLevel expected)
        {
            Mode.Instance.GuaranteedBossItems = guaranteedBossItems;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> GuaranteedBossItems_Data =>
            new List<object[]>
            {
                new object[]
                {
                    false,
                    RequirementType.GuaranteedBossItemsOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    true,
                    RequirementType.GuaranteedBossItemsOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    false,
                    RequirementType.GuaranteedBossItemsOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    true,
                    RequirementType.GuaranteedBossItemsOn,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(SmallKeyShuffle_Data))]
        public void SmallKeyShuffle_AccessibilityTests(
            WorldState worldState, DungeonItemShuffle dungeonItemShuffle, RequirementType type,
            AccessibilityLevel expected)
        {
            Mode.Instance.WorldState = worldState;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> SmallKeyShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Keysanity,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.Keysanity,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.Keysanity,
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Keysanity,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    DungeonItemShuffle.Keysanity,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    DungeonItemShuffle.Keysanity,
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(WorldStateEntranceShuffle_Data))]
        public void WorldStateEntranceShuffle_AccessibilityTests(
            WorldState worldState, bool entranceShuffle, RequirementType type,
            AccessibilityLevel expected)
        {
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> WorldStateEntranceShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    WorldState.StandardOpen,
                    false,
                    RequirementType.WorldStateNonInvertedEntranceShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    false,
                    RequirementType.WorldStateNonInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    false,
                    RequirementType.WorldStateNonInvertedEntranceShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    true,
                    RequirementType.WorldStateNonInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    RequirementType.WorldStateNonInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    true,
                    RequirementType.WorldStateNonInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    false,
                    RequirementType.WorldStateInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    false,
                    RequirementType.WorldStateInvertedEntranceShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    false,
                    RequirementType.WorldStateInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    true,
                    RequirementType.WorldStateInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    RequirementType.WorldStateInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    true,
                    RequirementType.WorldStateInvertedEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    false,
                    RequirementType.WorldStateInvertedEntranceShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    false,
                    RequirementType.WorldStateInvertedEntranceShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    false,
                    RequirementType.WorldStateInvertedEntranceShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    true,
                    RequirementType.WorldStateInvertedEntranceShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    RequirementType.WorldStateInvertedEntranceShuffleOn,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    true,
                    RequirementType.WorldStateInvertedEntranceShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    false,
                    RequirementType.WorldStateRetroEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    false,
                    RequirementType.WorldStateRetroEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    false,
                    RequirementType.WorldStateRetroEntranceShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    true,
                    RequirementType.WorldStateRetroEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    RequirementType.WorldStateRetroEntranceShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    WorldState.Retro,
                    true,
                    RequirementType.WorldStateRetroEntranceShuffleOff,
                    AccessibilityLevel.None
                }
            };
    }
}
