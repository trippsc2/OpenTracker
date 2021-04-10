using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.BigKeyShuffle
{
    public class BigKeyShuffleRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new BigKeyShuffleRequirement(_mode, true);
            _mode.BigKeyShuffle.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.BigKeyShuffle)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool bigKeyShuffle, bool requirement)
        {
            _mode.BigKeyShuffle.Returns(bigKeyShuffle);
            var sut = new BigKeyShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IBigKeyShuffleRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as BigKeyShuffleRequirement);
        }
    }
}