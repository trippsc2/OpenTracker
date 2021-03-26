using Autofac;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    [Collection("Tests")]
    public class StaticRequirementTests
    {
        [Theory]
        [InlineData(RequirementType.NoRequirement, AccessibilityLevel.Normal)]
        [InlineData(RequirementType.Inspect, AccessibilityLevel.Inspect)]
        [InlineData(RequirementType.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        public void AccessibilityTests(RequirementType type, AccessibilityLevel expected)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var requirements = scope.Resolve<IRequirementDictionary>();
            
            Assert.Equal(expected, requirements[type].Accessibility);
        }
    }
}
