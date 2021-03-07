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
    public class TRTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(TRFrontEntry_To_TRFront))]
        [MemberData(nameof(TRF1SomariaTrack_To_TRFront))]
        [MemberData(nameof(TRFront_To_TRF1SomariaTrack))]
        [MemberData(nameof(TRF1CompassChestArea_To_TRF1SomariaTrack))]
        [MemberData(nameof(TRF1FourTorchRoom_To_TRF1SomariaTrack))]
        [MemberData(nameof(TRF1FirstKeyDoorArea_To_TRF1SomariaTrack))]
        [MemberData(nameof(TRF1SomariaTrack_To_TRF1CompassChestArea))]
        [MemberData(nameof(TRF1SomariaTrack_To_TRF1FourTorchRoom))]
        [MemberData(nameof(TRF1FourTorchRoom_To_TRF1RollerRoom))]
        [MemberData(nameof(TRF1SomariaTrack_To_TRF1FirstKeyDoorArea))]
        [MemberData(nameof(TRF1PastFirstKeyDoor_To_TRF1FirstKeyDoorArea))]
        [MemberData(nameof(TRF1FirstKeyDoorArea_To_TRF1FirstKeyDoor))]
        [MemberData(nameof(TRF1PastFirstKeyDoor_To_TRF1FirstKeyDoor))]
        [MemberData(nameof(TRF1FirstKeyDoorArea_To_TRF1PastFirstKeyDoor))]
        [MemberData(nameof(TRF1PastSecondKeyDoor_To_TRF1PastFirstKeyDoor))]
        [MemberData(nameof(TRF1PastFirstKeyDoor_To_TRF1SecondKeyDoor))]
        [MemberData(nameof(TRF1PastSecondKeyDoor_To_TRF1SecondKeyDoor))]
        [MemberData(nameof(TRF1PastFirstKeyDoor_To_TRF1PastSecondKeyDoor))]
        [MemberData(nameof(TRB1_To_TRF1PastSecondKeyDoor))]
        [MemberData(nameof(TRMiddleEntry_To_TRB1))]
        [MemberData(nameof(TRF1PastSecondKeyDoor_To_TRB1))]
        [MemberData(nameof(TRB1BigChestArea_To_TRB1))]
        [MemberData(nameof(TRB1RightSide_To_TRB1))]
        [MemberData(nameof(TRB1PastBigKeyChestKeyDoor_To_TRB1))]
        [MemberData(nameof(TRB1_To_TRB1BigKeyChestKeyDoor))]
        [MemberData(nameof(TRB1PastBigKeyChestKeyDoor_To_TRB1BigKeyChestKeyDoor))]
        [MemberData(nameof(TRB1_To_TRB1PastBigKeyChestKeyDoor))]
        [MemberData(nameof(TRMiddleEntry_To_TRB1MiddleRightEntranceArea))]
        [MemberData(nameof(TRB1_To_TRB1MiddleRightEntranceArea))]
        [MemberData(nameof(TRB1MiddleRightEntranceArea_To_TRB1BigChestArea))]
        [MemberData(nameof(TRB1BigChestArea_To_TRBigChest))]
        [MemberData(nameof(TRB1_To_TRB1BigKeyDoor))]
        [MemberData(nameof(TRB1RightSide_To_TRB1BigKeyDoor))]
        [MemberData(nameof(TRB1_To_TRB1RightSide))]
        [MemberData(nameof(TRPastB1toB2KeyDoor_To_TRB1RightSide))]
        [MemberData(nameof(TRB1RightSide_To_TRPastB1toB2KeyDoor))]
        [MemberData(nameof(TRB2DarkRoomTop_To_TRPastB1toB2KeyDoor))]
        [MemberData(nameof(TRPastB1toB2KeyDoor_To_TRB2DarkRoomTop))]
        [MemberData(nameof(TRB2DarkRoomBottom_To_TRB2DarkRoomTop))]
        [MemberData(nameof(TRB2DarkRoomTop_To_TRB2DarkRoomBottom))]
        [MemberData(nameof(TRB2PastDarkMaze_To_TRB2DarkRoomBottom))]
        [MemberData(nameof(TRBackEntry_To_TRB2PastDarkMaze))]
        [MemberData(nameof(TRB2DarkRoomBottom_To_TRB2PastDarkMaze))]
        [MemberData(nameof(TRB2PastKeyDoor_To_TRB2PastDarkMaze))]
        [MemberData(nameof(TRB2PastDarkMaze_To_TRLaserBridgeChests))]
        [MemberData(nameof(TRB2PastDarkMaze_To_TRB2KeyDoor))]
        [MemberData(nameof(TRB2PastKeyDoor_To_TRB2KeyDoor))]
        [MemberData(nameof(TRB2PastDarkMaze_To_TRB2PastKeyDoor))]
        [MemberData(nameof(TRB2PastKeyDoor_To_TRB3))]
        [MemberData(nameof(TRB3_To_TRB3BossRoomEntry))]
        [MemberData(nameof(TRB3BossRoomEntry_To_TRBossRoom))]
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
        
        public static IEnumerable<object[]> TRFrontEntry_To_TRFront =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1SomariaTrack_To_TRFront =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1SomariaTrack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRFront_To_TRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1CompassChestArea_To_TRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1CompassChestArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1CompassChestArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1FourTorchRoom_To_TRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1FirstKeyDoorArea_To_TRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FirstKeyDoorArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FirstKeyDoorArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SomariaTrack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1SomariaTrack_To_TRF1CompassChestArea =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1CompassChestArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1SomariaTrack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1CompassChestArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1SomariaTrack_To_TRF1FourTorchRoom =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1SomariaTrack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1FourTorchRoom_To_TRF1RollerRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1RollerRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1RollerRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1SomariaTrack_To_TRF1FirstKeyDoorArea =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1SomariaTrack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoor_To_TRF1FirstKeyDoorArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1FirstKeyDoorArea_To_TRF1FirstKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FirstKeyDoorArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoor_To_TRF1FirstKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1FirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1FirstKeyDoorArea_To_TRF1PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FirstKeyDoorArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1FirstKeyDoorArea
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1PastSecondKeyDoor_To_TRF1PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastSecondKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoor_To_TRF1SecondKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1PastSecondKeyDoor_To_TRF1SecondKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1SecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoor_To_TRF1PastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1_To_TRF1PastSecondKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRMiddleEntry_To_TRB1 =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRMiddleEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRF1PastSecondKeyDoor_To_TRB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRF1PastSecondKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FThirdKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1BigChestArea_To_TRB1 =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1BigChestArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1RightSide_To_TRB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1BigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1PastBigKeyChestKeyDoor_To_TRB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1PastBigKeyChestKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1PastBigKeyChestKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1BigKeyChestKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1_To_TRB1BigKeyChestKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyChestKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyChestKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1PastBigKeyChestKeyDoor_To_TRB1BigKeyChestKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyChestKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1PastBigKeyChestKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyChestKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1_To_TRB1PastBigKeyChestKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1PastBigKeyChestKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1BigKeyChestKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1PastBigKeyChestKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRMiddleEntry_To_TRB1MiddleRightEntranceArea =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRMiddleEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1_To_TRB1MiddleRightEntranceArea =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1MiddleRightEntranceArea_To_TRB1BigChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigChestArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigChestArea,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigChestArea,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigChestArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1BigChestArea_To_TRBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1BigChestArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1BigChestArea
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1_To_TRB1BigKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1RightSide_To_TRB1BigKeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1BigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1_To_TRB1RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1RightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1BigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1RightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRPastB1toB2KeyDoor_To_TRB1RightSide =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1RightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRPastB1toB2KeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1RightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1RightSide_To_TRPastB1toB2KeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1toB2KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2DarkRoomTop_To_TRPastB1toB2KeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2DarkRoomTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRPastB1toB2KeyDoor_To_TRB2DarkRoomTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRPastB1toB2KeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, false)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomTop,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRPastB1toB2KeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomTop,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRPastB1toB2KeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomTop,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2DarkRoomBottom_To_TRB2DarkRoomTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2DarkRoomBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomTop,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2DarkRoomBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomTop,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2DarkRoomTop_To_TRB2DarkRoomBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2DarkRoomTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2DarkRoomTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomBottom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2PastDarkMaze_To_TRB2DarkRoomBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, false)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomBottom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2DarkRoomBottom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRBackEntry_To_TRB2PastDarkMaze =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastDarkMaze,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRBackEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastDarkMaze,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2DarkRoomBottom_To_TRB2PastDarkMaze =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastDarkMaze,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2DarkRoomBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastDarkMaze,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2PastKeyDoor_To_TRB2PastDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastDarkMaze,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB2KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastDarkMaze,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2PastDarkMaze_To_TRLaserBridgeChests =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Shield, 2),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, false)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRLaserBridgeChests,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Shield, 2),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRLaserBridgeChests,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Shield, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRLaserBridgeChests,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Shield, 3),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRLaserBridgeChests,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Shield, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRLaserBridgeChests,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Shield, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    LocationID.TurtleRock,
                    DungeonNodeID.TRLaserBridgeChests,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2PastDarkMaze_To_TRB2KeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2PastKeyDoor_To_TRB2KeyDoor =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2PastDarkMaze_To_TRB2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB2KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB2PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2PastKeyDoor_To_TRB3 =>
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
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB3,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB3,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB3_To_TRB3BossRoomEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB3
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB3BossRoomEntry,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB3
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB3BossRoomEntry,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB3BossRoomEntry_To_TRBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB3BossRoomEntry
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TRB3BossRoomEntry
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRBossRoomBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
