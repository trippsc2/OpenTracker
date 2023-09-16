using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Entrance;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Entrance;

public class DropdownSectionTests
{
    private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
    private readonly IMode _mode = Substitute.For<IMode>();

    private readonly ICollectSection.Factory _collectSectionFactory = (section, force) =>
        new CollectSection(section, force);
    private readonly IUncollectSection.Factory _uncollectSectionFactory = section =>
        new UncollectSection(section);
    private readonly IMarking.Factory _markingFactory = () => Substitute.For<IMarking>();

    private readonly INode _exitNode = Substitute.For<INode>();
    private readonly INode _holeNode = Substitute.For<INode>();
    private readonly IRequirement _requirement = Substitute.For<IRequirement>();

    private readonly DropdownSection _sut;

    public DropdownSectionTests()
    {
        _sut = new DropdownSection(
            _saveLoadManager, _mode, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, _exitNode,
            _holeNode, _requirement);
    }
        
    [Fact]
    public void Ctor_ShouldAlwaysSetNameToShop()
    {
        Assert.Equal("Dropdown", _sut.Name);
    }

    [Fact]
    public void Ctor_ShouldAlwaysSetMarking()
    {
        Assert.NotNull(_sut.Marking);
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
        _exitNode.Accessibility.Returns(AccessibilityLevel.Normal);
            
        Assert.PropertyChanged(_sut, nameof(ISection.Accessibility), () =>
            _exitNode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _exitNode, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Theory]
    [InlineData(
        AccessibilityLevel.None, EntranceShuffle.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.Inspect, EntranceShuffle.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, EntranceShuffle.None, AccessibilityLevel.None,
        AccessibilityLevel.SequenceBreak)]
    [InlineData(
        AccessibilityLevel.Normal, EntranceShuffle.None, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    [InlineData(
        AccessibilityLevel.Inspect, EntranceShuffle.None, AccessibilityLevel.SequenceBreak,
        AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.Inspect, EntranceShuffle.None, AccessibilityLevel.Normal, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.None, EntranceShuffle.Insanity, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.None, EntranceShuffle.Insanity, AccessibilityLevel.Normal, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, EntranceShuffle.Insanity, AccessibilityLevel.None,
        AccessibilityLevel.SequenceBreak)]
    [InlineData(
        AccessibilityLevel.Normal, EntranceShuffle.Insanity, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldEqualExpected(
        AccessibilityLevel expected, EntranceShuffle entranceShuffle, AccessibilityLevel exitAccessibility,
        AccessibilityLevel holeAccessibility)
    {
        _mode.EntranceShuffle = entranceShuffle;
        _exitNode.Accessibility.Returns(exitAccessibility);
        _holeNode.Accessibility.Returns(holeAccessibility);
        _exitNode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _exitNode, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
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
        Assert.PropertyChanged(_sut, nameof(ISection.ShouldBeDisplayed), () => _sut.Available = 0);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    public void ShouldBeDisplayed_ShouldEqualExpected(bool expected, int available)
    {
        _sut.Available = available;
            
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
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    public void CanBeCleared_ShouldReturnExpected(bool expected, int available)
    {
        _sut.Available = available;

        Assert.Equal(expected, _sut.CanBeCleared());
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

    [Fact]
    public void Reset_ShouldSetMarkingToUnknown()
    {
        _sut.Marking!.Mark = MarkType.Aga;
        _sut.Reset();
            
        Assert.Equal(MarkType.Unknown, _sut.Marking!.Mark);
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

    [Theory]
    [InlineData(MarkType.Aga, MarkType.Aga)]
    [InlineData(MarkType.Blacksmith, MarkType.Blacksmith)]
    public void Save_ShouldSetSaveDataMarkingToExpected(MarkType expected, MarkType marking)
    {
        _sut.Marking!.Mark = marking;
        var saveData = _sut.Save();
            
        Assert.Equal(expected, saveData.Marking);
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

    [Theory]
    [InlineData(MarkType.Aga, MarkType.Aga)]
    [InlineData(MarkType.Blacksmith, MarkType.Blacksmith)]
    public void Save_ShouldSetMarkingToExpected(MarkType expected, MarkType marking)
    {
        var saveData = new SectionSaveData {Marking = marking};
        _sut.Load(saveData);
            
        Assert.Equal(expected, _sut.Marking!.Mark);
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
    public void ModeChanged_ShouldUpdateAccessibility()
    {
        _holeNode.Accessibility.Returns(AccessibilityLevel.Normal);
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.EntranceShuffle)));
            
        Assert.Equal(AccessibilityLevel.Normal, _sut.Accessibility);
    }

    [Fact]
    public void NodeChanged_ShouldUpdateAccessibility()
    {
        _holeNode.Accessibility.Returns(AccessibilityLevel.Normal);
        _holeNode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _exitNode, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
        Assert.Equal(AccessibilityLevel.Normal, _sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDropdownSection.Factory>();
        var sut = factory(
            Substitute.For<INode>(), Substitute.For<INode>(),
            Substitute.For<IRequirement>());
            
        Assert.NotNull(sut as DropdownSection);
    }
}