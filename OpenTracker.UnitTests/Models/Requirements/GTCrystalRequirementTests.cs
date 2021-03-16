using System.Collections.Generic;
using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    public class GTCrystalRequirementTests
    {
        [Theory]
        [MemberData(nameof(GTCrystalRequirement_Data))]
        public void AccessibilityTests(int towerCrystals, int crystals, int redCrystals, bool towerCrystalsKnown)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var items = scope.Resolve<IItemDictionary>();
            var prizes = scope.Resolve<IPrizeDictionary>();
            var requirements = scope.Resolve<IRequirementDictionary>();

            var towerCrystalsItem = (ICrystalRequirementItem)items[ItemType.TowerCrystals];
            
            towerCrystalsItem.Current = towerCrystals;
            towerCrystalsItem.Known = towerCrystalsKnown;
            prizes[PrizeType.Crystal].Current = crystals;
            prizes[PrizeType.RedCrystal].Current = redCrystals;

            AccessibilityLevel expected;

            if (crystals + redCrystals >= 7)
            {
                expected = AccessibilityLevel.Normal;
            }
            else if (towerCrystalsKnown)
            {
                expected = towerCrystals + crystals + redCrystals >= 7 ?
                    AccessibilityLevel.Normal : AccessibilityLevel.None;                
            }
            else
            {
                expected = AccessibilityLevel.SequenceBreak;
            }
    
            Assert.Equal(
                expected, requirements[RequirementType.GTCrystal].Accessibility);
        }
    
        public static IEnumerable<object[]> GTCrystalRequirement_Data()
        {
            var result = new List<object[]>();
    
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    for (var k = 0; k < 3; k++)
                    {
                        for (var l = 0; l < 2; l++)
                        {
                            result.Add(new object[] { i, j, k, l == 1 });
                        }
                    }
                }
            }
    
            return result;
        }
    }
}
