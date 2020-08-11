using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class DarkWorldNorthEastTests
    {
        [Theory]
        [MemberData(nameof(Start_To_DWWitchArea))]
        [MemberData(nameof(LWWitchArea_To_DWWitchArea))]
        [MemberData(nameof(DarkWorldWest_To_DWWitchArea))]
        [MemberData(nameof(DarkWorldEast_To_DWWitchArea))]
        [MemberData(nameof(Catfish_To_DWWitchArea))]
        [MemberData(nameof(Zora_To_Catfish))]
        [MemberData(nameof(DWWitchArea_To_Catfish))]
        [MemberData(nameof(Start_To_DarkWorldEast))]
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

        public static IEnumerable<object[]> Start_To_DWWitchArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWWitchArea_To_DWWitchArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWWitchAreaTest, 1),
                        (ItemType.Mirror, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWWitchAreaTest, 1),
                        (ItemType.Mirror, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWWitchAreaTest, 0),
                        (ItemType.Mirror, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWWitchAreaTest, 1),
                        (ItemType.Mirror, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWWitchAreaTest, 1),
                        (ItemType.Mirror, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_DWWitchArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
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
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldEast_To_DWWitchArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 1),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 1),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Gloves, 0),
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
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 1),
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
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Gloves, 0),
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
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 1),
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
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Gloves, 0),
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
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 1),
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
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Gloves, 0),
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

        public static IEnumerable<object[]> Catfish_To_DWWitchArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWWitchArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.CatfishTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Zora_To_Catfish =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.ZoraTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.ZoraTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.ZoraTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.ZoraTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.ZoraTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWWitchArea_To_Catfish =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.Catfish,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWWitchAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1),
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DarkWorldEast =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DarkWorldEast,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldEastAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
