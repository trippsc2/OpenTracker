using System.ComponentModel;
using Autofac;
using NSubstitute;
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
            var builder = ContainerConfig.GetContainerBuilder();

            builder.Register(_ => Substitute.For<IRequirementNode>());
            
            using var scope = builder.Build().BeginLifetimeScope();
            var mode = scope.Resolve<IMode>();
            var requirements = scope.Resolve<IRequirementDictionary>();
            var requirementNodes = scope.Resolve<IRequirementNodeDictionary>();
            
            mode.WorldState = WorldState.Inverted;

            var lightWorldNode = requirementNodes[RequirementNodeID.LightWorld];
            var lightWorldRequirement = requirements[RequirementType.LightWorld];
    
            Assert.Equal(AccessibilityLevel.None, lightWorldRequirement.Accessibility);

            lightWorldNode.Accessibility.Returns(AccessibilityLevel.Normal);
            lightWorldNode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                lightWorldNode, new PropertyChangedEventArgs(nameof(IRequirementNode.Accessibility)));
    
            Assert.Equal(AccessibilityLevel.Normal, lightWorldRequirement.Accessibility);
        }
    }
}
