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
    public class EPTests
    {
        // [Theory]
        // [MemberData(nameof(EPEntry_To_EP))]
        // [MemberData(nameof(EPPastBigKeyDoor_To_EP))]
        // [MemberData(nameof(EP_To_EPBigChest))]
        // [MemberData(nameof(EP_To_EPRightDarkRoom))]
        // [MemberData(nameof(EPPastRightKeyDoor_To_EPRightDarkRoom))]
        // [MemberData(nameof(EPRightDarkRoom_To_EPRightKeyDoor))]
        // [MemberData(nameof(EPPastRightKeyDoor_To_EPRightKeyDoor))]
        // [MemberData(nameof(EPRightDarkRoom_To_EPPastRightKeyDoor))]
        // [MemberData(nameof(EP_To_EPBigKeyDoor))]
        // [MemberData(nameof(EPPastBigKeyDoor_To_EPBigKeyDoor))]
        // [MemberData(nameof(EP_To_EPPastBigKeyDoor))]
        // [MemberData(nameof(EPPastBigKeyDoor_To_EPBackDarkRoom))]
        // [MemberData(nameof(EPBackDarkRoom_To_EPPastBigKeyDoor))]
        // [MemberData(nameof(EPPastBackKeyDoor_To_EPBossRoom))]
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
        // public static IEnumerable<object[]> EPEntry_To_EP =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EP,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.StandardOpen
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EP,
        //             AccessibilityLevel.Normal
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[]
        //             {
        //                 RequirementNodeID.EPEntry
        //             },
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EP,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPPastBigKeyDoor_To_EP =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EP,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.EPBigKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EP,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EP_To_EPBigChest =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBigChest,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.EPBigChest
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBigChest,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EP_To_EPRightDarkRoom =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightDarkRoom,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, true)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightDarkRoom,
        //             AccessibilityLevel.SequenceBreak
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 1)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, true)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightDarkRoom,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPPastRightKeyDoor_To_EPRightDarkRoom =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastRightKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightDarkRoom,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastRightKeyDoor
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.EPRightKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightDarkRoom,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPRightDarkRoom_To_EPRightKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPRightDarkRoom
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPPastRightKeyDoor_To_EPRightKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastRightKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPRightKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPRightDarkRoom_To_EPPastRightKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPRightDarkRoom
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPPastRightKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPRightDarkRoom
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.EPRightKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPPastRightKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EP_To_EPBigKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBigKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBigKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPPastBigKeyDoor_To_EPBigKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[0],
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBigKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 WorldState = WorldState.Inverted
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBigKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EP_To_EPPastBigKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPPastBigKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EP
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.EPBigKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPPastBigKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPPastBigKeyDoor_To_EPBackDarkRoom =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 ItemPlacement = ItemPlacement.Advanced
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPBack, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBackDarkRoom,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 ItemPlacement = ItemPlacement.Basic
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPBack, false)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBackDarkRoom,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 ItemPlacement = ItemPlacement.Advanced
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, true)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBackDarkRoom,
        //             AccessibilityLevel.SequenceBreak
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 ItemPlacement = ItemPlacement.Basic
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, true)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBackDarkRoom,
        //             AccessibilityLevel.SequenceBreak
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 ItemPlacement = ItemPlacement.Advanced
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 1),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, true)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBackDarkRoom,
        //             AccessibilityLevel.Normal
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 ItemPlacement = ItemPlacement.Advanced
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 0),
        //                 (ItemType.FireRod, 1)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, true)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBackDarkRoom,
        //             AccessibilityLevel.Normal
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 ItemPlacement = ItemPlacement.Basic
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBigKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Lamp, 1),
        //                 (ItemType.FireRod, 0)
        //             },
        //             new (SequenceBreakType, bool)[]
        //             {
        //                 (SequenceBreakType.DarkRoomEPRight, true)
        //             },
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBackDarkRoom,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPBackDarkRoom_To_EPPastBigKeyDoor =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPBackDarkRoom
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPPastBackKeyDoor,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData(),
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPBackDarkRoom
        //             },
        //             new KeyDoorID[]
        //             {
        //                 KeyDoorID.EPBackKeyDoor
        //             },
        //             new (ItemType, int)[0],
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPPastBackKeyDoor,
        //             AccessibilityLevel.Normal
        //         }
        //     };
        //
        // public static IEnumerable<object[]> EPPastBackKeyDoor_To_EPBossRoom =>
        //     new List<object[]>
        //     {
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 EnemyShuffle = false
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBackKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Bow, 0)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBossRoom,
        //             AccessibilityLevel.None
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 EnemyShuffle = true
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBackKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Bow, 0)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBossRoom,
        //             AccessibilityLevel.Normal
        //         },
        //         new object[]
        //         {
        //             new ModeSaveData()
        //             {
        //                 EnemyShuffle = false
        //             },
        //             new RequirementNodeID[0],
        //             new DungeonNodeID[]
        //             {
        //                 DungeonNodeID.EPPastBackKeyDoor
        //             },
        //             new KeyDoorID[0],
        //             new (ItemType, int)[]
        //             {
        //                 (ItemType.Bow, 1)
        //             },
        //             new (SequenceBreakType, bool)[0],
        //             LocationID.EasternPalace,
        //             DungeonNodeID.EPBossRoom,
        //             AccessibilityLevel.Normal
        //         }
        //     };
    }
}
