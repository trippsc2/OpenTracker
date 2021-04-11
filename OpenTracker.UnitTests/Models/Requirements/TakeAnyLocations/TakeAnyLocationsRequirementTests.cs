using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.TakeAnyLocations
{
    public class TakeAnyLocationsRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new TakeAnyLocationsRequirement(_mode, true);
            _mode.TakeAnyLocations.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.TakeAnyLocations)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool takeAnyLocations, bool requirement)
        {
            _mode.TakeAnyLocations.Returns(takeAnyLocations);
            var sut = new TakeAnyLocationsRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ITakeAnyLocationsRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as TakeAnyLocationsRequirement);
        }
    }
}