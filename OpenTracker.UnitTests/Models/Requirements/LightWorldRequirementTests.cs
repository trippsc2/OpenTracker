using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    // [Collection("Tests")]
    // public class LightWorldRequirementTests
    // {
    //     [Fact]
    //     public void AccessibilityTests()
    //     {
    //         Mode.Instance.WorldState = WorldState.Inverted;
    //         RequirementNodeDictionary.Instance.Reset();
    //         var lightWorldRequirement = RequirementDictionary.Instance[RequirementType.LightWorld];
    //
    //         Assert.Equal(AccessibilityLevel.None, lightWorldRequirement.Accessibility);
    //
    //         RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld].AlwaysAccessible = true;
    //
    //         Assert.Equal(AccessibilityLevel.Normal, lightWorldRequirement.Accessibility);
    //     }
    // }
}
