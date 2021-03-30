using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes
{
    public class HCTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(HCSanctuaryEntryToHCSanctuary))]
        [MemberData(nameof(HCBackToHCSanctuary))]
        [MemberData(nameof(HCFrontEntryToHCFront))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoorToHCFront))]
        [MemberData(nameof(HCDarkRoomFrontToHCFront))]
        [MemberData(nameof(HCFrontToHCEscapeFirstKeyDoor))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoorToHCEscapeFirstKeyDoor))]
        [MemberData(nameof(HCFrontToHCPastEscapeFirstKeyDoor))]
        [MemberData(nameof(HCPastEscapeSecondKeyDoorToHCPastEscapeFirstKeyDoor))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoorToHCEscapeSecondKeyDoor))]
        [MemberData(nameof(HCPastEscapeSecondKeyDoorToHCEscapeSecondKeyDoor))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoorToHCPastEscapeSecondKeyDoor))]
        [MemberData(nameof(HCFrontToHCDarkRoomFront))]
        [MemberData(nameof(HCPastDarkCrossKeyDoorToHCDarkRoomFront))]
        [MemberData(nameof(HCDarkRoomFrontToHCDarkCrossKeyDoor))]
        [MemberData(nameof(HCPastDarkCrossKeyDoorToHCDarkCrossKeyDoor))]
        [MemberData(nameof(HCDarkRoomFrontToHCPastDarkCrossKeyDoor))]
        [MemberData(nameof(HCPastSewerRatRoomKeyDoorToHCPastDarkCrossKeyDoor))]
        [MemberData(nameof(HCPastDarkCrossKeyDoorToHCSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCPastSewerRatRoomKeyDoorToHCSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCDarkRoomBackToHCPastSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCPastDarkCrossKeyDoorToHCPastSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCPastSewerRatRoomKeyDoorToHCDarkRoomBack))]
        [MemberData(nameof(HCBackToHCDarkRoomBack))]
        [MemberData(nameof(HCBackEntryToHCBack))]
        [MemberData(nameof(HCDarkRoomBackToHCBack))]
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
        
        public static IEnumerable<object[]> HCSanctuaryEntryToHCSanctuary =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
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
                        RequirementNodeID.HCSanctuaryEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCBackToHCSanctuary =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
                    AccessibilityLevel.Normal
                },
            };
        
        public static IEnumerable<object[]> HCFrontEntryToHCFront =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
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
                        RequirementNodeID.HCFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.Normal
                }
           };
        
        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoorToHCFront =>
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
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
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
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.HCEscapeFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCDarkRoomFrontToHCFront =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
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
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCFrontToHCEscapeFirstKeyDoor =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                },
            };
        
        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoorToHCEscapeFirstKeyDoor =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
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
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCFrontToHCPastEscapeFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new[]
                    {
                        KeyDoorID.HCEscapeFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastEscapeSecondKeyDoorToHCPastEscapeFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastEscapeSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastEscapeSecondKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.HCEscapeSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoorToHCEscapeSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastEscapeSecondKeyDoorToHCEscapeSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastEscapeSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoorToHCPastEscapeSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.HCEscapeSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCFrontToHCDarkRoomFront =>
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
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
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastDarkCrossKeyDoorToHCDarkRoomFront =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.HCDarkCrossKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCDarkRoomFrontToHCDarkCrossKeyDoor =>
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
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastDarkCrossKeyDoorToHCDarkCrossKeyDoor =>
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
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCDarkRoomFrontToHCPastDarkCrossKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new[]
                    {
                        KeyDoorID.HCDarkCrossKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastSewerRatRoomKeyDoorToHCPastDarkCrossKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastDarkCrossKeyDoorToHCSewerRatRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastSewerRatRoomKeyDoorToHCSewerRatRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCDarkRoomBackToHCPastSewerRatRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCDarkRoomBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastDarkCrossKeyDoorToHCPastSewerRatRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCPastSewerRatRoomKeyDoorToHCDarkRoomBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCBackToHCDarkRoomBack =>
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
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
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCBackEntryToHCBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.HCBackEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCDarkRoomBackToHCBack =>
             new List<object[]>
             {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.HCDarkRoomBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.Normal
                }
             };
    }
}