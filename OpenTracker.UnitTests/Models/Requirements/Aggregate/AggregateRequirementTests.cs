using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Aggregate
{
    public class AggregateRequirementTests
    {
        private readonly List<IRequirement> _requirements = new()
        {
            Substitute.For<IRequirement>(),
            Substitute.For<IRequirement>()
        };

        private readonly AggregateRequirement _sut;

        public AggregateRequirementTests()
        {
            _sut = new AggregateRequirement(_requirements);
        }

        [Fact]
        public void RequirementChanged_ShouldUpdateAccessibility()
        {
            const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
            _requirements[0].Accessibility.Returns(accessibility);
            _requirements[1].Accessibility.Returns(accessibility);

            _requirements[1].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirements[1], new PropertyChangedEventArgs(nameof(IRequirement.Accessibility)));
            
            Assert.Equal(accessibility, _sut.Accessibility);
        }
        
        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.Normal, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void Accessibility_ShouldEqualExpected(
            AccessibilityLevel expected, AccessibilityLevel requirement1, AccessibilityLevel requirement2)
        {
            _requirements[0].Accessibility.Returns(requirement1);
            _requirements[1].Accessibility.Returns(requirement2);

            _requirements[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirements[0], new PropertyChangedEventArgs(nameof(IRequirement.Accessibility)));
            
            Assert.Equal(expected, _sut.Accessibility);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IAggregateRequirement.Factory>();
            var sut = factory(_requirements);
            
            Assert.NotNull(sut as AggregateRequirement);
        }
    }
}