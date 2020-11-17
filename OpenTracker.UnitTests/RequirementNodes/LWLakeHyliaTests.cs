using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    [Collection("Tests")]
    public class LWLakeHyliaTests
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
        public void Tests(
            ModeSaveData mode, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();
            PrizeDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            RequirementNodeDictionary.Instance.Reset();
            Mode.Instance.Load(mode);

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var prize in prizes)
            {
                PrizeDictionary.Instance[prize.Item1].Current = prize.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            foreach (var node in accessibleNodes)
            {
                RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
            }

            Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
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
                    AccessibilityLevel.Normal
                }
            };
    }
}
