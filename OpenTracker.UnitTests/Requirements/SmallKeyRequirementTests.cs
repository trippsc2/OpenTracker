using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class SmallKeyRequirementTests
    {
        [Theory]
        [MemberData(nameof(SmallKey_Data))]
        public void AccessibilityTests(
            WorldState worldState, (ItemType, int)[] items, RequirementType type,
            AccessibilityLevel expected)
        {
            Mode.Instance.WorldState = worldState;
            ItemDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<(ItemType, int, RequirementType)> SmallKey_InputData =>
            new List<(ItemType, int, RequirementType)>
            {
                (ItemType.TRSmallKey, 2, RequirementType.TRSmallKey2)
            };

        public static IEnumerable<object[]> SmallKey_Data()
        {
            var result = new List<object[]>();

            foreach (var input in SmallKey_InputData)
            {
                result.AddRange(GetSmallKeyData(input.Item1, input.Item2, input.Item3));
            }

            return result;
        }

        public static IEnumerable<object[]> GetSmallKeyData(
            ItemType item, int required, RequirementType type)
        {
            var result = new List<object[]>();
            int maximum = ItemDictionary.Instance[item].Maximum;

            foreach (WorldState worldState in Enum.GetValues(typeof(WorldState)))
            {
                result.Add(new object[]
                {
                    worldState,
                    new (ItemType, int)[0],
                    type,
                    AccessibilityLevel.None
                });

                for (int dungeonKeys = 0; dungeonKeys <= maximum; dungeonKeys++)
                {
                    for (int genericKeys = 0; genericKeys <= maximum; genericKeys++)
                    {
                        result.Add(new object[]
                        {
                            worldState,
                            new (ItemType, int)[]
                            {
                                (item, dungeonKeys),
                                (ItemType.SmallKey, genericKeys)
                            },
                            type,
                            GetExpected(worldState, dungeonKeys, genericKeys, required)
                        });
                    }
                }
            }

            return result;
        }

        public static AccessibilityLevel GetExpected(
            WorldState worldState, int dungeonKeys, int genericKeys, int required)
        {
            return dungeonKeys + (worldState == WorldState.Retro ? genericKeys : 0) >= required ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
