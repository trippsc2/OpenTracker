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
    public class MMNodeTests
    {
        [Theory]
        [MemberData(nameof(Entry_To_MM))]
        [MemberData(nameof(PastEntranceGap_To_MM))]
        [MemberData(nameof(MM_To_PastEntranceGap))]
        [MemberData(nameof(B1TopSide_To_PastEntranceGap))]
        [MemberData(nameof(B1LeftSidePastFirstKeyDoor_To_PastEntranceGap))]
        [MemberData(nameof(B1PastPortalBigKeyDoor_To_PastEntranceGap))]
        [MemberData(nameof(PastEntranceGap_To_BigChest))]
        [MemberData(nameof(PastEntranceGap_To_B1TopSide))]
        [MemberData(nameof(B1PastPortalBigKeyDoor_To_B1TopSide))]
        [MemberData(nameof(B1PastBridgeBigKeyDoor_To_B1TopSide))]
        [MemberData(nameof(B1TopSide_To_B1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(B1LeftSidePastFirstKeyDoor_To_B1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(B1TopSide_To_B1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(B1LeftSidePastFirstKeyDoor_To_B1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(PastEntranceGap_To_B1LeftSidePastFirstKeyDoor))]
        [MemberData(nameof(B1LeftSidePastSecondKeyDoor_To_B1LeftSidePastFirstKeyDoor))]
        [MemberData(nameof(B1LeftSidePastFirstKeyDoor_To_B1LeftSidePastSecondKeyDoor))]
        [MemberData(nameof(B1PastFourTorchRoom_To_B1LeftSidePastSecondKeyDoor))]
        [MemberData(nameof(B1LeftSidePastSecondKeyDoor_To_B1PastFourTorchRoom))]
        [MemberData(nameof(B1LeftSidePastSecondKeyDoor_To_F1PastFourTorchRoom))]
        [MemberData(nameof(PastEntranceGap_To_B1PastPortalBigKeyDoor))]
        [MemberData(nameof(B1TopSide_To_B1PastBridgeBigKeyDoor))]
        [MemberData(nameof(B1PastBridgeBigKeyDoor_To_DarkRoom))]
        [MemberData(nameof(B2PastWorthlessKeyDoor_To_DarkRoom))]
        [MemberData(nameof(DarkRoom_To_B2PastWorthlessKeyDoor))]
        [MemberData(nameof(DarkRoom_To_B2PastCaneOfSomariaSwitch))]
        [MemberData(nameof(BossRoom_To_B2PastCaneOfSomariaSwitch))]
        [MemberData(nameof(B2PastCaneOfSomariaSwitch_To_BossRoom))]
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

            ((IDungeon)LocationDictionary.Instance[LocationID.MiseryMire]).DungeonDataQueue
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

        public static IEnumerable<object[]> Entry_To_MM =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastEntranceGap_To_MM =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MM,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MM_To_PastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1TopSide_To_PastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopLeftKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1LeftSidePastFirstKeyDoor_To_PastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastPortalBigKeyDoor_To_PastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastPortalBigKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastPortalBigKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMPastEntranceGap,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastPortalBigKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMPortalBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastEntranceGap_To_BigChest =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastEntranceGap_To_B1TopSide =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopLeftKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastPortalBigKeyDoor_To_B1TopSide =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastPortalBigKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastPortalBigKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastBridgeBigKeyDoor_To_B1TopSide =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastBridgeBigKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastBridgeBigKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1TopSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastBridgeBigKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBridgeBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1TopSide_To_B1LobbyBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1LeftSidePastFirstKeyDoor_To_B1LobbyBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1TopSide_To_B1RightSideBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1LeftSidePastFirstKeyDoor_To_B1RightSideBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastEntranceGap_To_B1LeftSidePastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1LeftSidePastSecondKeyDoor_To_B1LeftSidePastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1LeftSidePastFirstKeyDoor_To_B1LeftSidePastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastFourTorchRoom_To_B1LeftSidePastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastFourTorchRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastFourTorchRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1LeftSidePastSecondKeyDoor_To_B1PastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1LeftSidePastSecondKeyDoor_To_F1PastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1LeftSidePastSecondKeyDoorTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastEntranceGap_To_B1PastPortalBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMPastEntranceGapTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMPortalBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1TopSide_To_B1PastBridgeBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1TopSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBridgeBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastBridgeBigKeyDoor_To_DarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMDarkRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastBridgeBigKeyDoorTest, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMDarkRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastBridgeBigKeyDoorTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMDarkRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastBridgeBigKeyDoorTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.MMDarkRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB1PastBridgeBigKeyDoorTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2PastWorthlessKeyDoor_To_DarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMDarkRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB2PastWorthlessKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMDarkRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB2PastWorthlessKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMDarkRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMB2PastWorthlessKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB2WorthlessKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkRoom_To_B2PastWorthlessKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMDarkRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMDarkRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMDarkRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB2WorthlessKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkRoom_To_B2PastCaneOfSomariaSwitch =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMDarkRoomTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMDarkRoomTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMDarkRoomTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossRoom_To_B2PastCaneOfSomariaSwitch =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMBossRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBossRoomBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2PastCaneOfSomariaSwitch_To_BossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMBossRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MMBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBossRoomBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
