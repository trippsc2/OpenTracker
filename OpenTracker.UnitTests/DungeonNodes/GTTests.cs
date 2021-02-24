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
    public class GTTests
    {
        [Theory]
        [MemberData(nameof(GTEntry_To_GT))]
        [MemberData(nameof(GT1FLeft_To_GTBobsTorch))]
        [MemberData(nameof(GT_To_GT1FLeft))]
        [MemberData(nameof(GT1FRight_To_GT1FLeft))]
        [MemberData(nameof(GT1FLeft_To_GT1FLeftToRightKeyDoor))]
        [MemberData(nameof(GT1FRight_To_GT1FLeftToRightKeyDoor))]
        [MemberData(nameof(GT1FLeft_To_GT1FLeftPastHammerBlocks))]
        [MemberData(nameof(GT1FLeftPastHammerBlocks_To_GT1FLeftDMsRoom))]
        [MemberData(nameof(GT1FLeftPastHammerBlocks_To_GT1FLeftPastBonkableGaps))]
        [MemberData(nameof(GT1FLeftMapChestRoom_To_GT1FLeftPastBonkableGaps))]
        [MemberData(nameof(GT1FLeftSpikeTrapPortalRoom_To_GT1FLeftPastBonkableGaps))]
        [MemberData(nameof(GT1FLeftPastBonkableGaps_To_GT1FMapChestRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftMapChestRoom_To_GT1FMapChestRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastBonkableGaps_To_GT1FLeftMapChestRoom))]
        [MemberData(nameof(GT1FLeftPastBonkableGaps_To_GT1FSpikeTrapPortalRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftSpikeTrapPortalRoom_To_GT1FSpikeTrapPortalRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastBonkableGaps_To_GT1FLeftSpikeTrapPortalRoom))]
        [MemberData(nameof(GT1FLeftSpikeTrapPortalRoom_To_GT1FLeftFiresnakeRoom))]
        [MemberData(nameof(GT1FLeftFiresnakeRoom_To_GT1FLeftPastFiresnakeRoomGap))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomKeyDoor_To_GT1FLeftPastFiresnakeRoomGap))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomGap_To_GT1FFiresnakeRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomKeyDoor_To_GT1FFiresnakeRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomGap_To_GT1FLeftPastFiresnakeRoomKeyDoor))]
        [MemberData(nameof(GT1FLeftPastFiresnakeRoomKeyDoor_To_GT1FLeftRandomizerRoom))]
        [MemberData(nameof(GT_To_GT1FRight))]
        [MemberData(nameof(GT1FLeft_To_GT1FRight))]
        [MemberData(nameof(GT1FRight_To_GT1FRightTileRoom))]
        [MemberData(nameof(GT1FRightFourTorchRoom_To_GT1FRightTileRoom))]
        [MemberData(nameof(GT1FRightTileRoom_To_GT1FTileRoomKeyDoor))]
        [MemberData(nameof(GT1FRightFourTorchRoom_To_GT1FTileRoomKeyDoor))]
        [MemberData(nameof(GT1FRightTileRoom_To_GT1FRightFourTorchRoom))]
        [MemberData(nameof(GT1FRightFourTorchRoom_To_GT1FRightCompassRoom))]
        [MemberData(nameof(GT1FRightCompassRoom_To_GT1FRightPastCompassRoomPortal))]
        [MemberData(nameof(GT1FRightCollapsingWalkway_To_GT1FRightPastCompassRoomPortal))]
        [MemberData(nameof(GT1FRightPastCompassRoomPortal_To_GT1FCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(GT1FRightCollapsingWalkway_To_GT1FCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(GT1FRightPastCompassRoomPortal_To_GT1FRightCollapsingWalkway))]
        [MemberData(nameof(GT1FLeftRandomizerRoom_To_GT1FBottomRoom))]
        [MemberData(nameof(GT1FRightCollapsingWalkway_To_GT1FBottomRoom))]
        [MemberData(nameof(GTBoss1_To_GTB1BossChests))]
        [MemberData(nameof(GT1FBottomRoom_To_GTBigChest))]
        [MemberData(nameof(GT_To_GT3FPastRedGoriyaRooms))]
        [MemberData(nameof(GT3FPastBigKeyDoor_To_GT3FPastRedGoriyaRooms))]
        [MemberData(nameof(GT3FPastRedGoriyaRooms_To_GT3FBigKeyDoor))]
        [MemberData(nameof(GT3FPastBigKeyDoor_To_GT3FBigKeyDoor))]
        [MemberData(nameof(GT3FPastRedGoriyaRooms_To_GT3FPastBigKeyDoor))]
        [MemberData(nameof(GTBoss2_To_GT4FPastBoss2))]
        [MemberData(nameof(GT4FPastBoss2_To_GT5FPastFourTorchRooms))]
        [MemberData(nameof(GT6FPastFirstKeyDoor_To_GT5FPastFourTorchRooms))]
        [MemberData(nameof(GT5FPastFourTorchRooms_To_GT6FFirstKeyDoor))]
        [MemberData(nameof(GT6FPastFirstKeyDoor_To_GT6FFirstKeyDoor))]
        [MemberData(nameof(GT5FPastFourTorchRooms_To_GT6FPastFirstKeyDoor))]
        [MemberData(nameof(GT6FBossRoom_To_GT6FPastFirstKeyDoor))]
        [MemberData(nameof(GT6FPastFirstKeyDoor_To_GT6FSecondKeyDoor))]
        [MemberData(nameof(GT6FBossRoom_To_GT6FSecondKeyDoor))]
        [MemberData(nameof(GT6FPastFirstKeyDoor_To_GT6FPastFirstKeyDoor))]
        [MemberData(nameof(GTBoss3_To_GTBoss3Item))]
        [MemberData(nameof(GTBoss3Item_To_GT6FPastBossRoomGap))]
        [MemberData(nameof(GT6FBossRoom_To_GT6FPastBossRoomGap))]
        [MemberData(nameof(GT6FPastBossRoomGap_To_GTFinalBossRoom))]
        public void Tests(
            ModeSaveData mode, RequirementNodeID[] accessibleEntryNodes,
            DungeonNodeID[] accessibleNodes, KeyDoorID[] unlockedDoors, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, LocationID dungeonID, DungeonNodeID id,
            AccessibilityLevel expected)
        {
            RequirementNodeDictionary.Instance.Reset();
            var dungeon = (IDungeon)LocationDictionary.Instance[dungeonID];
            var dungeonData = MutableDungeonFactory.GetMutableDungeon(dungeon);
            dungeon.FinishMutableDungeonCreation(dungeonData);
            dungeonData.Reset();
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            Mode.Instance.Load(mode);

            foreach (var node in accessibleEntryNodes)
            {
                RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
            }

            foreach (var node in accessibleNodes)
            {
                dungeonData.Nodes[node].AlwaysAccessible = true;
            }

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            foreach (var door in unlockedDoors)
            {
                dungeonData.KeyDoors[door].Unlocked = true;
            }

            Assert.Equal(expected, dungeonData.Nodes[id].Accessibility);
        }

        public static IEnumerable<object[]> GTEntry_To_GT =>
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
                    new RequirementNodeID[]
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

        public static IEnumerable<object[]> GT1FLeft_To_GTBobsTorch =>
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT_To_GT1FLeft =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRight_To_GT1FLeft =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FRight
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeft_To_GT1FLeftToRightKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRight_To_GT1FLeftToRightKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeft_To_GT1FLeftPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftPastHammerBlocks_To_GT1FLeftDMsRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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

        public static IEnumerable<object[]> GT1FLeftPastHammerBlocks_To_GT1FLeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    LocationID.GanonsTower,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GT1FLeftMapChestRoom_To_GT1FLeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftMapChestRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeftSpikeTrapPortalRoom_To_GT1FLeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeftPastBonkableGaps_To_GT1FMapChestRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftMapChestRoom_To_GT1FMapChestRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftPastBonkableGaps_To_GT1FLeftMapChestRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeftPastBonkableGaps_To_GT1FSpikeTrapPortalRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftSpikeTrapPortalRoom_To_GT1FSpikeTrapPortalRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftPastBonkableGaps_To_GT1FLeftSpikeTrapPortalRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastBonkableGaps
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeftSpikeTrapPortalRoom_To_GT1FLeftFiresnakeRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftFiresnakeRoom_To_GT1FLeftPastFiresnakeRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftFiresnakeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftFiresnakeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftFiresnakeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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

        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomKeyDoor_To_GT1FLeftPastFiresnakeRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomGap_To_GT1FFiresnakeRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomKeyDoor_To_GT1FFiresnakeRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomGap_To_GT1FLeftPastFiresnakeRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeftPastFiresnakeRoomGap
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeftPastFiresnakeRoomKeyDoor_To_GT1FLeftRandomizerRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT_To_GT1FRight =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FLeft_To_GT1FRight =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FLeft
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FRight_To_GT1FRightTileRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightFourTorchRoom_To_GT1FRightTileRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FRightFourTorchRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FRightTileRoom_To_GT1FTileRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightFourTorchRoom_To_GT1FTileRoomKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightTileRoom_To_GT1FRightFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FRightTileRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FRightFourTorchRoom_To_GT1FRightCompassRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightCompassRoom_To_GT1FRightPastCompassRoomPortal =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightCollapsingWalkway_To_GT1FRightPastCompassRoomPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FRightCollapsingWalkway
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FRightPastCompassRoomPortal_To_GT1FCollapsingWalkwayKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightCollapsingWalkway_To_GT1FCollapsingWalkwayKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightPastCompassRoomPortal_To_GT1FRightCollapsingWalkway =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FRightPastCompassRoomPortal
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT1FLeftRandomizerRoom_To_GT1FBottomRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FRightCollapsingWalkway_To_GT1FBottomRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GTBoss1_To_GTB1BossChests =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT1FBottomRoom_To_GTBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT1FBottomRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT_To_GT3FPastRedGoriyaRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT3FPastBigKeyDoor_To_GT3FPastRedGoriyaRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT3FPastBigKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT3FPastRedGoriyaRooms_To_GT3FBigKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT3FPastBigKeyDoor_To_GT3FBigKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT3FPastRedGoriyaRooms_To_GT3FPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT3FPastRedGoriyaRooms
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GTBoss2_To_GT4FPastBoss2 =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT4FPastBoss2_To_GT5FPastFourTorchRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT4FPastBoss2
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT4FPastBoss2
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT4FPastBoss2
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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

        public static IEnumerable<object[]> GT6FPastFirstKeyDoor_To_GT5FPastFourTorchRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT5FPastFourTorchRooms_To_GT6FFirstKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT6FPastFirstKeyDoor_To_GT6FFirstKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT5FPastFourTorchRooms_To_GT6FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT5FPastFourTorchRooms
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT6FBossRoom_To_GT6FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT6FBossRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GT6FPastFirstKeyDoor_To_GT6FSecondKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT6FBossRoom_To_GT6FSecondKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT6FPastFirstKeyDoor_To_GT6FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT6FPastFirstKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> GTBoss3_To_GTBoss3Item =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GTBoss3
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GTBoss3
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GTBoss3
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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

        public static IEnumerable<object[]> GTBoss3Item_To_GT6FPastBossRoomGap =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT6FBossRoom_To_GT6FPastBossRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> GT6FPastBossRoomGap_To_GTFinalBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.GT6FPastBossRoomGap
                    },
                    new KeyDoorID[]
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
