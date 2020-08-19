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
    public class SPNodeTests
    {
        [Theory]
        [MemberData(nameof(Entry_To_SP))]
        [MemberData(nameof(AfterRiver_To_SP))]
        [MemberData(nameof(SP_To_AfterRiver))]
        [MemberData(nameof(B1_To_AfterRiver))]
        [MemberData(nameof(AfterRiver_To_B1))]
        [MemberData(nameof(B1PastFirstRightKeyDoor_To_B1))]
        [MemberData(nameof(B1_To_B1PastFirstRightKeyDoor))]
        [MemberData(nameof(B1PastSecondRightKeyDoor_To_B1PastFirstRightKeyDoor))]
        [MemberData(nameof(B1PastFirstRightKeyDoor_To_B1PastSecondRightKeyDoor))]
        [MemberData(nameof(B1PastRightHammerBlocks_To_B1PastSecondRightKeyDoor))]
        [MemberData(nameof(B1PastSecondRightKeyDoor_To_B1PastRightHammerBlocks))]
        [MemberData(nameof(B1KeyLedge_To_B1PastRightHammerBlocks))]
        [MemberData(nameof(B1PastLeftKeyDoor_To_B1PastRightHammerBlocks))]
        [MemberData(nameof(B1PastRightHammerBlocks_To_B1KeyLedge))]
        [MemberData(nameof(B1PastRightHammerBlocks_To_B1PastLeftKeyDoor))]
        [MemberData(nameof(B1PastRightHammerBlocks_To_BigChest))]
        [MemberData(nameof(B1PastRightHammerBlocks_To_B1Back))]
        [MemberData(nameof(B1PastBackFirstKeyDoor_To_B1Back))]
        [MemberData(nameof(B1Back_To_B1PastBackFirstKeyDoor))]
        [MemberData(nameof(BossRoom_To_B1PastBackFirstKeyDoor))]
        [MemberData(nameof(B1PastBackFirstKeyDoor_To_BossRoom))]
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

            ((IDungeon)LocationDictionary.Instance[LocationID.SwampPalace]).DungeonDataQueue
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

        public static IEnumerable<object[]> Entry_To_SP =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AfterRiver_To_SP =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPAfterRiverTest, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPAfterRiverTest, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPAfterRiverTest, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SP_To_AfterRiver =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPTest, 0),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPTest, 1),
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPTest, 1),
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1_To_AfterRiver =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SP1FKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AfterRiver_To_B1 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPAfterRiverTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPAfterRiverTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPAfterRiverTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SP1FKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastFirstRightKeyDoor_To_B1 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastFirstRightKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastFirstRightKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastFirstRightKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1FirstRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1_To_B1PastFirstRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1FirstRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastSecondRightKeyDoor_To_B1PastFirstRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastSecondRightKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastSecondRightKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastSecondRightKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastFirstRightKeyDoor_To_B1PastSecondRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastFirstRightKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastFirstRightKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastFirstRightKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastRightHammerBlocks_To_B1PastSecondRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastSecondRightKeyDoor_To_B1PastRightHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastSecondRightKeyDoorTest, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastSecondRightKeyDoorTest, 1),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastSecondRightKeyDoorTest, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1KeyLedge_To_B1PastRightHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1KeyLedgeTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1KeyLedgeTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastLeftKeyDoor_To_B1PastRightHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastLeftKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastLeftKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastLeftKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1LeftKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastRightHammerBlocks_To_B1KeyLedge =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1KeyLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1KeyLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1KeyLedge,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastRightHammerBlocks_To_B1PastLeftKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1LeftKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastRightHammerBlocks_To_BigChest =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastRightHammerBlocks_To_B1Back =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastRightHammerBlocksTest, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastBackFirstKeyDoor_To_B1Back =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastBackFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastBackFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastBackFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1BackFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1Back_To_B1PastBackFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1BackTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1BackTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1BackTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1BackFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossRoom_To_B1PastBackFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPBossRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPBossRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastBackFirstKeyDoor_To_BossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastBackFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastBackFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SPB1PastBackFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPBossRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
