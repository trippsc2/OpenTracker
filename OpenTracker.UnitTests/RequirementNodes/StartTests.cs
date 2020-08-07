using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class StartTests
    {
        [Theory]
        [MemberData(nameof(Start_Data))]
        public void AccessibilityTests(
            WorldState worldState, bool entranceShuffle, AccessibilityLevel expected)
        {
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;

            Assert.Equal(
                expected,
                RequirementNodeDictionary.Instance[RequirementNodeID.Start].Accessibility);
        }

        public static IEnumerable<object[]> Start_Data =>
            new List<object[]>
            {
                new object[]
                {
                    WorldState.StandardOpen,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.StandardOpen,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Retro,
                    true,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    AccessibilityLevel.Normal
                }
            };
    }
}
