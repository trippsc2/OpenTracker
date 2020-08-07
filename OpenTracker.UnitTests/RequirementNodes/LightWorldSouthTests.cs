using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class LightWorldSouthTests
    {
        [Theory]
        [MemberData(nameof(Start_To_RaceGameLedge))]
        [MemberData(nameof(LightWorld_To_RaceGameLedge))]
        [MemberData(nameof(DarkWorldSouth_To_RaceGameLedge))]
        [MemberData(nameof(Start_To_SouthOfGroveLedge))]
        [MemberData(nameof(LightWorld_To_SouthOfGroveLedge))]
        [MemberData(nameof(DarkWorldSouth_To_SouthOfGroveLedge))]
        [MemberData(nameof(Start_To_DesertLedge))]
        [MemberData(nameof(DesertPalaceBackEntrance_To_DesertLedge))]
        [MemberData(nameof(MireArea_To_DesertLedge))]
        [MemberData(nameof(DPFrontEntry_To_DesertLedge))]
        [MemberData(nameof(Start_To_DesertPalaceBackEntrance))]
        [MemberData(nameof(DesertLedge_To_DesertPalaceBackEntrance))]
        [MemberData(nameof(MireArea_To_DesertPalaceBackEntrance))]
        [MemberData(nameof(Start_To_CheckerboardLedge))]
        [MemberData(nameof(LightWorld_To_CheckerboardLedge))]
        [MemberData(nameof(MireArea_To_CheckerboardLedge))]
        [MemberData(nameof(CheckerboardLedge_To_CheckerboardCave))]
        [MemberData(nameof(LightWorld_To_DesertPalaceFrontEntrance))]
        [MemberData(nameof(MireArea_To_DesertPalaceFrontEntrance))]
        [MemberData(nameof(LightWorld_To_RupeeCave))]
        [MemberData(nameof(LightWorld_To_LWMirePortal))]
        [MemberData(nameof(DWMirePortal_To_LWMirePortal))]
        [MemberData(nameof(LightWorld_To_CentralBonkRocks))]
        [MemberData(nameof(LightWorld_To_LWSouthPortal))]
        [MemberData(nameof(DWSouthPortal_To_LWSouthPortal))]
        [MemberData(nameof(LightWorld_To_HypeFairyCave))]
        [MemberData(nameof(LightWorld_To_MiniMoldormCave))]
        [MemberData(nameof(LightWorld_To_IceRodCave))]
        [MemberData(nameof(LightWorld_To_IceFairyCave))]
        public void AccessibilityTests(
            RequirementNodeID id, ItemPlacement itemPlacement,
            DungeonItemShuffle dungeonItemShuffle, WorldState worldState,
            bool entranceShuffle, bool enemyShuffle, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, AccessibilityLevel expected)
        {
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
        }

        public static IEnumerable<object[]> Start_To_RaceGameLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.RaceGameLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_RaceGameLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_RaceGameLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RaceGameLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_SouthOfGroveLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SouthOfGroveLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_SouthOfGroveLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_SouthOfGroveLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.SouthOfGroveLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DesertLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertPalaceBackEntrance_To_DesertLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_DesertLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DPFrontEntry_To_DesertLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DPFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DesertPalaceBackEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertLedge_To_DesertPalaceBackEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_DesertPalaceBackEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceBackEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_CheckerboardLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_CheckerboardLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_CheckerboardLedge =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> CheckerboardLedge_To_CheckerboardCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CheckerboardCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CheckerboardLedgeTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_DesertPalaceFrontEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Book, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Book, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Book, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Book, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Book, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Book, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Book, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Book, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Book, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_DesertPalaceFrontEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_RupeeCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.RupeeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LWMirePortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWMirePortal_To_LWMirePortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_CentralBonkRocks =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.CentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LWSouthPortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWSouthPortal_To_LWSouthPortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_HypeFairyCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HypeFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HypeFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HypeFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HypeFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HypeFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_MiniMoldormCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.MiniMoldormCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiniMoldormCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiniMoldormCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiniMoldormCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiniMoldormCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_IceRodCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.IceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.IceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.IceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_IceFairyCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.IceFairyCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
