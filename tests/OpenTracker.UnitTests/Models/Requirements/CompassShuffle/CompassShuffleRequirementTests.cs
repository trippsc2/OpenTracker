using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
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

        [Fact]
        public void Met_ShouldRaisePropertyChanged()
        {
            var sut = new CompassShuffleRequirement(_mode, true);
            _mode.CompassShuffle.Returns(true);

            Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
                () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle))));
        }

        [Fact]
        public void Met_ShouldRaiseChangePropagated()
        {
            var sut = new CompassShuffleRequirement(_mode, true);
            _mode.CompassShuffle.Returns(true);

            var eventRaised = false;

            void Handler(object? sender, EventArgs e)
            {
                eventRaised = true;
            }
            
            sut.ChangePropagated += Handler;
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle)));
            sut.ChangePropagated -= Handler;
            
            Assert.True(eventRaised);
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
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            var sut = new CompassShuffleRequirement(_mode, true);
            _mode.CompassShuffle.Returns(true);

            Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility),
                () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle))));
        }

        [Theory]
        [InlineData(AccessibilityLevel.Normal, false, false)]
        [InlineData(AccessibilityLevel.None, false, true)]
        [InlineData(AccessibilityLevel.Normal, true, true)]
        public void Accessibility_ShouldReturnExpectedValue(
            AccessibilityLevel expected, bool bigKeyShuffle, bool requirement)
        {
            _mode.CompassShuffle.Returns(bigKeyShuffle);
            var sut = new CompassShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Accessibility);
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