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
    public class HCNodeTests
    {
        [Theory]
        [MemberData(nameof(Sanctuary_AccessibilityData))]
        [MemberData(nameof(Front_AccessibilityData))]
        [MemberData(nameof(PastEscapeFirstKeyDoor_AccessibilityData))]
        [MemberData(nameof(PastEscapeSecondKeyDoor_AccessibilityData))]
        [MemberData(nameof(DarkRoomFront_AccessibilityData))]
        [MemberData(nameof(PastDarkCrossKeyDoor_AccessibilityData))]
        [MemberData(nameof(PastSewerRatRoomKeyDoor_AccessibilityData))]
        [MemberData(nameof(DarkRoomBack_AccessibilityData))]
        [MemberData(nameof(Back_AccessibilityData))]
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

            ((IDungeon)LocationDictionary.Instance[LocationID.HyruleCastle]).DungeonDataQueue
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

        public static IEnumerable<object[]> Sanctuary_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
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
                    DungeonNodeID.HCSanctuary,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
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
                    DungeonNodeID.HCSanctuary,
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
                    DungeonNodeID.HCSanctuary,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.SuperBunnyMirror
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.SuperBunnyMirror
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.SuperBunnyMirror
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor,
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.SuperBunnyMirror,
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor,
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCSanctuary,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
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
                    DungeonNodeID.HCSanctuary,
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

        public static IEnumerable<object[]> Front_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCFront,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCFront,
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
                    DungeonNodeID.HCFront,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCFront,
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
                    DungeonNodeID.HCFront,
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
                    DungeonNodeID.HCFront,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
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
                    DungeonNodeID.HCFront,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
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
                    DungeonNodeID.HCFront,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
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
                    DungeonNodeID.HCFront,
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

        public static IEnumerable<object[]> PastEscapeFirstKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
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
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCEscapeFirstKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastEscapeSecondKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCPastEscapeSecondKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCEscapeFirstKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCPastEscapeSecondKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCEscapeFirstKeyDoor,
                        KeyDoorID.HCEscapeSecondKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkRoomFront_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCDarkRoomFront,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomFront,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomFront,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomFront,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomFront,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastDarkCrossKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
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
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastSewerRatRoomKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor,
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkRoomBack_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCDarkRoomBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor,
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCDarkRoomBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomHC
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Back_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.HCBack,
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
                    DungeonNodeID.HCBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor,
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCBack,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCBack,
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
                    DungeonNodeID.HCBack,
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
                    DungeonNodeID.HCBack,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor,
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCBack,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCFront,
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
                    DungeonNodeID.HCBack,
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
                    DungeonNodeID.HCBack,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCBack,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.HCBack,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCBack,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.HCDarkCrossRoomKeyDoor,
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.HCBack,
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
    }
}
