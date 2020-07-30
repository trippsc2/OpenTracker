using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class GTCrystalRequirementTests
    {
        [Theory]
        [MemberData(nameof(GTCrystalRequirement_Data))]
        public void AccessibilityTests(int towerCrystals, int crystals, int redCrystals)
        {
            ItemDictionary.Instance.Reset();
            ItemDictionary.Instance[ItemType.TowerCrystals].Current = towerCrystals;
            ItemDictionary.Instance[ItemType.Crystal].Current = crystals;
            ItemDictionary.Instance[ItemType.RedCrystal].Current = redCrystals;

            AccessibilityLevel expected = towerCrystals + crystals + redCrystals >= 7 ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;

            Assert.Equal(
                expected, RequirementDictionary.Instance[RequirementType.GTCrystal].Accessibility);
        }

        public static IEnumerable<object[]> GTCrystalRequirement_Data()
        {
            var result = new List<object[]>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        result.Add(new object[] { i, j, k });
                    }
                }
            }

            return result;
        }
    }
}
