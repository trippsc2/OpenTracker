using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
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
        [MemberData(nameof(TRFrontEntryToTRFront))]
        [MemberData(nameof(TRF1SomariaTrackToTRFront))]
        [MemberData(nameof(TRFrontToTRF1SomariaTrack))]
        [MemberData(nameof(TRF1CompassChestAreaToTRF1SomariaTrack))]
        [MemberData(nameof(TRF1FourTorchRoomToTRF1SomariaTrack))]
        [MemberData(nameof(TRF1FirstKeyDoorAreaToTRF1SomariaTrack))]
        [MemberData(nameof(TRF1SomariaTrackToTRF1CompassChestArea))]
        [MemberData(nameof(TRF1SomariaTrackToTRF1FourTorchRoom))]
        [MemberData(nameof(TRF1FourTorchRoomToTRF1RollerRoom))]
        [MemberData(nameof(TRF1SomariaTrackToTRF1FirstKeyDoorArea))]
        [MemberData(nameof(TRF1PastFirstKeyDoorToTRF1FirstKeyDoorArea))]
        [MemberData(nameof(TRF1FirstKeyDoorAreaToTRF1FirstKeyDoor))]
        [MemberData(nameof(TRF1PastFirstKeyDoorToTRF1FirstKeyDoor))]
        [MemberData(nameof(TRF1FirstKeyDoorAreaToTRF1PastFirstKeyDoor))]
        [MemberData(nameof(TRF1PastSecondKeyDoorToTRF1PastFirstKeyDoor))]
        [MemberData(nameof(TRF1PastFirstKeyDoorToTRF1SecondKeyDoor))]
        [MemberData(nameof(TRF1PastSecondKeyDoorToTRF1SecondKeyDoor))]
        [MemberData(nameof(TRF1PastFirstKeyDoorToTRF1PastSecondKeyDoor))]
        [MemberData(nameof(TRB1ToTRF1PastSecondKeyDoor))]
        [MemberData(nameof(TRMiddleEntryToTRB1))]
        [MemberData(nameof(TRF1PastSecondKeyDoorToTRB1))]
        [MemberData(nameof(TRB1BigChestAreaToTRB1))]
        [MemberData(nameof(TRB1RightSideToTRB1))]
        [MemberData(nameof(TRB1PastBigKeyChestKeyDoorToTRB1))]
        [MemberData(nameof(TRB1ToTRB1BigKeyChestKeyDoor))]
        [MemberData(nameof(TRB1PastBigKeyChestKeyDoorToTRB1BigKeyChestKeyDoor))]
        [MemberData(nameof(TRB1ToTRB1PastBigKeyChestKeyDoor))]
        [MemberData(nameof(TRMiddleEntryToTRB1MiddleRightEntranceArea))]
        [MemberData(nameof(TRB1ToTRB1MiddleRightEntranceArea))]
        [MemberData(nameof(TRB1MiddleRightEntranceAreaToTRB1BigChestArea))]
        [MemberData(nameof(TRB1BigChestAreaToTRBigChest))]
        [MemberData(nameof(TRB1ToTRB1BigKeyDoor))]
        [MemberData(nameof(TRB1RightSideToTRB1BigKeyDoor))]
        [MemberData(nameof(TRB1ToTRB1RightSide))]
        [MemberData(nameof(TRPastB1ToB2KeyDoorToTRB1RightSide))]
        [MemberData(nameof(TRB1RightSideToTRPastB1ToB2KeyDoor))]
        [MemberData(nameof(TRB2DarkRoomTopToTRPastB1ToB2KeyDoor))]
        [MemberData(nameof(TRPastB1ToB2KeyDoorToTRB2DarkRoomTop))]
        [MemberData(nameof(TRB2DarkRoomBottomToTRB2DarkRoomTop))]
        [MemberData(nameof(TRB2DarkRoomTopToTRB2DarkRoomBottom))]
        [MemberData(nameof(TRB2PastDarkMazeToTRB2DarkRoomBottom))]
        [MemberData(nameof(TRBackEntryToTRB2PastDarkMaze))]
        [MemberData(nameof(TRB2DarkRoomBottomToTRB2PastDarkMaze))]
        [MemberData(nameof(TRB2PastKeyDoorToTRB2PastDarkMaze))]
        [MemberData(nameof(TRB2PastDarkMazeToTRLaserBridgeChests))]
        [MemberData(nameof(TRB2PastDarkMazeToTRB2KeyDoor))]
        [MemberData(nameof(TRB2PastKeyDoorToTRB2KeyDoor))]
        [MemberData(nameof(TRB2PastDarkMazeToTRB2PastKeyDoor))]
        [MemberData(nameof(TRB2PastKeyDoorToTRB3))]
        [MemberData(nameof(TRB3ToTRB3BossRoomEntry))]
        [MemberData(nameof(TRB3BossRoomEntryToTRBossRoom))]
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
        
        public static IEnumerable<object[]> TRFrontEntryToTRFront =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1SomariaTrackToTRFront =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRFrontToTRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1CompassChestAreaToTRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1FourTorchRoomToTRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1FirstKeyDoorAreaToTRF1SomariaTrack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1SomariaTrackToTRF1CompassChestArea =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1SomariaTrackToTRF1FourTorchRoom =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1FourTorchRoomToTRF1RollerRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1SomariaTrackToTRF1FirstKeyDoorArea =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoorToTRF1FirstKeyDoorArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRF1FirstKeyDoorAreaToTRF1FirstKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoorToTRF1FirstKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1FirstKeyDoorAreaToTRF1PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRF1FirstKeyDoorArea
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRF1PastSecondKeyDoorToTRF1PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRF1PastSecondKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoorToTRF1SecondKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1PastSecondKeyDoorToTRF1SecondKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1PastFirstKeyDoorToTRF1PastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRF1PastFirstKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRB1ToTRF1PastSecondKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRMiddleEntryToTRB1 =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRF1PastSecondKeyDoorToTRB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRF1PastSecondKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRB1BigChestAreaToTRB1 =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB1RightSideToTRB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRB1PastBigKeyChestKeyDoorToTRB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1PastBigKeyChestKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRB1ToTRB1BigKeyChestKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB1PastBigKeyChestKeyDoorToTRB1BigKeyChestKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB1ToTRB1PastBigKeyChestKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRMiddleEntryToTRB1MiddleRightEntranceArea =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB1ToTRB1MiddleRightEntranceArea =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB1MiddleRightEntranceAreaToTRB1BigChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1MiddleRightEntranceArea
                    },
                    new KeyDoorID[0],
                    new[]
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
        
        public static IEnumerable<object[]> TRB1BigChestAreaToTRBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1BigChestArea
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRB1ToTRB1BigKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB1RightSideToTRB1BigKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB1ToTRB1RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB1
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRPastB1ToB2KeyDoorToTRB1RightSide =>
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
                    new[]
                    {
                        DungeonNodeID.TRPastB1ToB2KeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRB1RightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB1RightSideToTRPastB1ToB2KeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRPastB1ToB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.TRB1RightSide
                    },
                    new[]
                    {
                        KeyDoorID.TRB1ToB2KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRPastB1ToB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRB2DarkRoomTopToTRPastB1ToB2KeyDoor =>
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
                    DungeonNodeID.TRPastB1ToB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.TRB2DarkRoomTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.TurtleRock,
                    DungeonNodeID.TRPastB1ToB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> TRPastB1ToB2KeyDoorToTRB2DarkRoomTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.TRPastB1ToB2KeyDoor
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
                    new[]
                    {
                        DungeonNodeID.TRPastB1ToB2KeyDoor
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
                    new[]
                    {
                        DungeonNodeID.TRPastB1ToB2KeyDoor
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
        
        public static IEnumerable<object[]> TRB2DarkRoomBottomToTRB2DarkRoomTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB2DarkRoomTopToTRB2DarkRoomBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB2PastDarkMazeToTRB2DarkRoomBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRBackEntryToTRB2PastDarkMaze =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB2DarkRoomBottomToTRB2PastDarkMaze =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB2PastKeyDoorToTRB2PastDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB2PastKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRB2PastDarkMazeToTRLaserBridgeChests =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new KeyDoorID[0],
                    new[]
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
        
        public static IEnumerable<object[]> TRB2PastDarkMazeToTRB2KeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB2PastKeyDoorToTRB2KeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB2PastDarkMazeToTRB2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB2PastDarkMaze
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TRB2PastKeyDoorToTRB3 =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB3ToTRB3BossRoomEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TRB3BossRoomEntryToTRBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TRB3BossRoomEntry
                    },
                    new[]
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
