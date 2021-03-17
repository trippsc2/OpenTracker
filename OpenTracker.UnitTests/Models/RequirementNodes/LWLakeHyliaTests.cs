using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.RequirementNodes
{
    public class LWLakeHyliaTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(LightWorldNotBunnyToLWLakeHyliaFakeFlippers))]
        [MemberData(nameof(LightWorldNotBunnyToLWLakeHyliaFlippers))]
        [MemberData(nameof(WaterfallFairyNotBunnyToLWLakeHyliaFlippers))]
        [MemberData(nameof(LightWorldNotBunnyToLWLakeHyliaWaterWalk))]
        [MemberData(nameof(WaterfallFairyNoyBunnyToLWLakeHyliaWaterWalk))]
        [MemberData(nameof(LWLakeHyliaFlippersToHobo))]
        [MemberData(nameof(LWLakeHyliaFakeFlippersToHobo))]
        [MemberData(nameof(LWLakeHyliaWaterWalkToHobo))]
        [MemberData(nameof(LWLakeHyliaFlippersToLakeHyliaIsland))]
        [MemberData(nameof(DWLakeHyliaFlippersToLakeHyliaIsland))]
        [MemberData(nameof(LWLakeHyliaFakeFlippersToLakeHyliaIsland))]
        [MemberData(nameof(LWLakeHyliaWaterWalkToLakeHyliaIsland))]
        [MemberData(nameof(DWLakeHyliaWaterWalkToLakeHyliaIsland))]
        [MemberData(nameof(LWLakeHyliaFlippersToLakeHyliaFairyIsland))]
        [MemberData(nameof(IcePalaceIslandToLakeHyliaFairyIsland))]
        [MemberData(nameof(LWLakeHyliaFakeFlippersToLakeHyliaFairyIsland))]
        [MemberData(nameof(LWLakeHyliaWaterWalkToLakeHyliaFairyIsland))]
        [MemberData(nameof(LakeHyliaFairyIslandToLakeHyliaFairyIslandStandardOpen))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> LightWorldNotBunnyToLWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunnyToLWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> WaterfallFairyNotBunnyToLWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunnyToLWLakeHyliaWaterWalk =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, false)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> WaterfallFairyNoyBunnyToLWLakeHyliaWaterWalk =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippersToHobo =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippersToHobo =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalkToHobo =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippersToLakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFlippersToLakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippersToLakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalkToLakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaWaterWalkToLakeHyliaIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippersToLakeHyliaFairyIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIslandToLakeHyliaFairyIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippersToLakeHyliaFairyIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalkToLakeHyliaFairyIsland =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LakeHyliaFairyIslandToLakeHyliaFairyIslandStandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LakeHyliaFairyIsland
                    },
                    RequirementNodeID.LakeHyliaFairyIslandStandardOpen,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LakeHyliaFairyIsland
                    },
                    RequirementNodeID.LakeHyliaFairyIslandStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
