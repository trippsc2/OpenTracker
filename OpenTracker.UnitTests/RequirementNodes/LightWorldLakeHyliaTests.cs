using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class LightWorldLakeHyliaTests
    {
        [Theory]
        [MemberData(nameof(LightWorld_To_LWLakeHylia))]
        [MemberData(nameof(DWLakeHylia_To_LWLakeHylia))]
        [MemberData(nameof(LightWorld_To_LWLakeHyliaWaterWalk))]
        [MemberData(nameof(WaterfallFairy_To_LWLakeHyliaWaterWalk))]
        [MemberData(nameof(LWLakeHylia_To_LakeHyliaIsland))]
        [MemberData(nameof(LWLakeHyliaWaterWalk_To_LakeHyliaIsland))]
        [MemberData(nameof(DWLakeHylia_To_LakeHyliaIsland))]
        [MemberData(nameof(DWLakeHyliaWaterWalk_To_LakeHyliaIsland))]
        [MemberData(nameof(Start_To_LakeHyliaFairyIsland))]
        [MemberData(nameof(LWLakeHylia_To_LakeHyliaFairyIsland))]
        [MemberData(nameof(IcePalaceEntrance_To_LakeHyliaFairyIsland))]
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

        public static IEnumerable<object[]> LightWorld_To_LWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Bow, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWLakeHylia_To_LWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHylia,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LWLakeHyliaWaterWalk =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, false),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, false),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, false),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true),
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> WaterfallFairy_To_LWLakeHyliaWaterWalk =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 0),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 0),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 0),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 0),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.WaterfallFairyTest, 1),
                        (ItemType.WaterfallFairyAccess, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> LWLakeHylia_To_LakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWLakeHyliaWaterWalk_To_LakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaWaterWalkTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaWaterWalkTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaWaterWalkTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaWaterWalkTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWLakeHylia_To_LakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_LakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWLakeHyliaWaterWalkTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_LakeHyliaFairyIsland =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LakeHyliaFairyIslandAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWLakeHylia_To_LakeHyliaFairyIsland =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWLakeHyliaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IcePalaceEntrance_To_LakeHyliaFairyIsland =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.LakeHyliaFairyIsland,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
