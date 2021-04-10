using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.SmallKeyShuffle
{
    public class SmallKeyShuffleRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new SmallKeyShuffleRequirement(_mode, true);
            _mode.SmallKeyShuffle.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool smallKeyShuffle, bool requirement)
        {
            _mode.SmallKeyShuffle.Returns(smallKeyShuffle);
            var sut = new SmallKeyShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ISmallKeyShuffleRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as SmallKeyShuffleRequirement);
        }
    }
}