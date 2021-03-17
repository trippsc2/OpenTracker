using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.DungeonNodes
{
    public class EPTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(EPEntryToEP))]
        [MemberData(nameof(EPPastBigKeyDoorToEP))]
        [MemberData(nameof(EPToEPBigChest))]
        [MemberData(nameof(EPToEPRightDarkRoom))]
        [MemberData(nameof(EPPastRightKeyDoorToEPRightDarkRoom))]
        [MemberData(nameof(EPRightDarkRoomToEPRightKeyDoor))]
        [MemberData(nameof(EPPastRightKeyDoorToEPRightKeyDoor))]
        [MemberData(nameof(EPRightDarkRoomToEPPastRightKeyDoor))]
        [MemberData(nameof(EPToEPBigKeyDoor))]
        [MemberData(nameof(EPPastBigKeyDoorToEPBigKeyDoor))]
        [MemberData(nameof(EPToEPPastBigKeyDoor))]
        [MemberData(nameof(EPPastBigKeyDoorToEPBackDarkRoom))]
        [MemberData(nameof(EPBackDarkRoomToEPPastBigKeyDoor))]
        [MemberData(nameof(EPPastBackKeyDoorToEPBossRoom))]
        public override void Tests(
            ModeSaveData modeData, RequirementNodeID[] accessibleEntryNodes,
            DungeonNodeID[] accessibleNodes, KeyDoorID[] unlockedDoors, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, LocationID dungeonID, DungeonNodeID id,
            AccessibilityLevel expected)
        {
            base.Tests(
                modeData, accessibleEntryNodes, accessibleNodes, unlockedDoors, items,
                sequenceBreaks, dungeonID, id, expected);
        }
        
        public static IEnumerable<object[]> EPEntryToEP =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EP,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EP,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        RequirementNodeID.EPEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EP,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPPastBigKeyDoorToEP =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EP,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EP,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPToEPBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new[]
                    {
                        KeyDoorID.EPBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPToEPRightDarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightDarkRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightDarkRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPPastRightKeyDoorToEPRightDarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastRightKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.EPRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightDarkRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPRightDarkRoomToEPRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPRightDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPPastRightKeyDoorToEPRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPRightDarkRoomToEPPastRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPRightDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPPastRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPRightDarkRoom
                    },
                    new[]
                    {
                        KeyDoorID.EPRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPPastRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPToEPBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPPastBigKeyDoorToEPBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPToEPPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPPastBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EP
                    },
                    new[]
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPPastBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPPastBigKeyDoorToEPBackDarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBackDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBackDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBackDarkRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBackDarkRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBackDarkRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBackDarkRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true)
                    },
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBackDarkRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPBackDarkRoomToEPPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPBackDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPPastBackKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPBackDarkRoom
                    },
                    new[]
                    {
                        KeyDoorID.EPBackKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPPastBackKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> EPPastBackKeyDoorToEPBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBackKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = true
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBackKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBossRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.EPPastBackKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.EasternPalace,
                    DungeonNodeID.EPBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
