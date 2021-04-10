using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.EnemyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.EnemyShuffle
{
    public class EnemyShuffleRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new EnemyShuffleRequirement(_mode, true);
            _mode.EnemyShuffle.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.EnemyShuffle)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool enemyShuffle, bool requirement)
        {
            _mode.EnemyShuffle.Returns(enemyShuffle);
            var sut = new EnemyShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IEnemyShuffleRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as EnemyShuffleRequirement);
        }
    }
}