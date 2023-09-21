using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.KeyDoor;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.KeyDoor;

[ExcludeFromCodeCoverage]
public sealed class KeyDoorRequirementTests
{
    private readonly IKeyDoor _keyDoor = Substitute.For<IKeyDoor>();

    private readonly KeyDoorRequirement _sut;

    public KeyDoorRequirementTests()
    {
        _sut = new KeyDoorRequirement(_keyDoor);
    }
    
    private void ChangeKeyDoorUnlocked(bool newValue)
    {
        _keyDoor.Unlocked.Returns(newValue);
        _keyDoor.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _keyDoor,
                new PropertyChangedEventArgs(nameof(IKeyDoor.Unlocked)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeKeyDoorUnlocked(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Met_ShouldMatchExpected(bool expected, bool unlocked)
    {
        ChangeKeyDoorUnlocked(unlocked);

        _sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeKeyDoorUnlocked(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();
        
        ChangeKeyDoorUnlocked(true);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, false)]
    [InlineData(AccessibilityLevel.Normal, true)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, bool unlocked)
    {
        ChangeKeyDoorUnlocked(unlocked);

        _sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<KeyDoorRequirement.Factory>();
        var sut1 = factory(_keyDoor);
        var sut2 = factory(_keyDoor);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}