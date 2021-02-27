using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.DungeonNodes
{
    [Collection("Tests")]
    public class DPTests
    {
        // [Theory]
        // [MemberData(nameof(DPFrontEntry_To_DPFront))]
        // [MemberData(nameof(DPLeftEntry_To_DPFront))]
        // [MemberData(nameof(DPPastRightKeyDoor_To_DPFront))]
        // [MemberData(nameof(DPFront_To_DPTorch))]
        // [MemberData(nameof(DPFront_To_DPBigChest))]
        // [MemberData(nameof(DPFront_To_DPRightKeyDoor))]
        // [MemberData(nameof(DPPastRightKeyDoor_To_DPRightKeyDoor))]
        // [MemberData(nameof(DPFront_To_DPPastRightKeyDoor))]
        // [MemberData(nameof(DPBackEntry_To_DPBack))]
        // [MemberData(nameof(DPBack_To_DP2F))]
        // [MemberData(nameof(DP2FPastFirstKeyDoor_To_DP2F))]
        // [MemberData(nameof(DP2F_To_DP2FFirstKeyDoor))]
        // [MemberData(nameof(DP2FPastFirstKeyDoor_To_DP2FFirstKeyDoor))]
        // [MemberData(nameof(DP2F_To_DP2FPastFirstKeyDoor))]
        // [MemberData(nameof(DP2FPastSecondKeyDoor_To_DP2FPastFirstKeyDoor))]
        // [MemberData(nameof(DP2F_To_DP2FSecondKeyDoor))]
        // [MemberData(nameof(DP2FPastFirstKeyDoor_To_DP2FSecondKeyDoor))]
        // [MemberData(nameof(DP2FPastFirstKeyDoor_To_DP2FPastSecondKeyDoor))]
        // [MemberData(nameof(DP2FPastSecondKeyDoor_To_DPPastFourTorchWall))]
        // [MemberData(nameof(DPPastFourTorchWall_To_DPBossRoom))]
        // public void Tests(
        //     ModeSaveData mode, RequirementNodeID[] accessibleEntryNodes,
        //     DungeonNodeID[] accessibleNodes, KeyDoorID[] unlockedDoors, (ItemType, int)[] items,
        //     (SequenceBreakType, bool)[] sequenceBreaks, LocationID dungeonID, DungeonNodeID id,
        //     AccessibilityLevel expected)
        // {
        //     RequirementNodeDictionary.Instance.Reset();
        //     var dungeon = (IDungeon)LocationDictionary.Instance[dungeonID];
        //     var dungeonData = MutableDungeonFactory.GetMutableDungeon(dungeon);
        //     dungeon.FinishMutableDungeonCreation(dungeonData);
        //     dungeonData.Reset();
        //     ItemDictionary.Instance.Reset();
        //     SequenceBreakDictionary.Instance.Reset();
        //     Mode.Instance.Load(mode);
        //
        //     foreach (var node in accessibleEntryNodes)
        //     {
        //         RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
        //     }
        //
        //     foreach (var node in accessibleNodes)
        //     {
        //         dungeonData.Nodes[node].AlwaysAccessible = true;
        //     }
        //
        //     foreach (var item in items)
        //     {
        //         ItemDictionary.Instance[item.Item1].Current = item.Item2;
        //     }
        //
        //     foreach (var sequenceBreak in sequenceBreaks)
        //     {
        //         SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
        //             sequenceBreak.Item2;
        //     }
        //
        //     foreach (var door in unlockedDoors)
        //     {
        //         dungeonData.KeyDoors[door].Unlocked = true;
        //     }
        //
        //     Assert.Equal(expected, dungeonData.Nodes[id].Accessibility);
        // }
        //
        // public static IEnumerable<object[]> DPFrontEntry_To_DPFront =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPFront,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[]
        //             {
        //                 RequirementNodeID.DPFrontEntry
        //             },
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPFront,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPLeftEntry_To_DPFront =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPFront,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[]
        //             {
        //                 RequirementNodeID.DPLeftEntry
        //             },
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPFront,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPPastRightKeyDoor_To_DPFront =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPPastRightKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPFront,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPPastRightKeyDoor
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DPRightKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPFront,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPFront_To_DPTorch =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Boots, 1)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPTorch,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPFront
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Boots, 0)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPTorch,
        //             AccessibilityLevel.Inspect
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPFront
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Boots, 1)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPTorch,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPFront_To_DPBigChest =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPFront
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPBigChest,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPFront
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DPBigChest
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPBigChest,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPFront_To_DPRightKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPRightKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPFront
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPRightKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPPastRightKeyDoor_To_DPRightKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPRightKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPPastRightKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPRightKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPFront_To_DPPastRightKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPFront
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPPastRightKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPFront
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DPRightKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPPastRightKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPBackEntry_To_DPBack =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPBack,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[]
        //             {
        //                 RequirementNodeID.DPBackEntry
        //             },
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPBack,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPBack_To_DP2F =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPBack
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2F,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPBack
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DP1FKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2F,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2FPastFirstKeyDoor_To_DP2F =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastFirstKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2F,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastFirstKeyDoor
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DP2FFirstKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2F,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2F_To_DP2FFirstKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FFirstKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2F
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FFirstKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2FPastFirstKeyDoor_To_DP2FFirstKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FFirstKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastFirstKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FFirstKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2F_To_DP2FPastFirstKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2F
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FPastFirstKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2F
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DP2FFirstKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FPastFirstKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2FPastSecondKeyDoor_To_DP2FPastFirstKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastSecondKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FPastFirstKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastSecondKeyDoor
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DP2FSecondKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FPastFirstKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2F_To_DP2FSecondKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FSecondKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastFirstKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FSecondKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2FPastFirstKeyDoor_To_DP2FSecondKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FSecondKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastSecondKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FSecondKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2FPastFirstKeyDoor_To_DP2FPastSecondKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastFirstKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FPastSecondKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastFirstKeyDoor
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DP2FSecondKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DP2FPastSecondKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DP2FPastSecondKeyDoor_To_DPPastFourTorchWall =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastSecondKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPPastFourTorchWall,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastSecondKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 1),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPPastFourTorchWall,
        //             AccessibilityLevel.Normal
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DP2FPastSecondKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0),
        //                 (ItemType.FireRod, 1)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPPastFourTorchWall,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> DPPastFourTorchWall_To_DPBossRoom =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPPastFourTorchWall
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPBossRoom,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.DPPastFourTorchWall
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.DPBigKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.DesertPalace,
        //             DungeonNodeID.DPBossRoom,
        //             AccessibilityLevel.Normal
        //         }
        //     };
    }
}
