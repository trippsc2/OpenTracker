using OpenTracker.Models.AccessibilityLevels;
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
    public class HCTests
    {
        [Theory]
        [MemberData(nameof(Advanced_Standard_StandardOpen))]
        [MemberData(nameof(Advanced_Standard_Retro))]
        [MemberData(nameof(Advanced_Standard_Inverted))]
        [MemberData(nameof(Advanced_MapsCompasses_StandardOpen))]
        [MemberData(nameof(Advanced_MapsCompasses_Retro))]
        [MemberData(nameof(Advanced_MapsCompasses_Inverted))]
        [MemberData(nameof(Advanced_MapsCompassesSmallKeys_StandardOpen))]
        [MemberData(nameof(Advanced_MapsCompassesSmallKeys_Inverted))]
        [MemberData(nameof(Basic_Standard_StandardOpen))]
        [MemberData(nameof(Basic_Standard_Retro))]
        [MemberData(nameof(Basic_MapsCompasses_StandardOpen))]
        public void AccessibilityTests(
            ItemPlacement itemPlacement, DungeonItemShuffle dungeonItemShuffle,
            WorldState worldState, bool entranceShuffle, bool enemyShuffle,
            bool guaranteedBossItems, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, AccessibilityLevel expectedAccessibility,
            int expectedAccessible)
        {
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            Mode.Instance.GuaranteedBossItems = guaranteedBossItems;

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled = sequenceBreak.Item2;
            }

            Assert.Equal(
                expectedAccessibility,
                LocationDictionary.Instance[LocationID.HyruleCastle].Sections[0].Accessibility);
            Assert.Equal(
                expectedAccessible,
                ((IItemSection)LocationDictionary.Instance[LocationID.HyruleCastle].Sections[0]).Accessible);
        }

        public static IEnumerable<object[]> Advanced_Standard_StandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    6
                }
            };

        public static IEnumerable<object[]> Advanced_Standard_Retro =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Advanced_Standard_Inverted =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    6
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    6
                }
            };

        public static IEnumerable<object[]> Advanced_MapsCompasses_StandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Advanced_MapsCompasses_Retro =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> Advanced_MapsCompasses_Inverted =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Advanced_MapsCompassesSmallKeys_StandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> Advanced_MapsCompassesSmallKeys_Inverted =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    AccessibilityLevel.None,
                    0
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, false),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    1
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    3
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Partial,
                    7
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                },
                new object[]
                {
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true),
                        (SequenceBreakType.DungeonRevive, true),
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal,
                    8
                }
            };

        public static IEnumerable<object[]> Basic_Standard_StandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    6
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    6
                }
            };

        public static IEnumerable<object[]> Basic_Standard_Retro =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.HCSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                }
            };

        public static IEnumerable<object[]> Basic_MapsCompasses_StandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.Partial,
                    4
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 0),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    AccessibilityLevel.SequenceBreak,
                    7
                },
                new object[]
                {
                    ItemPlacement.Basic,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.HCBackEntryTest, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    AccessibilityLevel.Normal,
                    7
                }
            };
    }
}
