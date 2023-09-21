using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements.KeyDoor;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyDoors;

[ExcludeFromCodeCoverage]
public sealed class KeyDoorTests
{
    private readonly INode _node = Substitute.For<INode>();

    private KeyDoorRequirement _requirement = default!;

    private readonly KeyDoor _sut;

    public KeyDoorTests()
    {
        _sut = new KeyDoor(Factory, _node);
        return;

        KeyDoorRequirement Factory(IKeyDoor keyDoor)
        {
            return _requirement = new KeyDoorRequirement(keyDoor);
        }
    }

    [Fact]
    public void Requirement_ShouldReturnNewRequirement()
    {
        Assert.Equal(_requirement, _sut.Requirement);
    }

    [Fact]
    public void Unlocked_ShouldRaisePropertyChanged()
    {
        Assert.PropertyChanged(_sut, nameof(IKeyDoor.Unlocked), () => 
            _sut.Unlocked = !_sut.Unlocked);
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldReturnNodeAccessibility(
        AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        _node.Accessibility.Returns(nodeAccessibility);
            
        Assert.Equal(expected, _sut.Accessibility);
    }

    [Fact]
    public void NodeChanged_ShouldPropertyChangeAccessibility()
    {
        Assert.PropertyChanged(_sut, nameof(IKeyDoor.Accessibility), () =>
            _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IKeyDoor.Factory>();
        var sut = factory(_node);
            
        Assert.NotNull(sut as KeyDoor);
    }
}