using OpenTracker.Models;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.DungeonNodes
{
    public class SPNodeTests
    {
        [Theory]
        [MemberData(nameof(SP_AccessibilityData))]
        [MemberData(nameof(AfterRiver_AccessibilityData))]
        [MemberData(nameof(B1_AccessibilityData))]
        [MemberData(nameof(B1PastFirstRightKeyDoor_AccessibilityData))]
        [MemberData(nameof(B1PastSecondRightKeyDoor_AccessibilityData))]
        [MemberData(nameof(B1PastRightHammerBlocks_AccessibilityData))]
        [MemberData(nameof(B1KeyLedge_AccessibilityData))]
        [MemberData(nameof(B1PastLeftKeyDoor_AccessibilityData))]
        [MemberData(nameof(BigChest_AccessibilityData))]
        [MemberData(nameof(B1Back_AccessibilityData))]
        [MemberData(nameof(B1PastBackFirstKeyDoor_AccessibilityData))]
        [MemberData(nameof(BossRoom_AccessibilityData))]
        public void AccessibilityTests(
            DungeonNodeID id, WorldState worldState, DungeonItemShuffle dungeonItemShuffle,
            ItemPlacement itemPlacement, bool entranceShuffle, bool enemyShuffle,
            (ItemType, int)[] items, SequenceBreakType[] sequenceBreaksDisabled, List<KeyDoorID> keyDoors,
            AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();

            Mode.Instance.WorldState = worldState;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.EntranceShuffle = entranceShuffle;
            Mode.Instance.EnemyShuffle = enemyShuffle;

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].SetCurrent(item.Item2);
            }

            foreach (var sequenceBreak in sequenceBreaksDisabled)
            {
                SequenceBreakDictionary.Instance[sequenceBreak].Enabled = false;
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

        public static IEnumerable<object[]> SP_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.SP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> AfterRiver_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPAfterRiver,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastFirstRightKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastSecondRightKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastRightHammerBlocks_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1KeyLedge_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1KeyLedge,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1KeyLedge,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastLeftKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor,
                        KeyDoorID.SPB1LeftKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigChest_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPBigChest,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPBigChest,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor,
                        KeyDoorID.SPBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1Back_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1Back,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> B1PastBackFirstKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor,
                        KeyDoorID.SPB1BackFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.SPBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor,
                        KeyDoorID.SPB1BackFirstKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.SPBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.SP1FKeyDoor,
                        KeyDoorID.SPB1FirstRightKeyDoor,
                        KeyDoorID.SPB1SecondRightKeyDoor,
                        KeyDoorID.SPB1BackFirstKeyDoor,
                        KeyDoorID.SPBossRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
