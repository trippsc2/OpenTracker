using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class DarkWorldSouthTests
    {
        [Theory]
        [MemberData(nameof(Start_To_DarkWorldSouth))]
        [MemberData(nameof(DarkWorldWest_To_DarkWorldSouth))]
        [MemberData(nameof(DWSouthPortal_To_DarkWorldSouth))]
        [MemberData(nameof(DarkWorldEast_To_DarkWorldSouth))]
        [MemberData(nameof(DarkWorldSouth_To_DWCentralBonkRocks))]
        [MemberData(nameof(DarkWorldSouth_To_BuyBigBomb))]
        [MemberData(nameof(LightWorld_To_BuyBigBomb))]
        [MemberData(nameof(BuyBigBomb_To_BigBombToLightWorld))]
        [MemberData(nameof(BuyBigBomb_To_BigBombToDWLakeHylia))]
        [MemberData(nameof(BuyBigBomb_To_BigBombToWall))]
        [MemberData(nameof(BigBombToDWLakeHylia_To_BigBombToWall))]
        [MemberData(nameof(BigBombToLightWorld_To_BigBombToWall))]
        [MemberData(nameof(LWSouthPortal_To_DWSouthPortal))]
        [MemberData(nameof(DarkWorldSouth_To_DWSouthPortal))]
        [MemberData(nameof(DarkWorldSouth_To_HypeCave))]
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

        public static IEnumerable<object[]> Start_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWSouthPortal_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> DarkWorldEast_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouth,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_DWCentralBonkRocks =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWCentralBonkRocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_BuyBigBomb =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.RedCrystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.RedCrystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.RedCrystal, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.RedCrystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.RedCrystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.RedCrystal, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_BuyBigBomb =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.RedCrystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.RedCrystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.RedCrystal, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BuyBigBomb,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> BuyBigBomb_To_BigBombToLightWorld =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BigBombToLightWorld,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BuyBigBomb_To_BigBombToDWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, false),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, false),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, false),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, false),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.BigBombToDWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> BuyBigBomb_To_BigBombToWall =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BuyBigBombTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigBombToDWLakeHylia_To_BigBombToWall =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToDWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToDWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToDWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToDWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToDWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToDWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigBombToLightWorld_To_BigBombToWall =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 0),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 0),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 0),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.BigBombToWall,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.BigBombToLightWorldTest, 1),
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWSouthPortal_To_DWSouthPortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWSouthPortalTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_DWSouthPortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWSouthPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_HypeCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HypeCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
