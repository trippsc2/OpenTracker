using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.KeyDoor;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.KeyDoor
{
    public class KeyDoorRequirementTests
    {
        private readonly IKeyDoor _keyDoor = Substitute.For<IKeyDoor>();

        private readonly KeyDoorRequirement _sut;

        public KeyDoorRequirementTests()
        {
            _sut = new KeyDoorRequirement(_keyDoor);
        }

        [Fact]
        public void ItemChanged_ShouldUpdateValue()
        {
            _keyDoor.Unlocked.Returns(true);

            _keyDoor.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _keyDoor, new PropertyChangedEventArgs(nameof(IKeyDoor.Unlocked)));
            
            Assert.Equal(AccessibilityLevel.Normal, _sut.Accessibility);
        }

        [Fact]
        public void Met_ShouldRaisePropertyChanged()
        {
            _keyDoor.Unlocked.Returns(true);

            Assert.PropertyChanged(_sut, nameof(IRequirement.Met), 
                () => _keyDoor.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _keyDoor, new PropertyChangedEventArgs(nameof(IKeyDoor.Unlocked))));
        }

        [Fact]
        public void Met_ShouldRaiseChangePropagated()
        {
            _keyDoor.Unlocked.Returns(true);

            var eventRaised = false;

            void Handler(object? sender, EventArgs e)
            {
                eventRaised = true;
            }
            
            _sut.ChangePropagated += Handler;
            _keyDoor.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _keyDoor, new PropertyChangedEventArgs(nameof(IKeyDoor.Unlocked)));
            _sut.ChangePropagated -= Handler;
            
            Assert.True(eventRaised);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Met_ShouldMatchExpected(bool expected, bool unlocked)
        {
            _keyDoor.Unlocked.Returns(unlocked);
            
            _keyDoor.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _keyDoor, new PropertyChangedEventArgs(nameof(IKeyDoor.Unlocked)));
            
            Assert.Equal(expected, _sut.Met);
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            _keyDoor.Unlocked.Returns(true);

            Assert.PropertyChanged(_sut, nameof(IRequirement.Accessibility), 
                () => _keyDoor.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _keyDoor, new PropertyChangedEventArgs(nameof(IKeyDoor.Unlocked))));
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, false)]
        [InlineData(AccessibilityLevel.Normal, true)]
        public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, bool unlocked)
        {
            _keyDoor.Unlocked.Returns(unlocked);
            
            _keyDoor.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _keyDoor, new PropertyChangedEventArgs(nameof(IKeyDoor.Unlocked)));
            
            Assert.Equal(expected, _sut.Accessibility);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IKeyDoorRequirement.Factory>();
            var sut = factory(_keyDoor);
            
            Assert.NotNull(sut as KeyDoorRequirement);
        }
    }
}