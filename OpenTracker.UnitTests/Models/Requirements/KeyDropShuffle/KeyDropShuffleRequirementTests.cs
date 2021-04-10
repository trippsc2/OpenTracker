using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.KeyDropShuffle
{
    public class KeyDropShuffleRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new KeyDropShuffleRequirement(_mode, true);
            _mode.KeyDropShuffle.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool keyDropShuffle, bool requirement)
        {
            _mode.KeyDropShuffle.Returns(keyDropShuffle);
            var sut = new KeyDropShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IKeyDropShuffleRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as KeyDropShuffleRequirement);
        }
    }
}