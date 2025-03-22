using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode
{
    public class WorldStateRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            const WorldState worldState = WorldState.Inverted;
            var sut = new WorldStateRequirement(_mode, worldState);
            _mode.WorldState.Returns(worldState);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.WorldState)));
            
            Assert.True(sut.Met);
        }

        [Fact]
        public void Met_ShouldRaisePropertyChanged()
        {
            const WorldState worldState = WorldState.Inverted;
            var sut = new WorldStateRequirement(_mode, worldState);
            _mode.WorldState.Returns(worldState);

            Assert.PropertyChanged(sut, nameof(IRequirement.Met), 
                () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.WorldState))));
        }

        [Fact]
        public void Met_ShouldRaiseChangePropagated()
        {
            const WorldState worldState = WorldState.Inverted;
            var sut = new WorldStateRequirement(_mode, worldState);
            _mode.WorldState.Returns(worldState);

            var eventRaised = false;

            void Handler(object? sender, EventArgs e)
            {
                eventRaised = true;
            }
            
            sut.ChangePropagated += Handler;
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.WorldState)));
            sut.ChangePropagated -= Handler;
            
            Assert.True(eventRaised);
        }

        [Theory]
        [InlineData(true, WorldState.StandardOpen, WorldState.StandardOpen)]
        [InlineData(false, WorldState.StandardOpen, WorldState.Inverted)]
        [InlineData(false, WorldState.Inverted, WorldState.StandardOpen)]
        [InlineData(true, WorldState.Inverted, WorldState.Inverted)]
        public void Met_ShouldReturnExpectedValue(bool expected, WorldState worldState, WorldState requirement)
        {
            _mode.WorldState.Returns(worldState);
            var sut = new WorldStateRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            const WorldState worldState = WorldState.Inverted;
            var sut = new WorldStateRequirement(_mode, worldState);
            _mode.WorldState.Returns(worldState);

            Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility), 
                () => _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.WorldState))));
        }

        [Theory]
        [InlineData(AccessibilityLevel.Normal, WorldState.StandardOpen, WorldState.StandardOpen)]
        [InlineData(AccessibilityLevel.None, WorldState.StandardOpen, WorldState.Inverted)]
        [InlineData(AccessibilityLevel.None, WorldState.Inverted, WorldState.StandardOpen)]
        [InlineData(AccessibilityLevel.Normal, WorldState.Inverted, WorldState.Inverted)]
        public void Accessibility_ShouldReturnExpectedValue(AccessibilityLevel expected, WorldState worldState, WorldState requirement)
        {
            _mode.WorldState.Returns(worldState);
            var sut = new WorldStateRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Accessibility);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IWorldStateRequirement.Factory>();
            var sut = factory(WorldState.StandardOpen);
            
            Assert.NotNull(sut as WorldStateRequirement);
        }
    }
}