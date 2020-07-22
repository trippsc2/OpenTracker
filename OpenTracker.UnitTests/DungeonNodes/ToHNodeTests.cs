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
    public class ToHNodeTests
    {
        [Theory]
        [MemberData(nameof(ToH_AccessibilityData))]
        [MemberData(nameof(PastKeyDoor_AccessibilityData))]
        [MemberData(nameof(BasementTorchRoom_AccessibilityData))]
        [MemberData(nameof(PastBigKeyDoor_AccessibilityData))]
        [MemberData(nameof(BigChest_AccessibilityData))]
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

            ((IDungeon)LocationDictionary.Instance[LocationID.TowerOfHera]).DungeonDataQueue
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

        public static IEnumerable<object[]> ToH_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ToH,
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
                    DungeonNodeID.ToH,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ToH,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.ToH,
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
                    DungeonNodeID.ToH,
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
                    DungeonNodeID.ToH,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ToH,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.ToH,
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
                    DungeonNodeID.ToH,
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
                    DungeonNodeID.ToH,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ToH,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Gloves, 2)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ToH,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ToH,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Gloves, 2),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.ToH,
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

        public static IEnumerable<object[]> PastKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ToHPastBigKeyDoor,
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
                    DungeonNodeID.ToHPastKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ToHKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BasementTorchRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ToHBasementTorchRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ToHKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ToHBasementTorchRoom,
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
                        KeyDoorID.ToHKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.ToHBasementTorchRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ToHKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastBigKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ToHPastBigKeyDoor,
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
                    DungeonNodeID.ToHPastBigKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ToHBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigChest_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.ToHBigChest,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ToHBigKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.ToHBigChest,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.ToHBigKeyDoor,
                        KeyDoorID.ToHBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
