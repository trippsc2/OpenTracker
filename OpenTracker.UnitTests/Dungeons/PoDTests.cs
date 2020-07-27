using OpenTracker.Models;
using OpenTracker.Models.AccessibilityLevels;
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
    public class PoDTests
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
                RequirementNodeDictionary.Instance[RequirementNodeID.PoDEntry].Accessibility);
        }

        public static IEnumerable<object[]> StandardOpen_EntryData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0)
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
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1)
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
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0)
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
                        (ItemType.Aga1, 0),
                        (ItemType.MoonPearl, 0)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 0)
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
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1)
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
                        (ItemType.Flippers, 0)
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
                        (ItemType.Flippers, 0)
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
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };

        [Theory]
        [MemberData(nameof(StandardKeys_Advanced_AccessibilityData))]
        public void AccessibilityTests(
            (ItemType, int)[] items, (SequenceBreakType, bool)[] sequenceBreaks,
            DungeonItemShuffle dungeonItemShuffle, ItemPlacement itemPlacement, bool enemyShuffle,
            AccessibilityLevel expectedAccessibility, int expectedAccessible)
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

            Mode.Instance.WorldState = WorldState.StandardOpen;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.EntranceShuffle = true;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            Mode.Instance.BossShuffle = true;

            BossPlacementDictionary.Instance[BossPlacementID.PoDBoss].Boss = BossType.Test;

            Assert.Equal(
                expectedAccessibility,
                LocationDictionary.Instance[LocationID.PalaceOfDarkness].Sections[0].Accessibility);
            Assert.Equal(
                expectedAccessible,
                ((IItemSection)LocationDictionary.Instance[LocationID.PalaceOfDarkness].Sections[0]).Accessible);
        }

        public static IEnumerable<object[]> StandardKeys_Advanced_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true),
                        (SequenceBreakType.CameraUnlock, true),
                        (SequenceBreakType.BombJumpPoDHammerJump, true),
                        (SequenceBreakType.DarkRoomPoDDarkBasement, true),
                        (SequenceBreakType.DarkRoomPoDDarkMaze, true),
                        (SequenceBreakType.DarkRoomPoDBossArea, true)
                    },
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    AccessibilityLevel.SequenceBreak,
                    5
                },
            };
    }
}
