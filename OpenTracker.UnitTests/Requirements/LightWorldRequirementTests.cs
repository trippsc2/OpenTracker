using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class LightWorldRequirementTests
    {
        [Theory]
        [MemberData(nameof(LightWorld_Data))]
        public void AccessibilityTests(
            WorldState worldState, bool entranceShuffle, IDictionary<ItemType, int> items)
        {
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Key].Current = item.Value;
            }

            Assert.Equal(
                RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld].Accessibility,
                RequirementDictionary.Instance[RequirementType.LightWorld].Accessibility);
        }

        public static IEnumerable<object[]> LightWorld_Data =>
            new List<object[]>
            {
                new object[]
                {
                    WorldState.StandardOpen,
                    false,
                    new Dictionary<ItemType, int>(0)
                },
                new object[]
                {
                    WorldState.Retro,
                    false,
                    new Dictionary<ItemType, int>(0)
                },
                new object[]
                {
                    WorldState.Inverted,
                    false,
                    new Dictionary<ItemType, int>(0)
                },
                new object[]
                {
                    WorldState.Inverted,
                    false,
                    new Dictionary<ItemType, int>
                    {
                        { ItemType.Aga1, 1 }
                    }
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    new Dictionary<ItemType, int>(0)
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    new Dictionary<ItemType, int>
                    {
                        { ItemType.Aga1, 1 }
                    }
                },
                new object[]
                {
                    WorldState.Inverted,
                    true,
                    new Dictionary<ItemType, int>
                    {
                        { ItemType.LightWorldAccess, 1 }
                    }
                }
            };
    }
}
