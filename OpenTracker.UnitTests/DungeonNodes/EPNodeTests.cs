using OpenTracker.Models;
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
    public class EPNodeTests
    {
        [Theory]
        [MemberData(nameof(EP_AccessibilityData))]
        [MemberData(nameof(BigChest_AccessibilityData))]
        [MemberData(nameof(RightDarkRoom_AccessibilityData))]
        [MemberData(nameof(PastRightKeyDoor_AccessibilityData))]
        [MemberData(nameof(PastBigKeyDoor_AccessibilityData))]
        [MemberData(nameof(BackDarkRoom_AccessibilityData))]
        [MemberData(nameof(PastBackKeyDoor_AccessibilityData))]
        [MemberData(nameof(BossRoom_AccessibilityData))]
        public void AccessibilityTests(
            DungeonNodeID id, WorldState worldState, DungeonItemShuffle dungeonItemShuffle,
            ItemPlacement itemPlacement, bool entranceShuffle, bool enemyShuffle,
            (ItemType, int)[] items, SequenceBreakType[] sequenceBreaksDisabled,
            List<KeyDoorID> keyDoors, AccessibilityLevel expected)
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

            ((IDungeon)LocationDictionary.Instance[LocationID.EasternPalace]).DungeonDataQueue
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

        public static IEnumerable<object[]> EP_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EP,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.EP,
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
                    DungeonNodeID.EP,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.EP,
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
                    DungeonNodeID.EP,
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
                    DungeonNodeID.EP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.EP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DungeonRevive
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.EP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.EP,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigChest_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EPBigChest,
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
                    DungeonNodeID.EPBigChest,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> RightDarkRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EPRightDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.EPRightDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomEPRight
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.EPRightDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomEPRight
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastRightKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EPPastRightKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.EPPastRightKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPRightKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastBigKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EPPastBigKeyDoor,
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
                    DungeonNodeID.EPPastBigKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BackDarkRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EPBackDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.EPBackDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomEPBack
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.EPBackDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomEPBack
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.EPBackDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomEPBack
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.EPBackDarkRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomEPBack
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastBackKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EPPastBackKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.EPPastBackKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor,
                        KeyDoorID.EPBackKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.EPBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor,
                        KeyDoorID.EPBackKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.EPBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor,
                        KeyDoorID.EPBackKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.EPBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.EPBigKeyDoor,
                        KeyDoorID.EPBackKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
