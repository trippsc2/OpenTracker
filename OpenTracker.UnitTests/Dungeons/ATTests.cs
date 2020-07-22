using OpenTracker.Models;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Sections;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Dungeons
{
    public class ATTests
    {
        [Theory]
        [MemberData(nameof(StandardOpen_EntryData))]
        [MemberData(nameof(Retro_EntryData))]
        [MemberData(nameof(Inverted_EntryData))]
        public void EntryTests(
            (ItemType, int)[] items, (SequenceBreakType, bool)[] sequenceBreaks,
            WorldState worldState, bool entranceShuffle, AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].SetCurrent(item.Item2);
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled = sequenceBreak.Item2;
            }

            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;

            Assert.Equal(
                expected,
                RequirementNodeDictionary.Instance[RequirementNodeID.ATEntry].Accessibility);
        }

        public static IEnumerable<object[]> StandardOpen_EntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Retro_EntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Inverted_EntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 0),
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Inverted,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 0),
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Cape, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Inverted,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Cape, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Inverted,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1),
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Cape, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Inverted,
                    true,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(AccessibilityData))]
        public void AccessibilityTests(
            (ItemType, int)[] items, (SequenceBreakType, bool)[] sequenceBreaks,
            WorldState worldState, AccessibilityLevel expectedAccessibility, int expectedAccessible)
        {
            ItemDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].SetCurrent(item.Item2);
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled = sequenceBreak.Item2;
            }

            Mode.Instance.WorldState = worldState;
            Mode.Instance.DungeonItemShuffle = DungeonItemShuffle.MapsCompassesSmallKeys;
            Mode.Instance.ItemPlacement = ItemPlacement.Advanced;
            Mode.Instance.EntranceShuffle = true;

            Assert.Equal(
                expectedAccessibility,
                LocationDictionary.Instance[LocationID.AgahnimTower].Sections[0].Accessibility);
            Assert.Equal(
                expectedAccessible,
                ((IItemSection)LocationDictionary.Instance[LocationID.AgahnimTower].Sections[0]).Accessible);
        }

        public static IEnumerable<object[]> AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    AccessibilityLevel.SequenceBreak,
                    2
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, false)
                    },
                    WorldState.StandardOpen,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    AccessibilityLevel.Normal,
                    2
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    AccessibilityLevel.SequenceBreak,
                    2
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    AccessibilityLevel.SequenceBreak,
                    2
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, false)
                    },
                    WorldState.Retro,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    AccessibilityLevel.Normal,
                    2
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    AccessibilityLevel.Normal,
                    2
                }
            };

        [Theory]
        [MemberData(nameof(StandardKeys_BossAccessibilityData))]
        public void BossRoomAccessibilityTests(
            (ItemType, int)[] items, (SequenceBreakType, bool)[] sequenceBreaks,
            WorldState worldState, DungeonItemShuffle dungeonItemShuffle,
            AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].SetCurrent(item.Item2);
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled = sequenceBreak.Item2;
            }

            Mode.Instance.WorldState = worldState;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.ItemPlacement = ItemPlacement.Advanced;
            Mode.Instance.EntranceShuffle = true;
            Mode.Instance.BossShuffle = true;

            BossPlacementDictionary.Instance[BossPlacementID.ATBoss].Boss = BossType.Test;

            Assert.Equal(
                expected,
                LocationDictionary.Instance[LocationID.AgahnimTower].Sections[1].Accessibility);
        }

        public static IEnumerable<object[]> StandardKeys_BossAccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 1),
                        (ItemType.SmallKey, 2),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 2),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 2),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 1),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 2),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 2),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 2),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 1),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 2),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.ATSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 2),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Sword, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.ATSmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    AccessibilityLevel.Normal
                }
            };
    }
}
