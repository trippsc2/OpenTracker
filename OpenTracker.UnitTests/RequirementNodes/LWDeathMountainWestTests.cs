using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class LWDeathMountainWestTests
    {
        [Theory]
        [MemberData(nameof(Start_To_DeathMountainWestBottom))]
        [MemberData(nameof(LightWorld_To_DeathMountainWestBottom))]
        [MemberData(nameof(DeathMountainEntry_To_DeathMountainWestBottom))]
        [MemberData(nameof(DeathMountainExit_To_DeathMountainWestBottom))]
        [MemberData(nameof(SpectacleRockTop_To_DeathMountainWestBottom))]
        [MemberData(nameof(DeathMountainWestTop_To_DeathMountainWestBottom))]
        [MemberData(nameof(DeathMountainEastBottom_To_DeathMountainWestBottom))]
        [MemberData(nameof(DarkDeathMountainWestBottom_To_DeathMountainWestBottom))]
        [MemberData(nameof(DeathMountainWestTop_To_SpectacleRockTop))]
        [MemberData(nameof(DarkDeathMountainWestBottom_To_SpectacleRockTop))]
        [MemberData(nameof(Start_To_DeathMountainWestTop))]
        [MemberData(nameof(SpectacleRockTop_To_DeathMountainWestTop))]
        [MemberData(nameof(DeathMountainEastTop_To_DeathMountainWestTop))]
        [MemberData(nameof(DarkDeathMountainTop_To_DeathMountainWestTop))]
        public void AccessibilityTests(
            RequirementNodeID id, ItemPlacement itemPlacement,
            DungeonItemShuffle dungeonItemShuffle, WorldState worldState,
            bool entranceShuffle, bool enemyShuffle, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, AccessibilityLevel expected)
        {
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
        }

        public static IEnumerable<object[]> Start_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestBottomAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainEntry_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEntryTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainExit_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 0),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainExitTest, 1),
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SpectacleRockTop_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainWestTop_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainEastBottom_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastBottomTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkDeathMountainWestBottom_To_DeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestBottom,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainWestTop_To_SpectacleRockTop =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkDeathMountainWestBottom_To_SpectacleRockTop =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.SpectacleRockTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainWestBottomTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DeathMountainWestTop =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SpectacleRockTop_To_DeathMountainWestTop =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SpectacleRockTopTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainEastTop_To_DeathMountainWestTop =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainEastTopTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkDeathMountainTop_To_DeathMountainWestTop =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainTopTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainTopTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainTopTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainTopTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainTopTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainTopTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DeathMountainWestTop,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkDeathMountainTopTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
