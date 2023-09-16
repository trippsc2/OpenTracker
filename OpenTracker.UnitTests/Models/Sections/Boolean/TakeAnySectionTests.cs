using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boolean;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Boolean;

public class TakeAnySectionTests
{
    private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

    private readonly ICollectSection.Factory _collectSectionFactory = (section, force) =>
        new CollectSection(section, force);
    private readonly IUncollectSection.Factory _uncollectSectionFactory = section =>
        new UncollectSection(section);

    private readonly INode _node = Substitute.For<INode>();
    private readonly IRequirement _requirement = Substitute.For<IRequirement>();

    private readonly TakeAnySection _sut;

    public TakeAnySectionTests()
    {
        _sut = new TakeAnySection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _node, _requirement);
    }
        
    [Fact]
    public void Ctor_ShouldAlwaysSetNameToShop()
    {
        Assert.Equal("Take Any", _sut.Name);
    }

    [Fact]
    public void Ctor_ShouldAlwaysSetMarkingToNull()
    {
        Assert.Null(_sut.Marking);
    }

    [Fact]
    public void Ctor_ShouldSetAvailableTo1()
    {
        Assert.Equal(1, _sut.Available);
    }

    [Fact]
    public void Ctor_ShouldSetTotalTo1()
    {
        Assert.Equal(1, _sut.Total);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
            
        Assert.PropertyChanged(_sut, nameof(ISection.Accessibility), () =>
            _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldEqualExpected(AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        _node.Accessibility.Returns(nodeAccessibility);
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
        Assert.Equal(expected, _sut.Accessibility);
    }

    [Fact]
    public void Available_ShouldRaisePropertyChanged()
    {
        Assert.PropertyChanged(_sut, nameof(ISection.Available), () => _sut.Available = 0);
    }

    [Fact]
    public void IsActive_ShouldRaisePropertyChanged()
    {
        _requirement.Met.Returns(true);
            
        Assert.PropertyChanged(_sut, nameof(ISection.IsActive), () =>
            _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void IsActive_ShouldReturnExpected(bool expected, bool requirementMet)
    {
        _requirement.Met.Returns(requirementMet);
        _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
        Assert.Equal(expected, _sut.IsActive);
    }

    [Fact]
    public void ShouldBeDisplayed_ShouldRaisePropertyChanged()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
            
        Assert.PropertyChanged(_sut, nameof(ISection.ShouldBeDisplayed), () =>
            _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Theory]
    [InlineData(false, AccessibilityLevel.None, 0)]
    [InlineData(false, AccessibilityLevel.None, 1)]
    [InlineData(false, AccessibilityLevel.Inspect, 0)]
    [InlineData(true, AccessibilityLevel.Inspect, 1)]
    [InlineData(false, AccessibilityLevel.SequenceBreak, 0)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, 1)]
    [InlineData(false, AccessibilityLevel.Normal, 0)]
    [InlineData(true, AccessibilityLevel.Normal, 1)]
    public void ShouldBeDisplayed_ShouldEqualExpected(
        bool expected, AccessibilityLevel nodeAccessibility, int available)
    {
        _sut.Available = available;
        _node.Accessibility.Returns(nodeAccessibility);
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
        Assert.Equal(expected, _sut.ShouldBeDisplayed);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    public void IsAvailable_ShouldReturnExpected(bool expected, int available)
    {
        _sut.Available = available;
            
        Assert.Equal(expected, _sut.IsAvailable());
    }

    [Theory]
    [InlineData(false, AccessibilityLevel.None, 0, false)]
    [InlineData(false, AccessibilityLevel.None, 0, true)]
    [InlineData(false, AccessibilityLevel.None, 1, false)]
    [InlineData(true, AccessibilityLevel.None, 1, true)]
    [InlineData(false, AccessibilityLevel.Inspect, 0, false)]
    [InlineData(false, AccessibilityLevel.Inspect, 0, true)]
    [InlineData(false, AccessibilityLevel.Inspect, 1, false)]
    [InlineData(true, AccessibilityLevel.Inspect, 1, true)]
    [InlineData(false, AccessibilityLevel.SequenceBreak, 0, false)]
    [InlineData(false, AccessibilityLevel.SequenceBreak, 0, true)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, 1, false)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, 1, true)]
    [InlineData(false, AccessibilityLevel.Normal, 0, false)]
    [InlineData(false, AccessibilityLevel.Normal, 0, true)]
    [InlineData(true, AccessibilityLevel.Normal, 1, false)]
    [InlineData(true, AccessibilityLevel.Normal, 1, true)]
    public void CanBeCleared_ShouldReturnExpected(
        bool expected, AccessibilityLevel accessibility, int available, bool force)
    {
        _node.Accessibility.Returns(accessibility);
        _sut.Available = available;

        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
        Assert.Equal(expected, _sut.CanBeCleared(force));
    }

    [Fact]
    public void Clear_ShouldSetAvailableTo0()
    {
        _sut.Clear(false);
            
        Assert.Equal(0, _sut.Available);
    }

    [Fact]
    public void CreateCollectSectionAction_ShouldReturnNewAction()
    {
        var action = _sut.CreateCollectSectionAction(false);
            
        Assert.NotNull(action as CollectSection);
    }

    [Theory]
    [InlineData(true, 0)]
    [InlineData(false, 1)]
    public void CanBeUncleared_ShouldReturnExpected(bool expected, int available)
    {
        _sut.Available = available;
            
        Assert.Equal(expected, _sut.CanBeUncleared());
    }

    [Fact]
    public void CreateUncollectSectionAction_ShouldReturnNewAction()
    {
        var action = _sut.CreateUncollectSectionAction();
            
        Assert.NotNull(action as UncollectSection);
    }

    [Fact]
    public void Reset_ShouldSetUserManipulatedToFalse()
    {
        _sut.UserManipulated = true;
        _sut.Reset();
            
        Assert.False(_sut.UserManipulated);
    }

    [Fact]
    public void Reset_ShouldSetAvailableTo1()
    {
        _sut.Available = 0;
        _sut.Reset();
            
        Assert.Equal(1, _sut.Available);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Save_ShouldSetSaveDataUserManipulatedToExpected(bool expected, bool userManipulated)
    {
        _sut.UserManipulated = userManipulated;
        var saveData = _sut.Save();
            
        Assert.Equal(expected, saveData.UserManipulated);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void Save_ShouldSetSaveDataAvailableToExpected(int expected, int available)
    {
        _sut.Available = available;
        var saveData = _sut.Save();
            
        Assert.Equal(expected, saveData.Available);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        _sut.Load(null);
            
        Assert.Equal(1, _sut.Available);
    }
        
    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Load_ShouldSetUserManipulatedToExpected(bool expected, bool userManipulated)
    {
        var saveData = new SectionSaveData {UserManipulated = userManipulated};
        _sut.Load(saveData);
            
        Assert.Equal(expected, _sut.UserManipulated);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void Load_ShouldSetAvailableToExpected(int expected, int available)
    {
        var saveData = new SectionSaveData {Available = available};
        _sut.Load(saveData);
            
        Assert.Equal(expected, _sut.Available);
    }

    [Fact]
    public void RequirementChanged_ShouldUpdateIsActive()
    {
        _requirement.Met.Returns(true);
        _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
        Assert.True(_sut.IsActive);
    }

    [Fact]
    public void NodeChanged_ShouldUpdateAccessibility()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
        Assert.Equal(AccessibilityLevel.Normal, _sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ITakeAnySection.Factory>();
        var sut = factory(Substitute.For<INode>(), Substitute.For<IRequirement>());
            
        Assert.NotNull(sut as TakeAnySection);
    }
}