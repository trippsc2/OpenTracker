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
    // [Collection("Tests")]
    // public class DarkWorldSouthEastTests
    // {
    //     [Theory]
    //     [MemberData(nameof(DarkWorldWestNotBunny_To_DWLakeHyliaFlippers))]
    //     [MemberData(nameof(DarkWorldSouthNotBunny_To_DWLakeHyliaFlippers))]
    //     [MemberData(nameof(DWWitchAreaNotBunny_To_DWLakeHyliaFlippers))]
    //     [MemberData(nameof(DarkWorldEastNotBunny_To_DWLakeHyliaFlippers))]
    //     [MemberData(nameof(DarkWorldSouthEastNotBunny_To_DWLakeHyliaFlippers))]
    //     [MemberData(nameof(IcePalaceIslandInverted_To_DWLakeHyliaFlippers))]
    //     [MemberData(nameof(DarkWorldWestNotBunny_To_DWLakeHyliaFakeFlippers))]
    //     [MemberData(nameof(DarkWorldSouthNotBunny_To_DWLakeHyliaFakeFlippers))]
    //     [MemberData(nameof(DWWitchAreaNotBunny_To_DWLakeHyliaFakeFlippers))]
    //     [MemberData(nameof(DarkWorldEastNotBunny_To_DWLakeHyliaFakeFlippers))]
    //     [MemberData(nameof(DarkWorldSouthEastNotBunny_To_DWLakeHyliaFakeFlippers))]
    //     [MemberData(nameof(IcePalaceIslandInverted_To_DWLakeHyliaFakeFlippers))]
    //     [MemberData(nameof(DarkWorldWestNotBunny_To_DWLakeHyliaWaterWalk))]
    //     [MemberData(nameof(LakeHyliaFairyIsland_To_IcePalaceIsland))]
    //     [MemberData(nameof(LakeHyliaFairyIslandStandardOpen_To_IcePalaceIsland))]
    //     [MemberData(nameof(DWLakeHyliaFlippers_To_IcePalaceIsland))]
    //     [MemberData(nameof(DWLakeHyliaFakeFlippers_To_IcePalaceIsland))]
    //     [MemberData(nameof(DWLakeHyliaWaterWalk_To_IcePalaceIsland))]
    //     [MemberData(nameof(IcePalaceIsland_To_IcePalaceIslandInverted))]
    //     [MemberData(nameof(LightWorldMirror_To_DarkWorldSouthEast))]
    //     [MemberData(nameof(DWLakeHyliaFlippers_To_DarkWorldSouthEast))]
    //     [MemberData(nameof(DWLakeHyliaFakeFlippers_To_DarkWorldSouthEast))]
    //     [MemberData(nameof(DWLakeHyliaWaterWalk_To_DarkWorldSouthEast))]
    //     [MemberData(nameof(DarkWorldSouthEast_To_DarkWorldSouthEastNotBunny))]
    //     [MemberData(nameof(DarkWorldSouthEastNotBunny_To_DarkWorldSouthEastLift1))]
    //     public void Tests(
    //         ModeSaveData mode, (ItemType, int)[] items, (PrizeType, int)[] prizes,
    //         (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
    //         RequirementNodeID id, AccessibilityLevel expected)
    //     {
    //         ItemDictionary.Instance.Reset();
    //         PrizeDictionary.Instance.Reset();
    //         SequenceBreakDictionary.Instance.Reset();
    //         RequirementNodeDictionary.Instance.Reset();
    //         Mode.Instance.Load(mode);
    //
    //         foreach (var item in items)
    //         {
    //             ItemDictionary.Instance[item.Item1].Current = item.Item2;
    //         }
    //
    //         foreach (var prize in prizes)
    //         {
    //             PrizeDictionary.Instance[prize.Item1].Current = prize.Item2;
    //         }
    //
    //         foreach (var sequenceBreak in sequenceBreaks)
    //         {
    //             SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
    //                 sequenceBreak.Item2;
    //         }
    //
    //         foreach (var node in accessibleNodes)
    //         {
    //             RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
    //         }
    //
    //         Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
    //     }
    //
    //     public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWLakeHyliaFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldSouthNotBunny_To_DWLakeHyliaFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWWitchAreaNotBunny_To_DWLakeHyliaFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEastNotBunny_To_DWLakeHyliaFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldSouthEastNotBunny_To_DWLakeHyliaFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> IcePalaceIslandInverted_To_DWLakeHyliaFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Flippers, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFlippers,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWLakeHyliaFakeFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, false),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersQirnJump, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldSouthNotBunny_To_DWLakeHyliaFakeFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWWitchAreaNotBunny_To_DWLakeHyliaFakeFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEastNotBunny_To_DWLakeHyliaFakeFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldSouthEastNotBunny_To_DWLakeHyliaFakeFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> IcePalaceIslandInverted_To_DWLakeHyliaFakeFlippers =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, false),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 1),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 1),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 1),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 1),
    //                     (ItemType.Bottle, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Bow, 0),
    //                     (ItemType.RedBoomerang, 0),
    //                     (ItemType.CaneOfSomaria, 0),
    //                     (ItemType.IceRod, 0),
    //                     (ItemType.Bottle, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.FakeFlippersFairyRevival, true),
    //                     (SequenceBreakType.FakeFlippersSplashDeletion, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIslandInverted
    //                 },
    //                 RequirementNodeID.DWLakeHyliaFakeFlippers,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWLakeHyliaWaterWalk =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.WaterWalk, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaWaterWalk,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.WaterWalk, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaWaterWalk,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.WaterWalk, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldWestNotBunny
    //                 },
    //                 RequirementNodeID.DWLakeHyliaWaterWalk,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LakeHyliaFairyIsland_To_IcePalaceIsland =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.StandardOpen
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Mirror, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LakeHyliaFairyIsland
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Mirror, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LakeHyliaFairyIsland
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Mirror, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LakeHyliaFairyIsland
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LakeHyliaFairyIslandStandardOpen_To_IcePalaceIsland =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Gloves, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LakeHyliaFairyIslandStandardOpen
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Gloves, 2)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LakeHyliaFairyIslandStandardOpen
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFlippers_To_IcePalaceIsland =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.StandardOpen
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaFlippers
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaFlippers
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFakeFlippers_To_IcePalaceIsland =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.StandardOpen
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaFakeFlippers
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaFakeFlippers
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_IcePalaceIsland =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.StandardOpen
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaWaterWalk
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaWaterWalk
    //                 },
    //                 RequirementNodeID.IcePalaceIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> IcePalaceIsland_To_IcePalaceIslandInverted =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.StandardOpen
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIsland
    //                 },
    //                 RequirementNodeID.IcePalaceIslandInverted,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.IcePalaceIsland
    //                 },
    //                 RequirementNodeID.IcePalaceIslandInverted,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LightWorldMirror_To_DarkWorldSouthEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LightWorldMirror
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFlippers_To_DarkWorldSouthEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaFlippers
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFakeFlippers_To_DarkWorldSouthEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaFakeFlippers
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_DarkWorldSouthEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWLakeHyliaWaterWalk
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldSouthEast_To_DarkWorldSouthEastNotBunny =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.StandardOpen
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEast
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEastNotBunny,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldSouthEastNotBunny,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.StandardOpen
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEast
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEastNotBunny,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     WorldState = WorldState.Inverted
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEast
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEastNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldSouthEastNotBunny_To_DarkWorldSouthEastLift1 =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Gloves, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEastLift1,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Gloves, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldSouthEastNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldSouthEastLift1,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    // }
}
