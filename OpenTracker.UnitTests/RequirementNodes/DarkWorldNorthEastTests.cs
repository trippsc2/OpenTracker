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
    // public class DarkWorldNorthEastTests
    // {
    //     [Theory]
    //     [MemberData(nameof(LWWitchArea_To_DWWitchArea))]
    //     [MemberData(nameof(DarkWorldEastNotBunny_To_DWWitchArea))]
    //     [MemberData(nameof(DWLakeHyliaFlippers_To_DWWitchArea))]
    //     [MemberData(nameof(DWLakeHyliaFakeFlippers_To_DWWitchArea))]
    //     [MemberData(nameof(DWLakeHyliaWaterWalk_To_DWWitchArea))]
    //     [MemberData(nameof(DWWitchArea_To_DWWitchAreaNotBunny))]
    //     [MemberData(nameof(ZoraArea_To_CatfishArea))]
    //     [MemberData(nameof(DWWitchAreaNotBunny_To_CatfishArea))]
    //     [MemberData(nameof(LightWorldStandardOpen_To_DarkWorldEast))]
    //     [MemberData(nameof(LightWorldMirror_To_DarkWorldEast))]
    //     [MemberData(nameof(DWWitchAreaNotBunny_To_DarkWorldEast))]
    //     [MemberData(nameof(DarkWorldSouthHammer_To_DarkWorldEast))]
    //     [MemberData(nameof(DWEastPortalNotBunny_To_DarkWorldEast))]
    //     [MemberData(nameof(DWLakeHyliaFlippers_To_DarkWorldEast))]
    //     [MemberData(nameof(DWLakeHyliaFakeFlippers_To_DarkWorldEast))]
    //     [MemberData(nameof(DWLakeHyliaWaterWalk_To_DarkWorldEast))]
    //     [MemberData(nameof(DarkWorldEast_To_DarkWorldEastStandardOpen))]
    //     [MemberData(nameof(DarkWorldEast_To_DarkWorldEastNotBunny))]
    //     [MemberData(nameof(DarkWorldEastNotBunny_To_DarkWorldEastHammer))]
    //     [MemberData(nameof(DarkWorldEastNotBunny_To_FatFairyEntrance))]
    //     [MemberData(nameof(LWEastPortalStandardOpen_To_DWEastPortal))]
    //     [MemberData(nameof(DarkWorldEastHammer_To_DWEastPortal))]
    //     [MemberData(nameof(DWEastPortal_To_DWEastPortalInverted))]
    //     [MemberData(nameof(DWEastPortal_To_DWEastPortalNotBunny))]
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
    //     public static IEnumerable<object[]> LWWitchArea_To_DWWitchArea =>
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
    //                     RequirementNodeID.LWWitchArea
    //                 },
    //                 RequirementNodeID.DWWitchArea,
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
    //                     RequirementNodeID.LWWitchArea
    //                 },
    //                 RequirementNodeID.DWWitchArea,
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
    //                     RequirementNodeID.LWWitchArea
    //                 },
    //                 RequirementNodeID.DWWitchArea,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEastNotBunny_To_DWWitchArea =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 0),
    //                     (ItemType.Gloves, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWWitchArea,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 1),
    //                     (ItemType.Gloves, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWWitchArea,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 0),
    //                     (ItemType.Gloves, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DWWitchArea,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFlippers_To_DWWitchArea =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DWWitchArea,
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
    //                 RequirementNodeID.DWWitchArea,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFakeFlippers_To_DWWitchArea =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DWWitchArea,
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
    //                 RequirementNodeID.DWWitchArea,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_DWWitchArea =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DWWitchArea,
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
    //                 RequirementNodeID.DWWitchArea,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWWitchArea_To_DWWitchAreaNotBunny =>
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
    //                     RequirementNodeID.DWWitchArea
    //                 },
    //                 RequirementNodeID.DWWitchAreaNotBunny,
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
    //                 RequirementNodeID.DWWitchAreaNotBunny,
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
    //                     RequirementNodeID.DWWitchArea
    //                 },
    //                 RequirementNodeID.DWWitchAreaNotBunny,
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
    //                     RequirementNodeID.DWWitchArea
    //                 },
    //                 RequirementNodeID.DWWitchAreaNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> ZoraArea_To_CatfishArea =>
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
    //                     RequirementNodeID.ZoraArea
    //                 },
    //                 RequirementNodeID.CatfishArea,
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
    //                     RequirementNodeID.ZoraArea
    //                 },
    //                 RequirementNodeID.CatfishArea,
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
    //                     (ItemType.Mirror, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ZoraArea
    //                 },
    //                 RequirementNodeID.CatfishArea,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWWitchAreaNotBunny_To_CatfishArea =>
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
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.CatfishArea,
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
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.CatfishArea,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LightWorldStandardOpen_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[]
    //                 {
    //                     (PrizeType.Aga1, 0)
    //                 },
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LightWorldStandardOpen
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[]
    //                 {
    //                     (PrizeType.Aga1, 1)
    //                 },
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LightWorldStandardOpen
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LightWorldMirror_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldEast,
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
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWWitchAreaNotBunny_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 0),
    //                     (ItemType.Gloves, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 1),
    //                     (ItemType.Gloves, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 0),
    //                     (ItemType.Gloves, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWWitchAreaNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldSouthHammer_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldEast,
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
    //                     RequirementNodeID.DarkWorldSouthHammer
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWEastPortalNotBunny_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWEastPortalNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWEastPortalNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFlippers_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldEast,
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
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaFakeFlippers_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldEast,
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
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_DarkWorldEast =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkWorldEast,
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
    //                 RequirementNodeID.DarkWorldEast,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEast_To_DarkWorldEastStandardOpen =>
    //         new List<object[]>
    //         {
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
    //                     RequirementNodeID.DarkWorldEast
    //                 },
    //                 RequirementNodeID.DarkWorldEastStandardOpen,
    //                 AccessibilityLevel.None
    //             },
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
    //                     RequirementNodeID.DarkWorldEast
    //                 },
    //                 RequirementNodeID.DarkWorldEastStandardOpen,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEast_To_DarkWorldEastNotBunny =>
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
    //                     RequirementNodeID.DarkWorldEast
    //                 },
    //                 RequirementNodeID.DarkWorldEastNotBunny,
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
    //                 RequirementNodeID.DarkWorldEastNotBunny,
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
    //                     RequirementNodeID.DarkWorldEast
    //                 },
    //                 RequirementNodeID.DarkWorldEastNotBunny,
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
    //                     RequirementNodeID.DarkWorldEast
    //                 },
    //                 RequirementNodeID.DarkWorldEastNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEastNotBunny_To_DarkWorldEastHammer =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldEastHammer,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hammer, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.DarkWorldEastHammer,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEastNotBunny_To_FatFairyEntrance =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[]
    //                 {
    //                     (PrizeType.RedCrystal, 1)
    //                 },
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.FatFairyEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[]
    //                 {
    //                     (PrizeType.RedCrystal, 2)
    //                 },
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkWorldEastNotBunny
    //                 },
    //                 RequirementNodeID.FatFairyEntrance,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LWEastPortalStandardOpen_To_DWEastPortal =>
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
    //                     RequirementNodeID.LWEastPortalStandardOpen
    //                 },
    //                 RequirementNodeID.DWEastPortal,
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
    //                     RequirementNodeID.LWEastPortalStandardOpen
    //                 },
    //                 RequirementNodeID.DWEastPortal,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkWorldEastHammer_To_DWEastPortal =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DWEastPortal,
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
    //                     RequirementNodeID.DarkWorldEastHammer
    //                 },
    //                 RequirementNodeID.DWEastPortal,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWEastPortal_To_DWEastPortalInverted =>
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
    //                     RequirementNodeID.DWEastPortal
    //                 },
    //                 RequirementNodeID.DWEastPortalInverted,
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
    //                     RequirementNodeID.DWEastPortal
    //                 },
    //                 RequirementNodeID.DWEastPortalInverted,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWEastPortal_To_DWEastPortalNotBunny =>
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
    //                     RequirementNodeID.DWEastPortal
    //                 },
    //                 RequirementNodeID.DWEastPortalNotBunny,
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
    //                 RequirementNodeID.DWEastPortalNotBunny,
    //                 AccessibilityLevel.None
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
    //                     RequirementNodeID.DWEastPortal
    //                 },
    //                 RequirementNodeID.DWEastPortalNotBunny,
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
    //                     RequirementNodeID.DWEastPortal
    //                 },
    //                 RequirementNodeID.DWEastPortalNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    // }
}
