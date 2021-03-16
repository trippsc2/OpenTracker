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
        [MemberData(nameof(LightWorldNotBunny_To_LWLakeHyliaFakeFlippers))]
        [MemberData(nameof(LightWorldNotBunny_To_LWLakeHyliaFlippers))]
        [MemberData(nameof(WaterfallFairyNotBunny_To_LWLakeHyliaFlippers))]
        [MemberData(nameof(LightWorldNotBunny_To_LWLakeHyliaWaterWalk))]
        [MemberData(nameof(WaterfallFairyNoyBunny_To_LWLakeHyliaWaterWalk))]
        [MemberData(nameof(LWLakeHyliaFlippers_To_Hobo))]
        [MemberData(nameof(LWLakeHyliaFakeFlippers_To_Hobo))]
        [MemberData(nameof(LWLakeHyliaWaterWalk_To_Hobo))]
        [MemberData(nameof(LWLakeHyliaFlippers_To_LakeHyliaIsland))]
        [MemberData(nameof(DWLakeHyliaFlippers_To_LakeHyliaIsland))]
        [MemberData(nameof(LWLakeHyliaFakeFlippers_To_LakeHyliaIsland))]
        [MemberData(nameof(LWLakeHyliaWaterWalk_To_LakeHyliaIsland))]
        [MemberData(nameof(DWLakeHyliaWaterWalk_To_LakeHyliaIsland))]
        [MemberData(nameof(LWLakeHyliaFlippers_To_LakeHyliaFairyIsland))]
        [MemberData(nameof(IcePalaceIsland_To_LakeHyliaFairyIsland))]
        [MemberData(nameof(LWLakeHyliaFakeFlippers_To_LakeHyliaFairyIsland))]
        [MemberData(nameof(LWLakeHyliaWaterWalk_To_LakeHyliaFairyIsland))]
        [MemberData(nameof(LakeHyliaFairyIsland_To_LakeHyliaFairyIslandStandardOpen))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_LWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersScreenTransition, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_LWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> WaterfallFairyNotBunny_To_LWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_LWLakeHyliaWaterWalk =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> WaterfallFairyNoyBunny_To_LWLakeHyliaWaterWalk =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.WaterfallFairyNotBunny
                    },
                    RequirementNodeID.LWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippers_To_Hobo =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippers_To_Hobo =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalk_To_Hobo =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.Hobo,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippers_To_LakeHyliaIsland =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFlippers_To_LakeHyliaIsland =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippers_To_LakeHyliaIsland =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalk_To_LakeHyliaIsland =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_LakeHyliaIsland =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippers_To_LakeHyliaFairyIsland =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIsland_To_LakeHyliaFairyIsland =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippers_To_LakeHyliaFairyIsland =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalk_To_LakeHyliaFairyIsland =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.LakeHyliaFairyIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LakeHyliaFairyIsland_To_LakeHyliaFairyIslandStandardOpen =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
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
