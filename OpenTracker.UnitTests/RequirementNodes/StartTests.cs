using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    [Collection("Tests")]
    public class StartTests
    {
        [Theory]
        [MemberData(nameof(Inaccessible))]
        [MemberData(nameof(Start))]
        [MemberData(nameof(EntranceDungeon))]
        [MemberData(nameof(NonEntrance))]
        [MemberData(nameof(NonEntranceInverted))]
        public void Tests(
            ModeSaveData mode, RequirementNodeID[] accessibleNodes, RequirementNodeID id,
            AccessibilityLevel expected)
        {
            Mode.Instance.Load(mode);
            RequirementNodeDictionary.Instance.Reset();

            foreach (var node in accessibleNodes)
            {
                RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
            }

            Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
        }

        public static IEnumerable<object[]> Inaccessible =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    RequirementNodeID.Inaccessible,
                    AccessibilityLevel.None
                }
            };

        public static IEnumerable<object[]> Start =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    RequirementNodeID.Start,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.EntranceDungeonAllInsanity,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.EntranceDungeonAllInsanity,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.EntranceDungeonAllInsanity,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> NonEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.EntranceNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.EntranceNone,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.EntranceNone,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> NonEntranceInverted =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.EntranceNone
                    },
                    RequirementNodeID.EntranceNoneInverted,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.EntranceNone
                    },
                    RequirementNodeID.EntranceNoneInverted,
                    AccessibilityLevel.Normal
                }
            };
    }
}
