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
    public class GTTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(GTEntryToGT))]
        [MemberData(nameof(GT1FLeftToGTBobsTorch))]
        [MemberData(nameof(GTToGT1FLeft))]
        [MemberData(nameof(GT1FRightToGT1FLeft))]
        [MemberData(nameof(GT1FLeftToGT1FLeftToRightKeyDoor))]
        [MemberData(nameof(GT1FRightToGT1FLeftToRightKeyDoor))]
        [MemberData(nameof(GT1FLeftToGT1FLeftPastHammerBlocks))]
        [MemberData(nameof(GT1FLeftPastHammerBlocksToGT1FLeftDMsRoom))]
        [MemberData(nameof(GT1FLeftPastHammerBlocksToGT1FLeftPastBonkableGaps))]
        [MemberData(nameof(GT1FLeftMapChestRoomToGT1FLeftPastBonkableGaps))]
        [MemberData(nameof(GT1FLeftSpikeTrapPortalRoomToGT1FLeftPastBonkableGaps))]
        [MemberData(nameof(GT1FLeftPastBonkableGapsToGT1FMapChestRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftMapChestRoomToGT1FMapChestRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastBonkableGapsToGT1FLeftMapChestRoom))]
        [MemberData(nameof(GT1FLeftPastBonkableGapsToGT1FSpikeTrapPortalRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftSpikeTrapPortalRoomToGT1FSpikeTrapPortalRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastBonkableGapsToGT1FLeftSpikeTrapPortalRoom))]
        [MemberData(nameof(GT1FLeftSpikeTrapPortalRoomToGT1FLeftFiresnakeRoom))]
        [MemberData(nameof(GT1FLeftFiresnakeRoomToGT1FLeftPastFiresnakeRoomGap))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomKeyDoorToGT1FLeftPastFiresnakeRoomGap))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomGapToGT1FFiresnakeRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomKeyDoorToGT1FFiresnakeRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomGapToGT1FLeftPastFiresnakeRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomKeyDoorToGT1FLeftRandomizerRoom))]
        [MemberData(nameof(GTToGT1FRight))]
        [MemberData(nameof(GT1FLeftToGT1FRight))]
        [MemberData(nameof(GT1FRightToGT1FRightTileRoom))]
        [MemberData(nameof(GT1FRightFourTorchRoomToGT1FRightTileRoom))]
        [MemberData(nameof(GT1FRightTileRoomToGT1FTileRoomKeyDoor))]
        [MemberData(nameof(GT1FRightFourTorchRoomToGT1FTileRoomKeyDoor))]
        [MemberData(nameof(GT1FRightTileRoomToGT1FRightFourTorchRoom))]
        [MemberData(nameof(GT1FRightFourTorchRoomToGT1FRightCompassRoom))]
        [MemberData(nameof(GT1FRightCompassRoomToGT1FRightPastCompassRoomPortal))]
        [MemberData(nameof(GT1FRightCollapsingWalkwayToGT1FRightPastCompassRoomPortal))]
        [MemberData(nameof(GT1FRightPastCompassRoomPortalToGT1FCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(GT1FRightCollapsingWalkwayToGT1FCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(GT1FRightPastCompassRoomPortalToGT1FRightCollapsingWalkway))]
        [MemberData(nameof(GT1FLeftRandomizerRoomToGT1FBottomRoom))]
        [MemberData(nameof(GT1FRightCollapsingWalkwayToGT1FBottomRoom))]
        [MemberData(nameof(GTBoss1ToGTB1BossChests))]
        [MemberData(nameof(GT1FBottomRoomToGTBigChest))]
        [MemberData(nameof(GTToGT3FPastRedGoriyaRooms))]
        [MemberData(nameof(GT3FPastBigKeyDoorToGT3FPastRedGoriyaRooms))]
        [MemberData(nameof(GT3FPastRedGoriyaRoomsToGT3FBigKeyDoor))]
        [MemberData(nameof(GT3FPastBigKeyDoorToGT3FBigKeyDoor))]
        [MemberData(nameof(GT3FPastRedGoriyaRoomsToGT3FPastBigKeyDoor))]
        [MemberData(nameof(GTBoss2ToGT4FPastBoss2))]
        [MemberData(nameof(GT4FPastBoss2ToGT5FPastFourTorchRooms))]
        [MemberData(nameof(GT6FPastFirstKeyDoorToGT5FPastFourTorchRooms))]
        [MemberData(nameof(GT5FPastFourTorchRoomsToGT6FFirstKeyDoor))]
        [MemberData(nameof(GT6FPastFirstKeyDoorToGT6FFirstKeyDoor))]
        [MemberData(nameof(GT5FPastFourTorchRoomsToGT6FPastFirstKeyDoor))]
        [MemberData(nameof(GT6FBossRoomToGT6FPastFirstKeyDoor))]
        [MemberData(nameof(GT6FPastFirstKeyDoorToGT6FSecondKeyDoor))]
        [MemberData(nameof(GT6FBossRoomToGT6FSecondKeyDoor))]
        [MemberData(nameof(GT6FPastFirstKeyDoorToGT6FPastFirstKeyDoor))]
        [MemberData(nameof(GTBoss3ToGTBoss3Item))]
        [MemberData(nameof(GTBoss3ItemToGT6FPastBossRoomGap))]
        [MemberData(nameof(GT6FBossRoomToGT6FPastBossRoomGap))]
        [MemberData(nameof(GT6FPastBossRoomGapToGTFinalBossRoom))]
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
        
        public static IEnumerable<object[]> GTEntryToGT =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.GTEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftToGTBobsTorch =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBobsTorch,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBobsTorch,
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBobsTorch,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GTToGT1FLeft =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeft,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeft,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightToGT1FLeft =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRight
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeft,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRight
                    },
                    new[]
                    {
                        KeyDoorID.GT1FLeftToRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeft,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftToGT1FLeftToRightKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftToRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftToRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightToGT1FLeftToRightKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftToRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRight
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftToRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftToGT1FLeftPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastHammerBlocksToGT1FLeftDMsRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftDMsRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftDMsRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftDMsRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastHammerBlocksToGT1FLeftPastBonkableGaps =>
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
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, false),
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
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
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, false),
                        (SequenceBreakType.BonkOverLedge, false)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
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
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BonkOverLedge, false)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
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
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, false),
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
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
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
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
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
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
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftMapChestRoomToGT1FLeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftMapChestRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftMapChestRoom
                    },
                    new[]
                    {
                        KeyDoorID.GT1FMapChestRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftSpikeTrapPortalRoomToGT1FLeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                    },
                    new[]
                    {
                        KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastBonkableGapsToGT1FMapChestRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FMapChestRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FMapChestRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftMapChestRoomToGT1FMapChestRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FMapChestRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftMapChestRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FMapChestRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastBonkableGapsToGT1FLeftMapChestRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftMapChestRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new[]
                    {
                        KeyDoorID.GT1FMapChestRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftMapChestRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastBonkableGapsToGT1FSpikeTrapPortalRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftSpikeTrapPortalRoomToGT1FSpikeTrapPortalRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastBonkableGapsToGT1FLeftSpikeTrapPortalRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftSpikeTrapPortalRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new[]
                    {
                        KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftSpikeTrapPortalRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftSpikeTrapPortalRoomToGT1FLeftFiresnakeRoom =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftFiresnakeRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftFiresnakeRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftFiresnakeRoomToGT1FLeftPastFiresnakeRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftFiresnakeRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftFiresnakeRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftFiresnakeRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomKeyDoorToGT1FLeftPastFiresnakeRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.GT1FFiresnakeRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomGapToGT1FFiresnakeRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FFiresnakeRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FFiresnakeRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomKeyDoorToGT1FFiresnakeRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FFiresnakeRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FFiresnakeRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomGapToGT1FLeftPastFiresnakeRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomGap
                    },
                    new[]
                    {
                        KeyDoorID.GT1FFiresnakeRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomKeyDoorToGT1FLeftRandomizerRoom =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftRandomizerRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftRandomizerRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GTToGT1FRight =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRight,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRight,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftToGT1FRight =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRight,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new[]
                    {
                        KeyDoorID.GT1FLeftToRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRight,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightToGT1FRightTileRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRight
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightTileRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRight
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightTileRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightFourTorchRoomToGT1FRightTileRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightTileRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightFourTorchRoom
                    },
                    new[]
                    {
                        KeyDoorID.GT1FTileRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightTileRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightTileRoomToGT1FTileRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FTileRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightTileRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FTileRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightFourTorchRoomToGT1FTileRoomKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FTileRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FTileRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightTileRoomToGT1FRightFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightTileRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightTileRoom
                    },
                    new[]
                    {
                        KeyDoorID.GT1FTileRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightFourTorchRoomToGT1FRightCompassRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightCompassRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightCompassRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightCompassRoomToGT1FRightPastCompassRoomPortal =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightCompassRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightCollapsingWalkwayToGT1FRightPastCompassRoomPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightCollapsingWalkway
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightCollapsingWalkway
                    },
                    new[]
                    {
                        KeyDoorID.GT1FCollapsingWalkwayKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightPastCompassRoomPortalToGT1FCollapsingWalkwayKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightPastCompassRoomPortal
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightCollapsingWalkwayToGT1FCollapsingWalkwayKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightCollapsingWalkway
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightPastCompassRoomPortalToGT1FRightCollapsingWalkway =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightPastCompassRoomPortal
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightCollapsingWalkway,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightPastCompassRoomPortal
                    },
                    new[]
                    {
                        KeyDoorID.GT1FCollapsingWalkwayKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FRightCollapsingWalkway,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FLeftRandomizerRoomToGT1FBottomRoom =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FBottomRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FLeftRandomizerRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FBottomRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FRightCollapsingWalkwayToGT1FBottomRoom =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FBottomRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FRightCollapsingWalkway
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FBottomRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GTBoss1ToGTB1BossChests =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GTB1BossChests,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GTBoss1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTB1BossChests,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT1FBottomRoomToGTBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FBottomRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT1FBottomRoom
                    },
                    new[]
                    {
                        KeyDoorID.GTBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GTToGT3FPastRedGoriyaRooms =>
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
                        DungeonNodeID.GT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, false)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    AccessibilityLevel.None
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
                        DungeonNodeID.GT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    AccessibilityLevel.SequenceBreak
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
                        DungeonNodeID.GT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    AccessibilityLevel.Normal
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
                        DungeonNodeID.GT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT3FPastBigKeyDoorToGT3FPastRedGoriyaRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT3FPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT3FPastBigKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.GT3FBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT3FPastRedGoriyaRoomsToGT3FBigKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT3FPastRedGoriyaRooms
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT3FPastBigKeyDoorToGT3FBigKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT3FPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT3FPastRedGoriyaRoomsToGT3FPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT3FPastRedGoriyaRooms
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT3FPastRedGoriyaRooms
                    },
                    new[]
                    {
                        KeyDoorID.GT3FBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GTBoss2ToGT4FPastBoss2 =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT4FPastBoss2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GTBoss2
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT4FPastBoss2,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT4FPastBoss2ToGT5FPastFourTorchRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT4FPastBoss2
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT4FPastBoss2
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT4FPastBoss2
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT6FPastFirstKeyDoorToGT5FPastFourTorchRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.GT6FFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT5FPastFourTorchRoomsToGT6FFirstKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT5FPastFourTorchRooms
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT6FPastFirstKeyDoorToGT6FFirstKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT5FPastFourTorchRoomsToGT6FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT5FPastFourTorchRooms
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT5FPastFourTorchRooms
                    },
                    new[]
                    {
                        KeyDoorID.GT6FFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT6FBossRoomToGT6FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FBossRoom
                    },
                    new[]
                    {
                        KeyDoorID.GT6FSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT6FPastFirstKeyDoorToGT6FSecondKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT6FBossRoomToGT6FSecondKeyDoor =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT6FPastFirstKeyDoorToGT6FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.GT6FSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FBossRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GTBoss3ToGTBoss3Item =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GTBoss3
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBoss3Item,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GTBoss3
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBoss3Item,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GTBoss3
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GTBoss3Item,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GTBoss3ItemToGT6FPastBossRoomGap =>
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
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastBossRoomGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GTBoss3Item
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastBossRoomGap,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> GT6FBossRoomToGT6FPastBossRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastBossRoomGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT6FPastBossRoomGap,
                    AccessibilityLevel.SequenceBreak
                }
            };
        
        public static IEnumerable<object[]> GT6FPastBossRoomGapToGTFinalBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastBossRoomGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTFinalBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.GT6FPastBossRoomGap
                    },
                    new[]
                    {
                        KeyDoorID.GT7FBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.GanonsTower,
                    DungeonNodeID.GTFinalBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
