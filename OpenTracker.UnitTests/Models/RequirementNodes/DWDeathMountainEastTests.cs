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
    // public class DWDeathMountainEastTests
    // {
    //     [Theory]
    //     [MemberData(nameof(DarkDeathMountainTop_To_SuperBunnyCave))]
    //     [MemberData(nameof(DarkDeathMountainEastBottom_To_SuperBunnyCave))]
    //     [MemberData(nameof(SuperBunnyCave_To_SuperBunnyCaveChests))]
    //     [MemberData(nameof(LWFloatingIsland_To_DWFloatingIsland))]
    //     [MemberData(nameof(HookshotCaveEntrance_To_DWFloatingIsland))]
    //     [MemberData(nameof(DarkDeathMountainTopNotBunny_To_HookshotCaveEntrance))]
    //     [MemberData(nameof(HookshotCaveEntrance_To_HookshotCaveEntranceHookshot))]
    //     [MemberData(nameof(HookshotCaveEntrance_To_HookshotCaveEntranceHover))]
    //     [MemberData(nameof(HookshotCaveEntranceHookshot_To_HookshotCaveBonkableChest))]
    //     [MemberData(nameof(HookshotCaveEntrance_To_HookshotCaveBonkableChest))]
    //     [MemberData(nameof(HookshotCaveEntranceHover_To_HookshotCaveBonkableChest))]
    //     [MemberData(nameof(HookshotCaveEntranceHookshot_To_HookshotCaveBack))]
    //     [MemberData(nameof(HookshotCaveEntranceHover_To_HookshotCaveBack))]
    //     [MemberData(nameof(LWTurtleRockTopStandardOpen_To_DWTurtleRockTop))]
    //     [MemberData(nameof(DarkDeathMountainTopInverted_To_DWTurtleRockTop))]
    //     [MemberData(nameof(DWTurtleRockTop_To_DWTurtleRockTopInverted))]
    //     [MemberData(nameof(DWTurtleRockTop_To_DWTurtleRockTopNotBunny))]
    //     [MemberData(nameof(DWTurtleRockTopNotBunny_To_TurtleRockFrontEntrance))]
    //     [MemberData(nameof(DeathMountainEastBottom_To_DarkDeathMountainEastBottom))]
    //     [MemberData(nameof(DeathMountainEastBottomLift2_To_DarkDeathMountainEastBottom))]
    //     [MemberData(nameof(DarkDeathMountainTop_To_DarkDeathMountainEastBottom))]
    //     [MemberData(nameof(DarkDeathMountainEastBottom_To_DarkDeathMountainEastBottomInverted))]
    //     [MemberData(nameof(DarkDeathMountainEastBottom_To_DarkDeathMountainEastBottomConnector))]
    //     [MemberData(nameof(SpiralCaveLedge_To_TurtleRockTunnel))]
    //     [MemberData(nameof(MimicCaveLedge_To_TurtleRockTunnel))]
    //     [MemberData(nameof(TurtleRockTunnel_To_TurtleRockTunnelMirror))]
    //     [MemberData(nameof(TRKeyDoorsToMiddleExit_To_TurtleRockTunnelMirror))]
    //     [MemberData(nameof(DeathMountainEastTopConnector_To_TurtleRockSafetyDoor))]
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
    //     public static IEnumerable<object[]> DarkDeathMountainTop_To_SuperBunnyCave =>
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
    //                     RequirementNodeID.DarkDeathMountainTop
    //                 },
    //                 RequirementNodeID.SuperBunnyCave,
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
    //                     RequirementNodeID.DarkDeathMountainTop
    //                 },
    //                 RequirementNodeID.SuperBunnyCave,
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
    //                     RequirementNodeID.DarkDeathMountainTop
    //                 },
    //                 RequirementNodeID.SuperBunnyCave,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainEastBottom_To_SuperBunnyCave =>
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.SuperBunnyCave,
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.SuperBunnyCave,
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.SuperBunnyCave,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> SuperBunnyCave_To_SuperBunnyCaveChests =>
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
    //                 RequirementNodeID.SuperBunnyCaveChests,
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
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SuperBunnyCave
    //                 },
    //                 RequirementNodeID.SuperBunnyCaveChests,
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
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.SuperBunnyCaveChests,
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
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SuperBunnyCave
    //                 },
    //                 RequirementNodeID.SuperBunnyCaveChests,
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
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SuperBunnyCave
    //                 },
    //                 RequirementNodeID.SuperBunnyCaveChests,
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
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.SuperBunnyFallInHole, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.SuperBunnyCave
    //                 },
    //                 RequirementNodeID.SuperBunnyCaveChests,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LWFloatingIsland_To_DWFloatingIsland =>
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
    //                     RequirementNodeID.LWFloatingIsland
    //                 },
    //                 RequirementNodeID.DWFloatingIsland,
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
    //                     RequirementNodeID.LWFloatingIsland
    //                 },
    //                 RequirementNodeID.DWFloatingIsland,
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
    //                     RequirementNodeID.LWFloatingIsland
    //                 },
    //                 RequirementNodeID.DWFloatingIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntrance_To_DWFloatingIsland =>
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
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.DWFloatingIsland,
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
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.DWFloatingIsland,
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
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.DWFloatingIsland,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainTopNotBunny_To_HookshotCaveEntrance =>
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
    //                     RequirementNodeID.DarkDeathMountainTopNotBunny
    //                 },
    //                 RequirementNodeID.HookshotCaveEntrance,
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
    //                     RequirementNodeID.DarkDeathMountainTopNotBunny
    //                 },
    //                 RequirementNodeID.HookshotCaveEntrance,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntrance_To_HookshotCaveEntranceHookshot =>
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
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveEntranceHookshot,
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
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveEntranceHookshot,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntrance_To_HookshotCaveEntranceHover =>
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
    //                     (SequenceBreakType.Hover, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveEntranceHover,
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
    //                     (SequenceBreakType.Hover, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveEntranceHover,
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
    //                     (SequenceBreakType.Hover, true)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveEntranceHover,
    //                 AccessibilityLevel.SequenceBreak
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntranceHookshot_To_HookshotCaveBonkableChest =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.HookshotCaveBonkableChest,
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
    //                     RequirementNodeID.HookshotCaveEntranceHookshot
    //                 },
    //                 RequirementNodeID.HookshotCaveBonkableChest,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntrance_To_HookshotCaveBonkableChest =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     ItemPlacement = ItemPlacement.Advanced
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.BonkOverLedge, true),
    //                     (SequenceBreakType.Hover, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveBonkableChest,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     ItemPlacement = ItemPlacement.Basic
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.BonkOverLedge, true),
    //                     (SequenceBreakType.Hover, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveBonkableChest,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     ItemPlacement = ItemPlacement.Basic
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.BonkOverLedge, false),
    //                     (SequenceBreakType.Hover, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveBonkableChest,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     ItemPlacement = ItemPlacement.Basic
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.BonkOverLedge, true),
    //                     (SequenceBreakType.Hover, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveBonkableChest,
    //                 AccessibilityLevel.SequenceBreak
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData()
    //                 {
    //                     ItemPlacement = ItemPlacement.Advanced
    //                 },
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Boots, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[]
    //                 {
    //                     (SequenceBreakType.BonkOverLedge, true),
    //                     (SequenceBreakType.Hover, false)
    //                 },
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.HookshotCaveEntrance
    //                 },
    //                 RequirementNodeID.HookshotCaveBonkableChest,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntranceHover_To_HookshotCaveBonkableChest =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.HookshotCaveBonkableChest,
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
    //                     RequirementNodeID.HookshotCaveEntranceHover
    //                 },
    //                 RequirementNodeID.HookshotCaveBonkableChest,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntranceHookshot_To_HookshotCaveBack =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.HookshotCaveBack,
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
    //                     RequirementNodeID.HookshotCaveEntranceHookshot
    //                 },
    //                 RequirementNodeID.HookshotCaveBack,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> HookshotCaveEntranceHover_To_HookshotCaveBack =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.HookshotCaveBack,
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
    //                     RequirementNodeID.HookshotCaveEntranceHover
    //                 },
    //                 RequirementNodeID.HookshotCaveBack,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> LWTurtleRockTopStandardOpen_To_DWTurtleRockTop =>
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
    //                     RequirementNodeID.LWTurtleRockTopStandardOpen
    //                 },
    //                 RequirementNodeID.DWTurtleRockTop,
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
    //                     RequirementNodeID.LWTurtleRockTopStandardOpen
    //                 },
    //                 RequirementNodeID.DWTurtleRockTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainTopInverted_To_DWTurtleRockTop =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DWTurtleRockTop,
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
    //                     RequirementNodeID.DarkDeathMountainTopInverted
    //                 },
    //                 RequirementNodeID.DWTurtleRockTop,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWTurtleRockTop_To_DWTurtleRockTopInverted =>
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
    //                     RequirementNodeID.DWTurtleRockTop
    //                 },
    //                 RequirementNodeID.DWTurtleRockTopInverted,
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
    //                     RequirementNodeID.DWTurtleRockTop
    //                 },
    //                 RequirementNodeID.DWTurtleRockTopInverted,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWTurtleRockTop_To_DWTurtleRockTopNotBunny =>
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
    //                     RequirementNodeID.DWTurtleRockTop
    //                 },
    //                 RequirementNodeID.DWTurtleRockTopNotBunny,
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
    //                 RequirementNodeID.DWTurtleRockTopNotBunny,
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
    //                     RequirementNodeID.DWTurtleRockTop
    //                 },
    //                 RequirementNodeID.DWTurtleRockTopNotBunny,
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
    //                     RequirementNodeID.DWTurtleRockTop
    //                 },
    //                 RequirementNodeID.DWTurtleRockTopNotBunny,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DWTurtleRockTopNotBunny_To_TurtleRockFrontEntrance =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 1),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 1),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 3),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 3),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 3)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 1),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 1),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 1),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 1),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 1)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.None
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 2),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 3),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 2),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 3),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 2)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 3)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 0),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 2),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 3),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 2),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 3),
    //                     (ItemType.Quake, 0),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 2)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 0),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 0),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 3)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             },
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[]
    //                 {
    //                     (ItemType.Sword, 2),
    //                     (ItemType.Bombos, 1),
    //                     (ItemType.BombosDungeons, 0),
    //                     (ItemType.Ether, 1),
    //                     (ItemType.EtherDungeons, 0),
    //                     (ItemType.Quake, 1),
    //                     (ItemType.QuakeDungeons, 0)
    //                 },
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[]
    //                 {
    //                     RequirementNodeID.DWTurtleRockTopNotBunny
    //                 },
    //                 RequirementNodeID.TurtleRockFrontEntrance,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastBottom_To_DarkDeathMountainEastBottom =>
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
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottom,
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
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottom,
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
    //                     RequirementNodeID.DeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastBottomLift2_To_DarkDeathMountainEastBottom =>
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
    //                     RequirementNodeID.DeathMountainEastBottomLift2
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottom,
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
    //                     RequirementNodeID.DeathMountainEastBottomLift2
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainTop_To_DarkDeathMountainEastBottom =>
    //         new List<object[]>
    //         {
    //             new object[]
    //             {
    //                 new ModeSaveData(),
    //                 new (ItemType, int)[0],
    //                 new (PrizeType, int)[0],
    //                 new (SequenceBreakType, bool)[0],
    //                 new RequirementNodeID[0],
    //                 RequirementNodeID.DarkDeathMountainEastBottom,
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
    //                     RequirementNodeID.DarkDeathMountainTop
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottom,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainEastBottom_To_DarkDeathMountainEastBottomInverted =>
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottomInverted,
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottomInverted,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DarkDeathMountainEastBottom_To_DarkDeathMountainEastBottomConnector =>
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottomConnector,
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
    //                 RequirementNodeID.DarkDeathMountainEastBottomConnector,
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottomConnector,
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
    //                     RequirementNodeID.DarkDeathMountainEastBottom
    //                 },
    //                 RequirementNodeID.DarkDeathMountainEastBottomConnector,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> SpiralCaveLedge_To_TurtleRockTunnel =>
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
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.TurtleRockTunnel,
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
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.TurtleRockTunnel,
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
    //                     RequirementNodeID.SpiralCaveLedge
    //                 },
    //                 RequirementNodeID.TurtleRockTunnel,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> MimicCaveLedge_To_TurtleRockTunnel =>
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
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.TurtleRockTunnel,
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
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.TurtleRockTunnel,
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
    //                     RequirementNodeID.MimicCaveLedge
    //                 },
    //                 RequirementNodeID.TurtleRockTunnel,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> TurtleRockTunnel_To_TurtleRockTunnelMirror =>
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
    //                     RequirementNodeID.TurtleRockTunnel
    //                 },
    //                 RequirementNodeID.TurtleRockTunnelMirror,
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
    //                     RequirementNodeID.TurtleRockTunnel
    //                 },
    //                 RequirementNodeID.TurtleRockTunnelMirror,
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
    //                     RequirementNodeID.TurtleRockTunnel
    //                 },
    //                 RequirementNodeID.TurtleRockTunnelMirror,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> TRKeyDoorsToMiddleExit_To_TurtleRockTunnelMirror =>
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
    //                     RequirementNodeID.TRKeyDoorsToMiddleExit
    //                 },
    //                 RequirementNodeID.TurtleRockTunnelMirror,
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
    //                     RequirementNodeID.TRKeyDoorsToMiddleExit
    //                 },
    //                 RequirementNodeID.TurtleRockTunnelMirror,
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
    //                     RequirementNodeID.TRKeyDoorsToMiddleExit
    //                 },
    //                 RequirementNodeID.TurtleRockTunnelMirror,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    //
    //     public static IEnumerable<object[]> DeathMountainEastTopConnector_To_TurtleRockSafetyDoor =>
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
    //                     RequirementNodeID.DeathMountainEastTopConnector
    //                 },
    //                 RequirementNodeID.TurtleRockSafetyDoor,
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
    //                     RequirementNodeID.DeathMountainEastTopConnector
    //                 },
    //                 RequirementNodeID.TurtleRockSafetyDoor,
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
    //                     RequirementNodeID.DeathMountainEastTopConnector
    //                 },
    //                 RequirementNodeID.TurtleRockSafetyDoor,
    //                 AccessibilityLevel.Normal
    //             }
    //         };
    // }
}
