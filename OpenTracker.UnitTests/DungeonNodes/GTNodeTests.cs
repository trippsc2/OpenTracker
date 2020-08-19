using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OpenTracker.UnitTests.DungeonNodes
{
    [Collection("Tests")]
    public class GTNodeTests
    {
        [Theory]
        [MemberData(nameof(Entry_To_GT))]
        [MemberData(nameof(F1Left_To_GT))]
        [MemberData(nameof(F1Right_To_GT))]
        [MemberData(nameof(F3PastRedGoriyaRooms_To_GT))]
        [MemberData(nameof(F1Left_To_BobsTorch))]
        [MemberData(nameof(GT_To_F1Left))]
        [MemberData(nameof(F1LeftPastHammerBlocks_To_F1Left))]
        [MemberData(nameof(F1BottomRoom_To_F1Left))]
        [MemberData(nameof(F1Left_To_F1LeftPastHammerBlocks))]
        [MemberData(nameof(F1LeftDMsRoom_To_F1LeftPastHammerBlocks))]
        [MemberData(nameof(F1LeftPastBonkableGaps_To_F1LeftPastHammerBlocks))]
        [MemberData(nameof(F1LeftPastHammerBlocks_To_F1LeftDMsRoom))]
        [MemberData(nameof(F1LeftPastHammerBlocks_To_F1LeftPastBonkableGaps))]
        [MemberData(nameof(F1LeftMapChestRoom_To_F1LeftPastBonkableGaps))]
        [MemberData(nameof(F1LeftPastBonkableGaps_To_F1LeftMapChestRoom))]
        [MemberData(nameof(F1LeftSpikeTrapPortalRoom_To_F1LeftPastBonkableGaps))]
        [MemberData(nameof(F1LeftPastBonkableGaps_To_F1LeftSpikeTrapPortalRoom))]
        [MemberData(nameof(F1LeftSpikeTrapPortalRoom_To_F1LeftFiresnakeRoom))]
        [MemberData(nameof(F1LeftFiresnakeRoom_To_F1LeftPastFiresnakeRoomGap))]
        [MemberData(nameof(F1LeftPastFiresnakeRoomKeyDoor_To_F1LeftPastFiresnakeRoomGap))]
        [MemberData(nameof(F1LeftPastFiresnakeRoomGap_To_F1LeftPastFiresnakeRoomKeyDoor))]
        [MemberData(nameof(F1LeftPastFiresnakeRoomKeyDoor_To_F1LeftRandomizerRoom))]
        [MemberData(nameof(GT_To_F1Right))]
        [MemberData(nameof(F1RightTileRoom_To_F1Right))]
        [MemberData(nameof(F1Right_To_F1RightTileRoom))]
        [MemberData(nameof(F1RightFourTorchRoom_To_F1RightTileRoom))]
        [MemberData(nameof(F1RightTileRoom_To_F1RightFourTorchRoom))]
        [MemberData(nameof(F1RightCompassRoom_To_F1RightFourTorchRoom))]
        [MemberData(nameof(F1RightFourTorchRoom_To_F1RightCompassRoom))]
        [MemberData(nameof(F1RightCompassRoom_To_F1RightPastCompassRoomPortal))]
        [MemberData(nameof(F1RightCollapsingWalkway_To_F1RightPastCompassRoomPortal))]
        [MemberData(nameof(F1RightPastCompassRoomPortal_To_F1RightCollapsingWalkway))]
        [MemberData(nameof(F1LeftRandomizerRoom_To_F1BottomRoom))]
        [MemberData(nameof(F1RightCollapsingWalkway_To_F1BottomRoom))]
        [MemberData(nameof(Boss1_To_F1BottomRoom))]
        [MemberData(nameof(Boss1_To_B1BossChests))]
        [MemberData(nameof(F1BottomRoom_To_BigChest))]
        [MemberData(nameof(GT_To_F3PastRedGoriyaRooms))]
        [MemberData(nameof(F3PastBigKeyDoor_To_F3PastRedGoriyaRooms))]
        [MemberData(nameof(F3PastRedGoriyaRooms_To_F3PastBigKeyDoor))]
        [MemberData(nameof(Boss2_To_F3PastBigKeyDoor))]
        [MemberData(nameof(Boss2_To_F4PastBoss2))]
        [MemberData(nameof(F4PastBoss2_To_F5PastFourTorchRooms))]
        [MemberData(nameof(F6PastFirstKeyDoor_To_F5PastFourTorchRooms))]
        [MemberData(nameof(F5PastFourTorchRooms_To_F6PastFirstKeyDoor))]
        [MemberData(nameof(F6BossRoom_To_F6PastFirstKeyDoor))]
        [MemberData(nameof(F6PastFirstKeyDoor_To_F6BossRoom))]
        [MemberData(nameof(F6BossRoom_To_F6PastBossRoomGap))]
        [MemberData(nameof(Boss3_To_F6PastBossRoomGap))]
        [MemberData(nameof(F6PastBossRoomGap_To_FinalBossRoom))]
        public void AccessibilityTests(
            DungeonNodeID id, ItemPlacement itemPlacement,
            DungeonItemShuffle dungeonItemShuffle, WorldState worldState,
            bool entranceShuffle, bool enemyShuffle, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, KeyDoorID[] keyDoors,
            AccessibilityLevel expected)
        {
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            ((IDungeon)LocationDictionary.Instance[LocationID.GanonsTower]).DungeonDataQueue
                .TryPeek(out IMutableDungeon dungeonData);

            foreach (var keyDoor in dungeonData.SmallKeyDoors.Values)
            {
                keyDoor.Unlocked = keyDoors.Contains(keyDoor.ID);
            }

            foreach (var keyDoor in dungeonData.BigKeyDoors.Values)
            {
                keyDoor.Unlocked = keyDoors.Contains(keyDoor.ID);
            }

            Assert.Equal(expected, dungeonData.RequirementNodes[id].Accessibility);
        }

        public static IEnumerable<object[]> Entry_To_GT =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1Left_To_GT =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1Right_To_GT =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F3PastRedGoriyaRooms_To_GT =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1Left_To_BobsTorch =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GTBobsTorch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GTBobsTorch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    DungeonNodeID.GTBobsTorch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GT_To_F1Left =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeft,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeft,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastHammerBlocks_To_F1Left =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeft,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeft,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1BottomRoom_To_F1Left =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeft,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FBottomRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeft,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FBottomRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1Left_To_F1LeftPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 1),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftTest, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftDMsRoom_To_F1LeftPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftDMsRoomTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftDMsRoomTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftDMsRoomTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftDMsRoomTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftDMsRoomTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastBonkableGaps_To_F1LeftPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastHammerBlocks_To_F1LeftDMsRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftDMsRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftDMsRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftDMsRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftDMsRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftDMsRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastHammerBlocks_To_F1LeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastHammerBlocksTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftMapChestRoom_To_F1LeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftMapChestRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftMapChestRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftMapChestRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FMapChestRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftSpikeTrapPortalRoom_To_F1LeftPastBonkableGaps =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftSpikeTrapPortalRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftSpikeTrapPortalRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftSpikeTrapPortalRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastBonkableGaps_To_F1LeftMapChestRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftMapChestRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftMapChestRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftMapChestRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FMapChestRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastBonkableGaps_To_F1LeftSpikeTrapPortalRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftSpikeTrapPortalRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftSpikeTrapPortalRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftSpikeTrapPortalRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastBonkableGapsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftSpikeTrapPortalRoom_To_F1LeftFiresnakeRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftFiresnakeRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftSpikeTrapPortalRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftFiresnakeRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftSpikeTrapPortalRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftFiresnakeRoom_To_F1LeftPastFiresnakeRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftFiresnakeRoomTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftFiresnakeRoomTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftFiresnakeRoomTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftFiresnakeRoomTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftFiresnakeRoomTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastFiresnakeRoomKeyDoor_To_F1LeftPastFiresnakeRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FFiresnakeRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastFiresnakeRoomGap_To_F1LeftPastFiresnakeRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomGapTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FFiresnakeRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftPastFiresnakeRoomKeyDoor_To_F1LeftRandomizerRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FLeftRandomizerRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FLeftRandomizerRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftPastFiresnakeRoomKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
           };

        public static IEnumerable<object[]> GT_To_F1Right =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRight,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRight,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightTileRoom_To_F1Right =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRight,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTileRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRight,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTileRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1Right_To_F1RightTileRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightTileRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightTileRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightTileRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightFourTorchRoom_To_F1RightTileRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightTileRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightFourTorchRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightTileRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightFourTorchRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightTileRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightFourTorchRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FTileRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> F1RightTileRoom_To_F1RightFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTileRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTileRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightTileRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FTileRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightCompassRoom_To_F1RightFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCompassRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCompassRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightFourTorchRoom_To_F1RightCompassRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightCompassRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightFourTorchRoomTest, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightCompassRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightFourTorchRoomTest, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightCompassRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightFourTorchRoomTest, 1),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightCompassRoom_To_F1RightPastCompassRoomPortal =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCompassRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCompassRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightCollapsingWalkway_To_F1RightPastCompassRoomPortal =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCollapsingWalkwayTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCollapsingWalkwayTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCollapsingWalkwayTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FCollapsingWalkwayKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightPastCompassRoomPortal_To_F1RightCollapsingWalkway =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FRightCollapsingWalkway,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightPastCompassRoomPortalTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightCollapsingWalkway,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightPastCompassRoomPortalTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FRightCollapsingWalkway,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightPastCompassRoomPortalTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT1FCollapsingWalkwayKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1LeftRandomizerRoom_To_F1BottomRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FBottomRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftRandomizerRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FBottomRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FLeftRandomizerRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RightCollapsingWalkway_To_F1BottomRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FBottomRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCollapsingWalkwayTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FBottomRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FRightCollapsingWalkwayTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Boss1_To_F1BottomRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT1FBottomRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss1Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT1FBottomRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Boss1_To_B1BossChests =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GTB1BossChests,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss1Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GTB1BossChests,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1BottomRoom_To_BigChest =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GTBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FBottomRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GTBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FBottomRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GTBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT1FBottomRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GTBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GT_To_F3PastRedGoriyaRooms =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTTest, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F3PastBigKeyDoor_To_F3PastRedGoriyaRooms =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastBigKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastBigKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastBigKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT3FBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F3PastRedGoriyaRooms_To_F3PastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT3FPastRedGoriyaRoomsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT3FBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Boss2_To_F3PastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss2Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss2Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Boss2_To_F4PastBoss2 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT4FPastBoss2,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss2Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT4FPastBoss2,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss2Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F4PastBoss2_To_F5PastFourTorchRooms =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT4FPastBoss2Test, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT4FPastBoss2Test, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT4FPastBoss2Test, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT4FPastBoss2Test, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F6PastFirstKeyDoor_To_F5PastFourTorchRooms =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT6FFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F5PastFourTorchRooms_To_F6PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT5FPastFourTorchRoomsTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT5FPastFourTorchRoomsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT5FPastFourTorchRoomsTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT6FFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F6BossRoom_To_F6PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FBossRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT6FSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F6PastFirstKeyDoor_To_F6BossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT6FBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT6FSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F6BossRoom_To_F6PastBossRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT6FPastBossRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FBossRoomTest, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastBossRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FBossRoomTest, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastBossRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FBossRoomTest, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastBossRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FBossRoomTest, 1),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> Boss3_To_F6PastBossRoomGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GT6FPastBossRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss3Test, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastBossRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss3Test, 1),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GT6FPastBossRoomGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GTBoss3Test, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F6PastBossRoomGap_To_FinalBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.GTFinalBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastBossRoomGapTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GTFinalBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastBossRoomGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.GTFinalBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GT6FPastBossRoomGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.GT7FBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
