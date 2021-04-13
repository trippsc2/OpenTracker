using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.CompassShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.CompassShuffle
{
    public class CompassShuffleRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new CompassShuffleRequirement(_mode, true);
            _mode.CompassShuffle.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool bossShuffle, bool requirement)
        {
            _mode.CompassShuffle.Returns(bossShuffle);
            var sut = new CompassShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ICompassShuffleRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as CompassShuffleRequirement);
        }
    }
}