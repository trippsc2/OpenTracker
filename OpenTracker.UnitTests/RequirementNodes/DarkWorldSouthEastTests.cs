using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class DarkWorldSouthEastTests
    {
        [Theory]
        [MemberData(nameof(LWLakeHylia_To_DWLakeHylia))]
        [MemberData(nameof(DarkWorldSouth_To_DWLakeHylia))]
        [MemberData(nameof(DarkWorldEast_To_DWLakeHylia))]
        [MemberData(nameof(DarkWorldSouthEast_To_DWLakeHylia))]
        [MemberData(nameof(DarkWorldWest_To_DWLakeHylia))]
        [MemberData(nameof(DarkWorldWest_To_DWLakeHyliaWaterWalk))]
        [MemberData(nameof(Start_To_IcePalaceEntrance))]
        [MemberData(nameof(LakeHyliaFairyIsland_To_IcePalaceEntrance))]
        [MemberData(nameof(DWLakeHylia_To_IcePalaceEntrance))]
        [MemberData(nameof(Start_To_DarkWorldSouthEast))]
        [MemberData(nameof(LightWorld_To_DarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHylia_To_DarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHyliaWaterWalk_To_DarkWorldSouthEast))]
        [MemberData(nameof(DarkWorldSouthEast_To_DWIceRodCave))]
        [MemberData(nameof(DarkWorldSouthEast_To_DWIceRodRock))]
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

        public static IEnumerable<object[]> LWLakeHylia_To_DWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_DWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldEast_To_DWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthEast_To_DWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_DWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_DWLakeHyliaWaterWalk =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> Start_To_IcePalaceEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LakeHyliaFairyIsland_To_IcePalaceEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWLakeHylia_To_IcePalaceEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IcePalaceEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_DarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWLakeHylia_To_DarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_DarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldSouthEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthEast_To_DWIceRodCave =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodCave,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthEast_To_DWIceRodRock =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWIceRodRock,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
