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
    public class MMTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(MMEntryToMM))]
        [MemberData(nameof(MMToMMPastEntranceGap))]
        [MemberData(nameof(MMB1TopSideToMMPastEntranceGap))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoorToMMPastEntranceGap))]
        [MemberData(nameof(MMPastEntranceGapToMMBigChest))]
        [MemberData(nameof(MMPastEntranceGapToMMB1TopLeftKeyDoor))]
        [MemberData(nameof(MMB1TopSideToMMB1TopLeftKeyDoor))]
        [MemberData(nameof(MMPastEntranceGapToMMB1TopRightKeyDoor))]
        [MemberData(nameof(MMB1TopSideToMMB1TopRightKeyDoor))]
        [MemberData(nameof(MMPastEntranceGapToMMB1TopSide))]
        [MemberData(nameof(MMB1PastPortalBigKeyDoorToMMB1TopSide))]
        [MemberData(nameof(MMB1PastBridgeBigKeyDoorToMMB1TopSide))]
        [MemberData(nameof(MMB1TopSideToMMB1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoorToMMB1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(MMB1RightSideBeyondBlueBlocksToMMB1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LobbyBeyondBlueBlocksToMMB1RightSideKeyDoor))]
        [MemberData(nameof(MMB1RightSideBeyondBlueBlocksToMMB1RightSideKeyDoor))]
        [MemberData(nameof(MMB1TopSideToMMB1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoorToMMB1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LobbyBeyondBlueBlocksToMMB1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(MMPastEntranceGapToMMB1LeftSideFirstKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoorToMMB1LeftSideFirstKeyDoor))]
        [MemberData(nameof(MMPastEntranceGapToMMB1LeftSidePastFirstKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoorToMMB1LeftSidePastFirstKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoorToMMB1LeftSideSecondKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoorToMMB1LeftSideSecondKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoorToMMB1LeftSidePastSecondKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoorToMMB1PastFourTorchRoom))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoorToMMF1PastFourTorchRoom))]
        [MemberData(nameof(MMPastEntranceGapToMMB1PastPortalBigKeyDoor))]
        [MemberData(nameof(MMB1TopSideToMMBridgeBigKeyDoor))]
        [MemberData(nameof(MMB1PastBridgeBigKeyDoorToMMBridgeBigKeyDoor))]
        [MemberData(nameof(MMB1TopSideToMMB1PastBridgeBigKeyDoor))]
        [MemberData(nameof(MMB2PastWorthlessKeyDoorToMMDarkRoom))]
        [MemberData(nameof(MMB1PastBridgeBigKeyDoorToMMDarkRoom))]
        [MemberData(nameof(MMDarkRoomToMMB2WorthlessKeyDoor))]
        [MemberData(nameof(MMB2PastWorthlessKeyDoorToMMB2WorthlessKeyDoor))]
        [MemberData(nameof(MMDarkRoomToMMB2PastWorthlessKeyDoor))]
        [MemberData(nameof(MMDarkRoomToMMB2PastCaneOfSomariaSwitch))]
        [MemberData(nameof(MMB2PastCaneOfSomariaSwitchToMMBossRoom))]
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
        
        public static IEnumerable<object[]> MMEntryToMM =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.MMEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MM,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMToMMPastEntranceGap =>
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.BonkOverLedge, false),
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.BonkOverLedge, false),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1TopSideToMMPastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new[]
                    {
                        KeyDoorID.MMB1TopLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new[]
                    {
                        KeyDoorID.MMB1TopRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoorToMMPastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.MMB1LeftSideFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMPastEntranceGapToMMBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new[]
                    {
                        KeyDoorID.MMBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMPastEntranceGapToMMB1TopLeftKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1TopSideToMMB1TopLeftKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMPastEntranceGapToMMB1TopRightKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1TopSideToMMB1TopRightKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMPastEntranceGapToMMB1TopSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new[]
                    {
                        KeyDoorID.MMB1TopLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new[]
                    {
                        KeyDoorID.MMB1TopRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1PastPortalBigKeyDoorToMMB1TopSide =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1PastPortalBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1PastBridgeBigKeyDoorToMMB1TopSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.MMBridgeBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1TopSideToMMB1LobbyBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoorToMMB1LobbyBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1RightSideBeyondBlueBlocksToMMB1LobbyBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                    },
                    new[]
                    {
                        KeyDoorID.MMB1RightSideKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LobbyBeyondBlueBlocksToMMB1RightSideKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LobbyBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1RightSideBeyondBlueBlocksToMMB1RightSideKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1TopSideToMMB1RightSideBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoorToMMB1RightSideBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LobbyBeyondBlueBlocksToMMB1RightSideBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LobbyBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LobbyBeyondBlueBlocks
                    },
                    new[]
                    {
                        KeyDoorID.MMB1RightSideKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMPastEntranceGapToMMB1LeftSideFirstKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoorToMMB1LeftSideFirstKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMPastEntranceGapToMMB1LeftSidePastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new[]
                    {
                        KeyDoorID.MMB1LeftSideFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoorToMMB1LeftSidePastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.MMB1LeftSideSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoorToMMB1LeftSideSecondKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoorToMMB1LeftSideSecondKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoorToMMB1LeftSidePastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.MMB1LeftSideSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoorToMMB1PastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoorToMMF1PastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMPastEntranceGapToMMB1PastPortalBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new[]
                    {
                        KeyDoorID.MMPortalBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1TopSideToMMBridgeBigKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1PastBridgeBigKeyDoorToMMBridgeBigKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1TopSideToMMB1PastBridgeBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new[]
                    {
                        KeyDoorID.MMBridgeBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB2PastWorthlessKeyDoorToMMDarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB2PastWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB2PastWorthlessKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.MMB2WorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB1PastBridgeBigKeyDoorToMMDarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, false)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMDarkRoomToMMB2WorthlessKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB2PastWorthlessKeyDoorToMMB2WorthlessKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB2PastWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMDarkRoomToMMB2PastWorthlessKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new[]
                    {
                        KeyDoorID.MMB2WorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMDarkRoomToMMB2PastCaneOfSomariaSwitch =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> MMB2PastCaneOfSomariaSwitchToMMBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB2PastCaneOfSomariaSwitch
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.MMB2PastCaneOfSomariaSwitch
                    },
                    new[]
                    {
                        KeyDoorID.MMBossRoomBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
