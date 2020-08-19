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
    public class TRNodeTests
    {
        [Theory]
        [MemberData(nameof(Entry_To_Front))]
        [MemberData(nameof(F1CompassChestArea_To_Front))]
        [MemberData(nameof(F1FourTorchRoom_To_Front))]
        [MemberData(nameof(F1FirstKeyDoorArea_To_Front))]
        [MemberData(nameof(Front_To_F1CompassChestArea))]
        [MemberData(nameof(F1FourTorchRoom_To_F1CompassChestArea))]
        [MemberData(nameof(F1FirstKeyDoorArea_To_F1CompassChestArea))]
        [MemberData(nameof(Front_To_F1FourTorchRoom))]
        [MemberData(nameof(F1CompassChestArea_To_F1FourTorchRoom))]
        [MemberData(nameof(F1RollerRoom_To_F1FourTorchRoom))]
        [MemberData(nameof(F1FirstKeyDoorArea_To_F1FourTorchRoom))]
        [MemberData(nameof(F1FourTorchRoom_To_F1RollerRoom))]
        [MemberData(nameof(Front_To_F1FirstKeyDoorArea))]
        [MemberData(nameof(F1CompassChestArea_To_F1FirstKeyDoorArea))]
        [MemberData(nameof(F1FourTorchRoom_To_F1FirstKeyDoorArea))]
        [MemberData(nameof(F1PastFirstKeyDoor_To_F1FirstKeyDoorArea))]
        [MemberData(nameof(F1FirstKeyDoorArea_To_F1PastFirstKeyDoor))]
        [MemberData(nameof(F1PastSecondKeyDoor_To_F1PastFirstKeyDoor))]
        [MemberData(nameof(F1PastFirstKeyDoor_To_F1PastSecondKeyDoor))]
        [MemberData(nameof(B1_To_F1PastSecondKeyDoor))]
        [MemberData(nameof(Entry_To_B1))]
        [MemberData(nameof(F1PastSecondKeyDoor_To_B1))]
        [MemberData(nameof(B1PastBigKeyChestKeyDoor_To_B1))]
        [MemberData(nameof(B1BigChestArea_To_B1))]
        [MemberData(nameof(B1RightSide_To_B1))]
        [MemberData(nameof(Entry_To_B1MiddleRightEntranceArea))]
        [MemberData(nameof(B1_To_B1MiddleRightEntranceArea))]
        [MemberData(nameof(B1MiddleRightEntranceArea_To_B1BigChestArea))]
        [MemberData(nameof(B1BigChestArea_To_BigChest))]
        [MemberData(nameof(B1_To_B1RightSide))]
        [MemberData(nameof(PastB1toB2KeyDoor_To_B1RightSide))]
        [MemberData(nameof(B1RightSide_To_PastB1toB2KeyDoor))]
        [MemberData(nameof(B2DarkRoomTop_To_PastB1toB2KeyDoor))]
        [MemberData(nameof(PastB1toB2KeyDoor_To_B2DarkRoomTop))]
        [MemberData(nameof(B2DarkRoomBottom_To_B2DarkRoomTop))]
        [MemberData(nameof(B2DarkRoomTop_To_B2DarkRoomBottom))]
        [MemberData(nameof(B2PastDarkMaze_To_B2DarkRoomBottom))]
        [MemberData(nameof(B2DarkRoomBottom_To_B2PastDarkMaze))]
        [MemberData(nameof(B2PastKeyDoor_To_B2PastDarkMaze))]
        [MemberData(nameof(B2PastDarkMaze_To_LaserBridgeChests))]
        [MemberData(nameof(B2PastDarkMaze_To_B2PastKeyDoor))]
        [MemberData(nameof(B3_To_B2PastKeyDoor))]
        [MemberData(nameof(B2PastKeyDoor_To_B3))]
        [MemberData(nameof(B3BossRoomEntry_To_B3))]
        [MemberData(nameof(B3_To_B3BossRoomEntry))]
        [MemberData(nameof(BossRoom_To_B3BossRoomEntry))]
        [MemberData(nameof(B3BossRoomEntry_To_BossRoom))]
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

            ((IDungeon)LocationDictionary.Instance[LocationID.TurtleRock]).DungeonDataQueue
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

        public static IEnumerable<object[]> Entry_To_Front =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1CompassChestArea_To_Front =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FourTorchRoom_To_Front =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FirstKeyDoorArea_To_Front =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRFront,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Front_To_F1CompassChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FourTorchRoom_To_F1CompassChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FirstKeyDoorArea_To_F1CompassChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1CompassChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Front_To_F1FourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1CompassChestArea_To_F1FourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1RollerRoom_To_F1FourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1RollerRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1RollerRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FirstKeyDoorArea_To_F1FourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FourTorchRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FourTorchRoom_To_F1RollerRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1RollerRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1RollerRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1RollerRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Front_To_F1FirstKeyDoorArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1CompassChestArea_To_F1FirstKeyDoorArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1CompassChestAreaTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FourTorchRoom_To_F1FirstKeyDoorArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FourTorchRoomTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1PastFirstKeyDoor_To_F1FirstKeyDoorArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1FirstKeyDoorArea_To_F1PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1FirstKeyDoorAreaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1PastSecondKeyDoor_To_F1PastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastSecondKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastSecondKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastSecondKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1PastFirstKeyDoor_To_F1PastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastFirstKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastFirstKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1_To_F1PastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Entry_To_B1 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> F1PastSecondKeyDoor_To_B1 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastSecondKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastSecondKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRF1PastSecondKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TR1FThirdKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastBigKeyChestKeyDoor_To_B1 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1PastBigKeyChestKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1PastBigKeyChestKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1PastBigKeyChestKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1BigKeyChestKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1BigChestArea_To_B1 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1BigChestAreaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1BigChestAreaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1RightSide_To_B1 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1RightSideTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1RightSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1RightSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1BigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Entry_To_B1MiddleRightEntranceArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRMiddleEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1_To_B1MiddleRightEntranceArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1MiddleRightEntranceArea_To_B1BigChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1BigChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1MiddleRightEntranceAreaTest, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1BigChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1MiddleRightEntranceAreaTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1BigChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1MiddleRightEntranceAreaTest, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRB1BigChestArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1MiddleRightEntranceAreaTest, 1),
                        (ItemType.Hookshot, 0),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1BigChestArea_To_BigChest =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1BigChestAreaTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1BigChestAreaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRBigChest,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1BigChestAreaTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1_To_B1RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1RightSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1RightSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1RightSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1BigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastB1toB2KeyDoor_To_B1RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB1RightSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRPastB1toB2KeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB1RightSide,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRPastB1toB2KeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1RightSide_To_PastB1toB2KeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1RightSideTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1RightSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB1RightSideTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB1toB2KeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2DarkRoomTop_To_PastB1toB2KeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRPastB1toB2KeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastB1toB2KeyDoor_To_B2DarkRoomTop =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRPastB1toB2KeyDoorTest, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRPastB1toB2KeyDoorTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRPastB1toB2KeyDoorTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRPastB1toB2KeyDoorTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2DarkRoomBottom_To_B2DarkRoomTop =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomBottomTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomBottomTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomBottomTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2DarkRoomTop_To_B2DarkRoomBottom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomTopTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomTopTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomTopTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2PastDarkMaze_To_B2DarkRoomBottom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.TRB2DarkRoomBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomTR, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2DarkRoomBottom_To_B2PastDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2PastDarkMaze,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomBottomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2PastDarkMaze,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2DarkRoomBottomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2PastKeyDoor_To_B2PastDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2PastDarkMaze,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2PastDarkMaze,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2PastDarkMaze,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB2KeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2PastDarkMaze_To_LaserBridgeChests =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Shield, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Shield, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Shield, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, false)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Shield, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Shield, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Cape, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Shield, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Shield, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.TRLaserBridgeChests,
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Shield, 3)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TRLaserSkip, true)
                    },
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2PastDarkMaze_To_B2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2PastKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2PastKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2PastKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastDarkMazeTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRB2KeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B3_To_B2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB2PastKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3Test, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB2PastKeyDoor,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3Test, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B2PastKeyDoor_To_B3 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB3,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastKeyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB3,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB2PastKeyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B3BossRoomEntry_To_B3 =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB3,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3BossRoomEntryTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB3,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3BossRoomEntryTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB3,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3BossRoomEntryTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B3_To_B3BossRoomEntry =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB3BossRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3Test, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB3BossRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3Test, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB3BossRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3Test, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossRoom_To_B3BossRoomEntry =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRB3BossRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRBossRoomTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB3BossRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRB3BossRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRBossRoomTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRBossRoomBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B3BossRoomEntry_To_BossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.TRBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3BossRoomEntryTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3BossRoomEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.TRBossRoom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRB3BossRoomEntryTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new KeyDoorID[]
                    {
                        KeyDoorID.TRBossRoomBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
