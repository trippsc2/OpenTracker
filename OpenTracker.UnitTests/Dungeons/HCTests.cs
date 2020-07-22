using OpenTracker.Models;
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
    public class HCTests
    {
        [Theory]
        [MemberData(nameof(StandardOpen_FrontEntryData))]
        [MemberData(nameof(Retro_FrontEntryData))]
        [MemberData(nameof(Inverted_FrontEntryData))]
        public void FrontEntryTests(
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
                RequirementNodeDictionary.Instance[RequirementNodeID.HCFrontEntry].Accessibility);
        }

        public static IEnumerable<object[]> StandardOpen_FrontEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    true,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Retro_FrontEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    true,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> Inverted_FrontEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    WorldState.Inverted,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(StandardOpen_SanctuaryEntryData))]
        [MemberData(nameof(Retro_SanctuaryEntryData))]
        [MemberData(nameof(Inverted_SanctuaryEntryData))]
        public void SanctuaryEntryTests(
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
                RequirementNodeDictionary.Instance[RequirementNodeID.HCSanctuaryEntry].Accessibility);
        }

        public static IEnumerable<object[]> StandardOpen_SanctuaryEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    true,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Retro_SanctuaryEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    true,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Inverted_SanctuaryEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    WorldState.Inverted,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(HC_StandardOpen_BackEntryData))]
        [MemberData(nameof(HC_Retro_BackEntryData))]
        [MemberData(nameof(HC_Inverted_BackEntryData))]
        public void BackEntryTests(
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
                RequirementNodeDictionary.Instance[RequirementNodeID.HCBackEntry].Accessibility);
        }

        public static IEnumerable<object[]> HC_StandardOpen_BackEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
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
                        (ItemType.Gloves, 0)
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
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HC_Retro_BackEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
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
                        (ItemType.Gloves, 0)
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
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HC_Inverted_BackEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
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
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 1)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(Entrance_StandardKeys_Basic_AccessibilityData))]
        [MemberData(nameof(Entrance_StandardKeys_Advanced_AccessibilityData))]
        [MemberData(nameof(Entrance_MapCompass_Basic_AccessibilityData))]
        [MemberData(nameof(Entrance_MapCompass_Advanced_AccessibilityData))]
        [MemberData(nameof(Entrance_MapCompassKey_Basic_AccessibilityData))]
        [MemberData(nameof(Entrance_MapCompassKey_Advanced_AccessibilityData))]
        [MemberData(nameof(StandardOpen_StandardKeys_Basic_AccessibilityData))]
        [MemberData(nameof(StandardOpen_StandardKeys_Advanced_AccessibilityData))]
        [MemberData(nameof(Inverted_StandardKeys_AccessibilityData))]
        [MemberData(nameof(Retro_StandardKeys_Basic_AccessibilityData))]
        [MemberData(nameof(Retro_StandardKeys_Advanced_AccessibilityData))]
        [MemberData(nameof(StandardOpen_MapCompass_Basic_AccessibilityData))]
        [MemberData(nameof(StandardOpen_MapCompass_Advanced_AccessibilityData))]
        [MemberData(nameof(Inverted_MapCompass_AccessibilityData))]
        [MemberData(nameof(Retro_MapCompass_Basic_AccessibilityData))]
        [MemberData(nameof(Retro_MapCompass_Advanced_AccessibilityData))]
        [MemberData(nameof(StandardOpen_MapCompassKey_Basic_AccessibilityData))]
        [MemberData(nameof(StandardOpen_MapCompassKey_Advanced_AccessibilityData))]
        [MemberData(nameof(Inverted_MapCompassKey_AccessibilityData))]
        public void AccessibilityTests(
            (ItemType, int)[] items, (SequenceBreakType, bool)[] sequenceBreaks,
            WorldState worldState, DungeonItemShuffle dungeonItemShuffle, ItemPlacement itemPlacement,
            bool entranceShuffle, AccessibilityLevel expectedAccessibility, int expectedAccessible)
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
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.EntranceShuffle = entranceShuffle;

            Assert.Equal(
                expectedAccessibility,
                LocationDictionary.Instance[LocationID.HyruleCastle].Sections[0].Accessibility);
            Assert.Equal(
                expectedAccessible,
                ((IItemSection)LocationDictionary.Instance[LocationID.HyruleCastle].Sections[0]).Accessible);
        }

        public static IEnumerable<object[]> Entrance_StandardKeys_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
            };

        public static IEnumerable<object[]> Entrance_StandardKeys_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Entrance_MapCompass_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Entrance_MapCompass_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Entrance_MapCompassKey_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> Entrance_MapCompassKey_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> StandardOpen_StandardKeys_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal,
                    6
                }
            };

        public static IEnumerable<object[]> StandardOpen_StandardKeys_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    6
                }
            };

        public static IEnumerable<object[]> Inverted_StandardKeys_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, false),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, false),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    6
                }
            };

        public static IEnumerable<object[]> Retro_StandardKeys_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Retro_StandardKeys_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> StandardOpen_MapCompass_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> StandardOpen_MapCompass_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Inverted_MapCompass_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, false),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, false),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Retro_MapCompass_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> Retro_MapCompass_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Retro,
                    DungeonItemShuffle.MapsCompasses,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> StandardOpen_MapCompassKey_Basic_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal,
                    8
                }
            };
        
        public static IEnumerable<object[]> StandardOpen_MapCompassKey_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.StandardOpen,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> Inverted_MapCompassKey_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, false),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, false),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Gloves, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true),
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    WorldState.Inverted,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal,
                    8
                }
            };
    }
}
