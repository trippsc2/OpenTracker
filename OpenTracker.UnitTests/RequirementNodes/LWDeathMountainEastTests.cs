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
    // public class LWDeathMountainEastTests
    // {
    //     [Theory]
    //     [MemberData(nameof(DeathMountainWestBottomNotBunny_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(DeathMountainEastBottomConnector_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(ParadoxCave_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(DeathMountainEastTop_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(SpiralCaveLedge_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(MimicCaveLedge_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(DarkDeathMountainEastBottom_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(DarkDeathMountainEastBottomInverted_To_DeathMountainEastBottom))]
    //     [MemberData(nameof(DeathMountainEastBottom_To_DeathMountainEastBottomNotBunny))]
    //     [MemberData(nameof(DeathMountainEastBottomNotBunny_To_DeathMountainEastBottomLift2))]
    //     [MemberData(nameof(DeathMountainEastBottomLift2_To_DeathMountainEastBottomConnector))]
    //     [MemberData(nameof(DeathMountainEastTopConnector_To_DeathMountainEastBottomConnector))]
    //     [MemberData(nameof(DarkDeathMountainEastBottomConnector_To_DeathMountainEastBottomConnector))]
    //     [MemberData(nameof(DeathMountainEastBottom_To_ParadoxCave))]
    //     [MemberData(nameof(DeathMountainEastTop_To_ParadoxCave))]
    //     [MemberData(nameof(ParadoxCave_To_ParadoxCaveNotBunny))]
    //     [MemberData(nameof(ParadoxCave_To_ParadoxCaveSuperBunnyFallInHole))]
    //     [MemberData(nameof(ParadoxCaveNotBunny_To_ParadoxCaveTop))]
    //     [MemberData(nameof(ParadoxCaveSuperBunnyFallInHole_To_ParadoxCaveTop))]
    //     [MemberData(nameof(DeathMountainWestTopNotBunny_To_DeathMountainEastTop))]
    //     [MemberData(nameof(ParadoxCave_To_DeathMountainEastTop))]
    //     [MemberData(nameof(LWTurtleRockTopInvertedNotBunny_To_DeathMountainEastTop))]
    //     [MemberData(nameof(DarkDeathMountainTopMirror_To_DeathMountainEastTop))]
    //     [MemberData(nameof(DeathMountainEastTop_To_DeathMountainEastTopInverted))]
    //     [MemberData(nameof(DeathMountainEastTop_To_DeathMountainEastTopNotBunny))]
    //     [MemberData(nameof(DeathMountainEastTop_To_DeathMountainEastTopConnector))]
    //     [MemberData(nameof(TurtleRockSafetyDoor_To_DeathMountainEastTopConnector))]
    //     [MemberData(nameof(DeathMountainEastTop_To_SpiralCaveLedge))]
    //     [MemberData(nameof(TurtleRockTunnelMirror_To_SpiralCaveLedge))]
    //     [MemberData(nameof(SpiralCaveLedge_To_SpiralCave))]
    //     [MemberData(nameof(DeathMountainEastTopInverted_To_MimicCaveLedge))]
    //     [MemberData(nameof(TurtleRockTunnelMirror_To_MimicCaveLedge))]
    //     [MemberData(nameof(MimicCaveLedge_To_MimicCaveLedgeNotBunny))]
    //     [MemberData(nameof(MimicCaveLedgeNotBunny_To_MimicCave))]
    //     [MemberData(nameof(DeathMountainEastTopInverted_To_LWFloatingIsland))]
    //     [MemberData(nameof(DWFloatingIsland_To_LWFloatingIsland))]
    //     [MemberData(nameof(DeathMountainEastTopNotBunny_To_LWTurtleRockTop))]
    //     [MemberData(nameof(DWTurtleRockTopInverted_To_LWTurtleRockTop))]
    //     [MemberData(nameof(LWTurtleRockTop_To_LWTurtleRockTopInverted))]
    //     [MemberData(nameof(LWTurtleRockTopInverted_LWTurtleRockTopInvertedNotBunny))]
    //     [MemberData(nameof(LWTurtleRockTop_To_LWTurtleRockTopStandardOpen))]
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
    //     public static IEnumerable<object[]> DeathMountainWestBottomNotBunny_To_DeathMountainEastBottom =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hookshot, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainWestBottomNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Hookshot, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainWestBottomNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastBottomConnector_To_DeathMountainEastBottom =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     RequirementNodeID.DeathMountainEastBottomConnector
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> ParadoxCave_To_DeathMountainEastBottom =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     RequirementNodeID.ParadoxCave
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTop_To_DeathMountainEastBottom =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> SpiralCaveLedge_To_DeathMountainEastBottom =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> MimicCaveLedge_To_DeathMountainEastBottom =>
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
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainEastBottom_To_DeathMountainEastBottom =>
    //         new List<object[]>
    //         {
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     (ItemType.Mirror, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     (ItemType.Mirror, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainEastBottomInverted_To_DeathMountainEastBottom =>
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
    //                     RequirementNodeID.DarkDeathMountainEastBottomInverted
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
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
    //                     RequirementNodeID.DarkDeathMountainEastBottomInverted
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastBottom_To_DeathMountainEastBottomNotBunny =>
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastBottomNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomNotBunny,
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastBottomNotBunny_To_DeathMountainEastBottomLift2 =>
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
    //                     RequirementNodeID.DeathMountainEastBottomNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomLift2,
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
    //                     RequirementNodeID.DeathMountainEastBottomNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomLift2,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastBottomLift2_To_DeathMountainEastBottomConnector =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastBottomConnector,
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
    //                     RequirementNodeID.DeathMountainEastBottomLift2
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomConnector,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTopConnector_To_DeathMountainEastBottomConnector =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastBottomConnector,
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
    //                     RequirementNodeID.DeathMountainEastTopConnector
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomConnector,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainEastBottomConnector_To_DeathMountainEastBottomConnector =>
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
    //                     (ItemType.Mirror, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkDeathMountainEastBottomConnector
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomConnector,
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
    //                     RequirementNodeID.DarkDeathMountainEastBottomConnector
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomConnector,
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
    //                     (ItemType.Mirror, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DarkDeathMountainEastBottomConnector
    //                 },
    //                 RequirementNodeID.DeathMountainEastBottomConnector,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastBottom_To_ParadoxCave =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     EntranceShuffle = EntranceShuffle.All
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.ParadoxCave,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     EntranceShuffle = EntranceShuffle.None
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.ParadoxCave,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     EntranceShuffle = EntranceShuffle.Dungeon
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.ParadoxCave,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTop_To_ParadoxCave =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     EntranceShuffle = EntranceShuffle.All
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.ParadoxCave,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     EntranceShuffle = EntranceShuffle.None
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.ParadoxCave,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     EntranceShuffle = EntranceShuffle.Dungeon
    //                 },
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.ParadoxCave,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> ParadoxCave_To_ParadoxCaveNotBunny =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.ParadoxCaveNotBunny,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ParadoxCave
    //                 },
    //                 RequirementNodeID.ParadoxCaveSuperBunnyFallInHole,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ParadoxCave
    //                 },
    //                 RequirementNodeID.ParadoxCaveSuperBunnyFallInHole,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> ParadoxCave_To_ParadoxCaveSuperBunnyFallInHole =>
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.ParadoxCaveNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ParadoxCave
    //                 },
    //                 RequirementNodeID.ParadoxCaveNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ParadoxCave
    //                 },
    //                 RequirementNodeID.ParadoxCaveNotBunny,
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ParadoxCave
    //                 },
    //                 RequirementNodeID.ParadoxCaveNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> ParadoxCaveNotBunny_To_ParadoxCaveTop =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.ParadoxCaveTop,
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
    //                     RequirementNodeID.ParadoxCaveNotBunny
    //                 },
    //                 RequirementNodeID.ParadoxCaveTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> ParadoxCaveSuperBunnyFallInHole_To_ParadoxCaveTop =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ParadoxCaveSuperBunnyFallInHole
    //                 },
    //                 RequirementNodeID.ParadoxCaveTop,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 3)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.ParadoxCaveSuperBunnyFallInHole
    //                 },
    //                 RequirementNodeID.ParadoxCaveTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainWestTopNotBunny_To_DeathMountainEastTop =>
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
    //                     RequirementNodeID.DeathMountainWestTopNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastTop,
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
    //                     RequirementNodeID.DeathMountainWestTopNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> ParadoxCave_To_DeathMountainEastTop =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastTop,
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
    //                     RequirementNodeID.ParadoxCave
    //                 },
    //                 RequirementNodeID.DeathMountainEastTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LWTurtleRockTopInvertedNotBunny_To_DeathMountainEastTop =>
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
    //                     RequirementNodeID.LWTurtleRockTopInvertedNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastTop,
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
    //                     RequirementNodeID.LWTurtleRockTopInvertedNotBunny
    //                 },
    //                 RequirementNodeID.DeathMountainEastTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainTopMirror_To_DeathMountainEastTop =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastTop,
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
    //                     RequirementNodeID.DarkDeathMountainTopMirror
    //                 },
    //                 RequirementNodeID.DeathMountainEastTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTop_To_DeathMountainEastTopInverted =>
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
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopInverted,
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
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopInverted,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTop_To_DeathMountainEastTopNotBunny =>
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastTopNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopNotBunny,
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTop_To_DeathMountainEastTopConnector =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DeathMountainEastTopConnector,
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
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopConnector,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> TurtleRockSafetyDoor_To_DeathMountainEastTopConnector =>
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
    //                     (ItemType.Mirror, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.TurtleRockSafetyDoor
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopConnector,
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
    //                     RequirementNodeID.TurtleRockSafetyDoor
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopConnector,
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
    //                     (ItemType.Mirror, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.TurtleRockSafetyDoor
    //                 },
    //                 RequirementNodeID.DeathMountainEastTopConnector,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTop_To_SpiralCaveLedge =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.SpiralCaveLedge,
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
    //                     RequirementNodeID.DeathMountainEastTop
    //                 },
    //                 RequirementNodeID.SpiralCaveLedge,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> TurtleRockTunnelMirror_To_SpiralCaveLedge =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.SpiralCaveLedge,
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
    //                     RequirementNodeID.TurtleRockTunnelMirror
    //                 },
    //                 RequirementNodeID.SpiralCaveLedge,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> SpiralCaveLedge_To_SpiralCave =>
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.SpiralCave,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.SpiralCave,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.SpiralCave,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.SpiralCave,
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.SpiralCave,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTopInverted_To_MimicCaveLedge =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.MimicCaveLedge,
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
    //                     RequirementNodeID.DeathMountainEastTopInverted
    //                 },
    //                 RequirementNodeID.MimicCaveLedge,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> TurtleRockTunnelMirror_To_MimicCaveLedge =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.MimicCaveLedge,
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
    //                     RequirementNodeID.TurtleRockTunnelMirror
    //                 },
    //                 RequirementNodeID.MimicCaveLedge,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> MimicCaveLedge_To_MimicCaveLedgeNotBunny =>
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.MimicCaveLedgeNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.MimicCaveLedgeNotBunny,
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
    //                     (ItemType.MoonPearl, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.MimicCaveLedgeNotBunny,
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
    //                     (ItemType.MoonPearl, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.MimicCaveLedgeNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> MimicCaveLedgeNotBunny_To_MimicCave =>
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
    //                     RequirementNodeID.MimicCaveLedgeNotBunny
    //                 },
    //                 RequirementNodeID.MimicCave,
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
    //                     RequirementNodeID.MimicCaveLedgeNotBunny
    //                 },
    //                 RequirementNodeID.MimicCave,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTopInverted_To_LWFloatingIsland =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.LWFloatingIsland,
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
    //                     RequirementNodeID.DeathMountainEastTopInverted
    //                 },
    //                 RequirementNodeID.LWFloatingIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWFloatingIsland_To_LWFloatingIsland =>
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
    //                     (ItemType.Mirror, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWFloatingIsland
    //                 },
    //                 RequirementNodeID.LWFloatingIsland,
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
    //                     RequirementNodeID.DWFloatingIsland
    //                 },
    //                 RequirementNodeID.LWFloatingIsland,
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
    //                     (ItemType.Mirror, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWFloatingIsland
    //                 },
    //                 RequirementNodeID.LWFloatingIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTopNotBunny_To_LWTurtleRockTop =>
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
    //                     RequirementNodeID.DeathMountainEastTopNotBunny
    //                 },
    //                 RequirementNodeID.LWTurtleRockTop,
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
    //                     RequirementNodeID.DeathMountainEastTopNotBunny
    //                 },
    //                 RequirementNodeID.LWTurtleRockTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWTurtleRockTopInverted_To_LWTurtleRockTop =>
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
    //                     RequirementNodeID.DWTurtleRockTopInverted
    //                 },
    //                 RequirementNodeID.LWTurtleRockTop,
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
    //                     RequirementNodeID.DWTurtleRockTopInverted
    //                 },
    //                 RequirementNodeID.LWTurtleRockTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LWTurtleRockTop_To_LWTurtleRockTopInverted =>
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
    //                     RequirementNodeID.LWTurtleRockTop
    //                 },
    //                 RequirementNodeID.LWTurtleRockTopInverted,
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
    //                     RequirementNodeID.LWTurtleRockTop
    //                 },
    //                 RequirementNodeID.LWTurtleRockTopInverted,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LWTurtleRockTopInverted_LWTurtleRockTopInvertedNotBunny =>
    //         new List<object[]>
    //         {
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
    //                     RequirementNodeID.LWTurtleRockTopInverted
    //                 },
    //                 RequirementNodeID.LWTurtleRockTopInvertedNotBunny,
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
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.LWTurtleRockTopInverted
    //                 },
    //                 RequirementNodeID.LWTurtleRockTopInvertedNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LWTurtleRockTop_To_LWTurtleRockTopStandardOpen =>
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
    //                     RequirementNodeID.LWTurtleRockTop
    //                 },
    //                 RequirementNodeID.LWTurtleRockTopStandardOpen,
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
    //                     RequirementNodeID.LWTurtleRockTop
    //                 },
    //                 RequirementNodeID.LWTurtleRockTopStandardOpen,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    // }
}
