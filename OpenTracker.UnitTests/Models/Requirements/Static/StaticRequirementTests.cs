using Autofac;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements.Static;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Static
{
    public class StaticRequirementTests
    {
        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void Accessibility_ShouldAlwaysReturnExpected(
            AccessibilityLevel expected, AccessibilityLevel requirement)
        {
            var sut = new StaticRequirement(requirement);
            
            Assert.Equal(expected, sut.Accessibility);
        }

        [Fact]
        public void Met_ShouldAlwaysReturnTrue()
        {
            var sut = new StaticRequirement(AccessibilityLevel.None);
            
            Assert.True(sut.Met);
        }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IStaticRequirement.Factory>();
            var sut = factory(AccessibilityLevel.None);
            
            Assert.NotNull(sut as StaticRequirement);
        }
    }
}