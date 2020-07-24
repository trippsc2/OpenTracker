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
    public class ATNodeTests
    {
        [Theory]
        [MemberData(nameof(AT_AccessibilityData))]
        [MemberData(nameof(DarkMaze_AccessibilityData))]
        [MemberData(nameof(PastFirstKeyDoor_AccessibilityData))]
        [MemberData(nameof(PastSecondKeyDoor_AccessibilityData))]
        [MemberData(nameof(PastThirdKeyDoor_AccessibilityData))]
        [MemberData(nameof(PastFourthKeyDoor_AccessibilityData))]
        [MemberData(nameof(BossRoom_AccessibilityData))]
        public void AccessibilityTests(
            DungeonNodeID id, WorldState worldState, DungeonItemShuffle dungeonItemShuffle,
            ItemPlacement itemPlacement, bool entranceShuffle, (ItemType, int)[] items,
            SequenceBreakType[] sequenceBreaksDisabled, List<KeyDoorID> keyDoors,
            AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();

            Mode.Instance.WorldState = worldState;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.EntranceShuffle = entranceShuffle;

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].SetCurrent(item.Item2);
            }

            foreach (var sequenceBreak in sequenceBreaksDisabled)
            {
                SequenceBreakDictionary.Instance[sequenceBreak].Enabled = false;
            }

            ((IDungeon)LocationDictionary.Instance[LocationID.AgahnimTower]).DungeonDataQueue
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

        public static IEnumerable<object[]> AT_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flute, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.AT,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkMaze_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ATDarkMaze,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.ATDarkMaze,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomAT
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ATDarkMaze,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomAT
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastFirstKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ATPastFirstKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
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
                    DungeonNodeID.ATPastFirstKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastSecondKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ATPastSecondKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ATPastSecondKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastThirdKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ATPastThirdKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ATPastThirdKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor,
                        KeyDoorID.ATThirdKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastFourthKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ATPastFourthKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor,
                        KeyDoorID.ATThirdKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ATPastFourthKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor,
                        KeyDoorID.ATThirdKeyDoor,
                        KeyDoorID.ATFourthKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ATBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor,
                        KeyDoorID.ATThirdKeyDoor,
                        KeyDoorID.ATFourthKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ATBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 2)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor,
                        KeyDoorID.ATThirdKeyDoor,
                        KeyDoorID.ATFourthKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.ATBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 0)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ATFirstKeyDoor,
                        KeyDoorID.ATSecondKeyDoor,
                        KeyDoorID.ATThirdKeyDoor,
                        KeyDoorID.ATFourthKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
