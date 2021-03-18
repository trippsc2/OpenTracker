using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    public class LightWorldRequirementTests
    {
        [Fact]
        public void AccessibilityTests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var mode = scope.Resolve<IMode>();
            var requirements = scope.Resolve<IRequirementDictionary>();
            var requirementNodes = scope.Resolve<IRequirementNodeDictionary>();
            
            mode.WorldState = WorldState.Inverted;
            var lightWorldRequirement = requirements[RequirementType.LightWorld];
    
            Assert.Equal(AccessibilityLevel.None, lightWorldRequirement.Accessibility);
    
            // requirementNodes[RequirementNodeID.LightWorld].AlwaysAccessible = true;
    
            Assert.Equal(AccessibilityLevel.Normal, lightWorldRequirement.Accessibility);
        }
    }
}
