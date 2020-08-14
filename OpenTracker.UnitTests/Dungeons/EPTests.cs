using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Sections;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Dungeons
{
    [Collection("Tests")]
    public class EPTests
    {
        [Theory]
        [MemberData(nameof(StandardKeys_AccessibilityData))]
        [MemberData(nameof(MapCompass_AccessibilityData))]
        [MemberData(nameof(Keysanity_AccessibilityData))]
        public void AccessibilityTests(
            (ItemType, int)[] items, (SequenceBreakType, bool)[] sequenceBreaks,
            DungeonItemShuffle dungeonItemShuffle, bool enemyShuffle,
            AccessibilityLevel expectedAccessibility, int expectedAccessible)
        {
            ItemDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled = sequenceBreak.Item2;
            }

            Mode.Instance.WorldState = WorldState.StandardOpen;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.ItemPlacement = ItemPlacement.Advanced;
            Mode.Instance.EntranceShuffle = true;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            Mode.Instance.BossShuffle = true;

            BossPlacementDictionary.Instance[BossPlacementID.EPBoss].Boss = BossType.Test;

            Assert.Equal(
                expectedAccessibility,
                LocationDictionary.Instance[LocationID.EasternPalace].Sections[0].Accessibility);
            Assert.Equal(
                expectedAccessible,
                ((IItemSection)LocationDictionary.Instance[LocationID.EasternPalace].Sections[0]).Accessible);
        }

        public static IEnumerable<object[]> StandardKeys_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    DungeonItemShuffle.Standard,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    DungeonItemShuffle.Standard,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    DungeonItemShuffle.Standard,
                    false,
                    AccessibilityLevel.Normal,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    DungeonItemShuffle.Standard,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    DungeonItemShuffle.Standard,
                    true,
                    AccessibilityLevel.Normal,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    DungeonItemShuffle.Standard,
                    true,
                    AccessibilityLevel.Normal,
                    3
                }
            };

        public static IEnumerable<object[]> MapCompass_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    false,
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    false,
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    false,
                    AccessibilityLevel.Normal,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    true,
                    AccessibilityLevel.Normal,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.MapsCompasses,
                    true,
                    AccessibilityLevel.Normal,
                    5
                }
            };

        public static IEnumerable<object[]> Keysanity_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    false,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.Partial,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.Partial,
                    5
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, false),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    true,
                    AccessibilityLevel.Normal,
                    6
                }
            };

        [Theory]
        [MemberData(nameof(StandardKeys_Basic_BossAccessibilityData))]
        [MemberData(nameof(StandardKeys_Advanced_BossAccessibilityData))]
        [MemberData(nameof(Keysanity_Basic_BossAccessibilityData))]
        [MemberData(nameof(Keysanity_Advanced_BossAccessibilityData))]
        public void BossAccessibilityTests(
            (ItemType, int)[] items, (SequenceBreakType, bool)[] sequenceBreaks,
            DungeonItemShuffle dungeonItemShuffle, ItemPlacement itemPlacement,
            bool enemyShuffle, AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled = sequenceBreak.Item2;
            }

            Mode.Instance.WorldState = WorldState.StandardOpen;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.EntranceShuffle = true;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            Mode.Instance.BossShuffle = true;

            BossPlacementDictionary.Instance[BossPlacementID.EPBoss].Boss = BossType.Test;

            Assert.Equal(
                expected,
                LocationDictionary.Instance[LocationID.EasternPalace].Sections[1].Accessibility);
        }

        public static IEnumerable<object[]> StandardKeys_Basic_BossAccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> StandardKeys_Advanced_BossAccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal // Should be SequenceBreak due to key logic.
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPRight, true),
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal // Should be SequenceBreak due to key logic.
                }
            };

        public static IEnumerable<object[]> Keysanity_Basic_BossAccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Basic,
                    true,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Keysanity_Advanced_BossAccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.Bow, 1),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, false)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.Bow, 0),
                        (ItemType.EPBigKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomEPBack, true)
                    },
                    DungeonItemShuffle.Keysanity,
                    ItemPlacement.Advanced,
                    true,
                    AccessibilityLevel.Normal
                }
            };
    }
}
