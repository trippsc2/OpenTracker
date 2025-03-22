using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
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

        [Fact]
        public void Met_ShouldRaisePropertyChanged()
        {
            var sut = new EnemyShuffleRequirement(_mode, true);
            _mode.EnemyShuffle.Returns(true);

            Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
                () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.EnemyShuffle))));
        }

        [Fact]
        public void Met_ShouldRaiseChangePropagated()
        {
            var sut = new EnemyShuffleRequirement(_mode, true);
            _mode.EnemyShuffle.Returns(true);

            var eventRaised = false;

            void Handler(object? sender, EventArgs e)
            {
                eventRaised = true;
            }
            
            sut.ChangePropagated += Handler;
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.EnemyShuffle)));
            sut.ChangePropagated -= Handler;
            
            Assert.True(eventRaised);
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            var sut = new EnemyShuffleRequirement(_mode, true);
            _mode.EnemyShuffle.Returns(true);

            Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility),
                () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.EnemyShuffle))));
        }

        [Theory]
        [InlineData(AccessibilityLevel.Normal, false, false)]
        [InlineData(AccessibilityLevel.None, false, true)]
        [InlineData(AccessibilityLevel.Normal, true, true)]
        public void Accessibility_ShouldReturnExpectedValue(
            AccessibilityLevel expected, bool bigKeyShuffle, bool requirement)
        {
            _mode.EnemyShuffle.Returns(bigKeyShuffle);
            var sut = new EnemyShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Accessibility);
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