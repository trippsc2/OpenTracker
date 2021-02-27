using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    // [Collection("Tests")]
    // public class GTCrystalRequirementTests
    // {
    //     [Theory]
    //     [MemberData(nameof(GTCrystalRequirement_Data))]
    //     public void AccessibilityTests(int towerCrystals, int crystals, int redCrystals)
    //     {
    //         ItemDictionary.Instance.Reset();
    //         PrizeDictionary.Instance.Reset();
    //         ItemDictionary.Instance[ItemType.TowerCrystals].Current = towerCrystals;
    //         PrizeDictionary.Instance[PrizeType.Crystal].Current = crystals;
    //         PrizeDictionary.Instance[PrizeType.RedCrystal].Current = redCrystals;
    //
    //         AccessibilityLevel expected = towerCrystals + crystals + redCrystals >= 7 ?
    //             AccessibilityLevel.Normal : AccessibilityLevel.None;
    //
    //         Assert.Equal(
    //             expected, RequirementDictionary.Instance[RequirementType.GTCrystal].Accessibility);
    //     }
    //
    //     public static IEnumerable<object[]> GTCrystalRequirement_Data()
    //     {
    //         var result = new List<object[]>();
    //
    //         for (int i = 0; i < 8; i++)
    //         {
    //             for (int j = 0; j < 6; j++)
    //             {
    //                 for (int k = 0; k < 3; k++)
    //                 {
    //                     result.Add(new object[] { i, j, k });
    //                 }
    //             }
    //         }
    //
    //         return result;
    //     }
    // }
}
