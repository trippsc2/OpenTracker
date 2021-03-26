using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.DungeonNodes
{
    public class SWTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(SWFrontEntryToSWBigChestAreaBottom))]
        [MemberData(nameof(SWBigChestAreaTopToSWBigChestAreaBottom))]
        [MemberData(nameof(SWFrontLeftSideToSWBigChestAreaBottom))]
        [MemberData(nameof(SWFrontRightSideToSWBigChestAreaBottom))]
        [MemberData(nameof(SWFrontEntryToSWBigChestAreaTop))]
        [MemberData(nameof(SWBigChestAreaBottomToSWBigChestAreaTop))]
        [MemberData(nameof(SWBigChestAreaTopToSWBigChest))]
        [MemberData(nameof(SWFrontLeftSideToSWFrontLeftKeyDoor))]
        [MemberData(nameof(SWBigChestAreaBottomToSWFrontLeftKeyDoor))]
        [MemberData(nameof(SWFrontEntryToSWFrontLeftSide))]
        [MemberData(nameof(SWBigChestAreaBottomToSWFrontLeftSide))]
        [MemberData(nameof(SWFrontRightSideToSWFrontRightKeyDoor))]
        [MemberData(nameof(SWBigChestAreaBottomToSWFrontRightKeyDoor))]
        [MemberData(nameof(SWFrontEntryToSWFrontRightSide))]
        [MemberData(nameof(SWFrontLeftSideToSWFrontRightSide))]
        [MemberData(nameof(SWBigChestAreaBottomToSWFrontRightSide))]
        [MemberData(nameof(SWFrontEntryToSWFrontBackConnector))]
        [MemberData(nameof(SWPastTheWorthlessKeyDoorToSWFrontBackConnector))]
        [MemberData(nameof(SWFrontBackConnectorToSWWorthlessKeyDoor))]
        [MemberData(nameof(SWPastTheWorthlessKeyDoorToSWWorthlessKeyDoor))]
        [MemberData(nameof(SWFrontBackConnectorToSWPastTheWorthlessKeyDoor))]
        [MemberData(nameof(SWBackEntryToSWBack))]
        [MemberData(nameof(SWBackFirstKeyDoorToSWBack))]
        [MemberData(nameof(SWBackToSWBackFirstKeyDoor))]
        [MemberData(nameof(SWBackPastFirstKeyDoorToSWBackFirstKeyDoor))]
        [MemberData(nameof(SWBackToSWBackPastFirstKeyDoor))]
        [MemberData(nameof(SWBackPastFirstKeyDoorToSWBackPastFourTorchRoom))]
        [MemberData(nameof(SWBackPastFourTorchRoomToSWBackPastCurtains))]
        [MemberData(nameof(SWBossRoomToSWBackPastCurtains))]
        [MemberData(nameof(SWBackPastCurtainsToSWBackSecondKeyDoor))]
        [MemberData(nameof(SWBossRoomToSWBackSecondKeyDoor))]
        [MemberData(nameof(SWBackPastCurtainsToSWBossRoom))]
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
        
        public static IEnumerable<object[]> SWFrontEntryToSWBigChestAreaBottom =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBigChestAreaTopToSWBigChestAreaBottom =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontLeftSideToSWBigChestAreaBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new[]
                    {
                        KeyDoorID.SWFrontLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontRightSideToSWBigChestAreaBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontRightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontRightSide
                    },
                    new[]
                    {
                        KeyDoorID.SWFrontRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontEntryToSWBigChestAreaTop =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBigChestAreaBottomToSWBigChestAreaTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpSWBigChest, false)
                    },
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpSWBigChest, true)
                    },
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpSWBigChest, true)
                    },
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBigChestAreaTopToSWBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaTop
                    },
                    new[]
                    {
                        KeyDoorID.SWBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontLeftSideToSWFrontLeftKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBigChestAreaBottomToSWFrontLeftKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontEntryToSWFrontLeftSide =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBigChestAreaBottomToSWFrontLeftSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new[]
                    {
                        KeyDoorID.SWFrontLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontRightSideToSWFrontRightKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontRightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBigChestAreaBottomToSWFrontRightKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontEntryToSWFrontRightSide =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontLeftSideToSWFrontRightSide =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBigChestAreaBottomToSWFrontRightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new[]
                    {
                        KeyDoorID.SWFrontRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontEntryToSWFrontBackConnector =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWPastTheWorthlessKeyDoorToSWFrontBackConnector =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWPastTheWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWPastTheWorthlessKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.SWWorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontBackConnectorToSWWorthlessKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontBackConnector
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWPastTheWorthlessKeyDoorToSWWorthlessKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWPastTheWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWFrontBackConnectorToSWPastTheWorthlessKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontBackConnector
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWPastTheWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWFrontBackConnector
                    },
                    new[]
                    {
                        KeyDoorID.SWWorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWPastTheWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackEntryToSWBack =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.SWBackEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackFirstKeyDoorToSWBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.SWBackFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackToSWBackFirstKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackPastFirstKeyDoorToSWBackFirstKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackToSWBackPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBack
                    },
                    new[]
                    {
                        KeyDoorID.SWBackFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackPastFirstKeyDoorToSWBackPastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackPastFourTorchRoomToSWBackPastCurtains =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBossRoomToSWBackPastCurtains =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBossRoom
                    },
                    new[]
                    {
                        KeyDoorID.SWBackSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackPastCurtainsToSWBackSecondKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastCurtains
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBossRoomToSWBackSecondKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> SWBackPastCurtainsToSWBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastCurtains
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.SWBackPastCurtains
                    },
                    new[]
                    {
                        KeyDoorID.SWBackSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
