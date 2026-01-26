using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Item;

public class ItemSectionTests
{
    private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

    private readonly ICollectSection.Factory _collectSectionFactory = (section, force) =>
        new CollectSection(section, force);
    private readonly IUncollectSection.Factory _uncollectSectionFactory = section =>
        new UncollectSection(section);

    private readonly INode _node = Substitute.For<INode>();
    private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
    private readonly IMarking _marking = Substitute.For<IMarking>();
    private readonly IRequirement _requirement = Substitute.For<IRequirement>();
    private readonly INode _visibleNode = Substitute.For<INode>();

    [Theory]
    [InlineData("Cave", "Cave")]
    [InlineData("House", "House")]
    public void Ctor_ShouldSetNameToExpected(string expected, string name)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, name, _node, 1,
            _autoTrackValue, _marking, _requirement, _visibleNode);
            
        Assert.Equal(expected, sut.Name);
    }

    [Fact]
    public void Ctor_ShouldSetMarkingToExpected_WhenMarkingIsNotNull()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement, _visibleNode);

        Assert.NotNull(sut.Marking);
    }

    [Fact]
    public void Ctor_ShouldSetMarkingToNull_WhenMarkingIsNull()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, null, _requirement, _visibleNode);

        Assert.Null(sut.Marking);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void Ctor_ShouldSetAvailableToTotal(int expected, int total)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, total,
            _autoTrackValue, null, _requirement, _visibleNode);

        Assert.Equal(expected, sut.Available);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void Ctor_ShouldSetTotalToExpected(int expected, int total)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, total,
            _autoTrackValue, null, _requirement, _visibleNode);
            
        Assert.Equal(expected, sut.Total);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, null, _requirement, _visibleNode);
        _node.Accessibility.Returns(AccessibilityLevel.Normal);

        Assert.PropertyChanged(sut, nameof(ISection.Accessibility), () =>
            _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldEqualExpected_WhenVisibleNodeIsNull(
        AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
    {
        _node.Accessibility.Returns(nodeAccessibility);
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, null, _requirement);
            
        Assert.Equal(expected, sut.Accessibility);
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldEqualExpected_WhenVisibleNodeIsNotNull(
        AccessibilityLevel expected, AccessibilityLevel nodeAccessibility,
        AccessibilityLevel visibleNodeAccessibility)
    {
        _node.Accessibility.Returns(nodeAccessibility);
        _visibleNode.Accessibility.Returns(visibleNodeAccessibility);
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, null, _requirement, _visibleNode);
            
        Assert.Equal(expected, sut.Accessibility);
    }

    [Fact]
    public void Accessible_ShouldRaisePropertyChanged()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, null, _requirement);
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
            
        Assert.PropertyChanged(sut, nameof(IItemSection.Accessible), () =>
            _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(INode.Accessibility))));
    }

    [Theory]
    [InlineData(0, AccessibilityLevel.None, 0)]
    [InlineData(0, AccessibilityLevel.None, 1)]
    [InlineData(0, AccessibilityLevel.Inspect, 0)]
    [InlineData(0, AccessibilityLevel.Inspect, 1)]
    [InlineData(0, AccessibilityLevel.SequenceBreak, 0)]
    [InlineData(1, AccessibilityLevel.SequenceBreak, 1)]
    [InlineData(0, AccessibilityLevel.Normal, 0)]
    [InlineData(1, AccessibilityLevel.Normal, 1)]
    public void Accessible_ShouldEqualExpected(
        int expected, AccessibilityLevel nodeAccessibility, int available)
    {
        _node.Accessibility.Returns(nodeAccessibility);
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, null, _requirement) {Available = available};
            
        Assert.Equal(expected, sut.Accessible);
    }

    [Fact]
    public void Available_ShouldRaisePropertyChanged()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, null, _requirement);
            
        Assert.PropertyChanged(sut, nameof(ISection.Available), () => sut.Available = 0);
    }

    [Fact]
    public void IsActive_ShouldRaisePropertyChanged()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        _requirement.Met.Returns(true);
            
        Assert.PropertyChanged(sut, nameof(ISection.IsActive), () =>
            _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
    }

    [Fact]
    public void IsActive_ShouldAlwaysReturnTrue_WhenRequirementIsNull()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking);
            
        Assert.True(sut.IsActive);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void IsActive_ShouldReturnExpected(bool expected, bool requirementMet)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        _requirement.Met.Returns(requirementMet);
        _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
        Assert.Equal(expected, sut.IsActive);
    }

    [Fact]
    public void ShouldBeDisplayed_ShouldRaisePropertyChanged()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
            
        Assert.PropertyChanged(sut, nameof(ISection.ShouldBeDisplayed), () => sut.Available = 0);
    }

    [Theory]
    [InlineData(false, null, AccessibilityLevel.None, 0)]
    [InlineData(false, null, AccessibilityLevel.None, 1)]
    [InlineData(false, null, AccessibilityLevel.SequenceBreak, 0)]
    [InlineData(true, null, AccessibilityLevel.SequenceBreak, 1)]
    [InlineData(false, null, AccessibilityLevel.Normal, 0)]
    [InlineData(true, null, AccessibilityLevel.Normal, 1)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.None, 0)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.None, 1)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.Inspect, 0)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.Inspect, 1)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 0)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 1)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.Normal, 0)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.Normal, 1)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.None, 0)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.None, 1)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.Inspect, 0)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.Inspect, 1)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.SequenceBreak, 0)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.SequenceBreak, 1)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.Normal, 0)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.Normal, 1)]
    public void ShouldBeDisplayed_ShouldReturnExpected(
        bool expected, MarkType? mark, AccessibilityLevel nodeAccessibility, int available)
    {
        IMarking? marking = null;

        if (mark is not null)
        {
            _marking.Mark.Returns(mark.Value);
            marking = _marking;
        }

        _node.Accessibility.Returns(nodeAccessibility);
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, marking, _requirement) {Available = available};
            
        Assert.Equal(expected, sut.ShouldBeDisplayed);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    public void IsAvailable_ShouldReturnExpected(bool expected, int available)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement) {Available = available};

        Assert.Equal(expected, sut.IsAvailable());
    }

    [Theory]
    [InlineData(false, null, AccessibilityLevel.None, 0, false)]
    [InlineData(false, null, AccessibilityLevel.None, 1, false)]
    [InlineData(false, null, AccessibilityLevel.SequenceBreak, 0, false)]
    [InlineData(true, null, AccessibilityLevel.SequenceBreak, 1, false)]
    [InlineData(false, null, AccessibilityLevel.Normal, 0, false)]
    [InlineData(true, null, AccessibilityLevel.Normal, 1, false)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.None, 0, false)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.None, 1, false)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.Inspect, 0, false)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.Inspect, 1, false)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 0, false)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 1, false)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.Normal, 0, false)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.Normal, 1, false)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.None, 0, false)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.None, 1, false)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.Inspect, 0, false)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.Inspect, 1, false)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.SequenceBreak, 0, false)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.SequenceBreak, 1, false)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.Normal, 0, false)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.Normal, 1, false)]
    [InlineData(false, null, AccessibilityLevel.None, 0, true)]
    [InlineData(true, null, AccessibilityLevel.None, 1, true)]
    [InlineData(false, null, AccessibilityLevel.SequenceBreak, 0, true)]
    [InlineData(true, null, AccessibilityLevel.SequenceBreak, 1, true)]
    [InlineData(false, null, AccessibilityLevel.Normal, 0, true)]
    [InlineData(true, null, AccessibilityLevel.Normal, 1, true)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.None, 0, true)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.None, 1, true)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.Inspect, 0, true)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.Inspect, 1, true)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 0, true)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 1, true)]
    [InlineData(false, MarkType.Unknown, AccessibilityLevel.Normal, 0, true)]
    [InlineData(true, MarkType.Unknown, AccessibilityLevel.Normal, 1, true)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.None, 0, true)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.None, 1, true)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.Inspect, 0, true)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.Inspect, 1, true)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.SequenceBreak, 0, true)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.SequenceBreak, 1, true)]
    [InlineData(false, MarkType.Aga, AccessibilityLevel.Normal, 0, true)]
    [InlineData(true, MarkType.Aga, AccessibilityLevel.Normal, 1, true)]
    public void CanBeCleared_ShouldReturnExpected(
        bool expected, MarkType? mark, AccessibilityLevel nodeAccessibility, int available, bool force)
    {
        IMarking? marking = null;

        if (mark is not null)
        {
            _marking.Mark.Returns(mark.Value);
            marking = _marking;
        }

        _node.Accessibility.Returns(nodeAccessibility);
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, marking, _requirement) {Available = available};
            
        Assert.Equal(expected, sut.CanBeCleared(force));
    }

    [Theory]
    [InlineData(0, null, AccessibilityLevel.None, 0, false)]
    [InlineData(1, null, AccessibilityLevel.None, 1, false)]
    [InlineData(0, null, AccessibilityLevel.SequenceBreak, 0, false)]
    [InlineData(0, null, AccessibilityLevel.SequenceBreak, 1, false)]
    [InlineData(0, null, AccessibilityLevel.Normal, 0, false)]
    [InlineData(0, null, AccessibilityLevel.Normal, 1, false)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.None, 0, false)]
    [InlineData(1, MarkType.Unknown, AccessibilityLevel.None, 1, false)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Inspect, 0, false)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Inspect, 1, false)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 0, false)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 1, false)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Normal, 0, false)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Normal, 1, false)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.None, 0, false)]
    [InlineData(1, MarkType.Aga, AccessibilityLevel.None, 1, false)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.Inspect, 0, false)]
    [InlineData(1, MarkType.Aga, AccessibilityLevel.Inspect, 1, false)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.SequenceBreak, 0, false)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.SequenceBreak, 1, false)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.Normal, 0, false)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.Normal, 1, false)]
    [InlineData(0, null, AccessibilityLevel.None, 0, true)]
    [InlineData(0, null, AccessibilityLevel.None, 1, true)]
    [InlineData(0, null, AccessibilityLevel.SequenceBreak, 0, true)]
    [InlineData(0, null, AccessibilityLevel.SequenceBreak, 1, true)]
    [InlineData(0, null, AccessibilityLevel.Normal, 0, true)]
    [InlineData(0, null, AccessibilityLevel.Normal, 1, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.None, 0, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.None, 1, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Inspect, 0, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Inspect, 1, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 0, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.SequenceBreak, 1, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Normal, 0, true)]
    [InlineData(0, MarkType.Unknown, AccessibilityLevel.Normal, 1, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.None, 0, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.None, 1, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.Inspect, 0, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.Inspect, 1, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.SequenceBreak, 0, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.SequenceBreak, 1, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.Normal, 0, true)]
    [InlineData(0, MarkType.Aga, AccessibilityLevel.Normal, 1, true)]
    public void Clear_ShouldSetAvailableToExpected(
        int expected, MarkType? mark, AccessibilityLevel nodeAccessibility, int available, bool force)
    {
        IMarking? marking = null;

        if (mark is not null)
        {
            _marking.Mark.Returns(mark.Value);
            marking = _marking;
        }

        _node.Accessibility.Returns(nodeAccessibility);
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, marking, _requirement) {Available = available};
        sut.Clear(force);
            
        Assert.Equal(expected, sut.Available);
    }

    [Fact]
    public void CreateCollectSectionAction_ShouldReturnNewAction()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        var action = sut.CreateCollectSectionAction(false);
            
        Assert.NotNull(action as CollectSection);
    }

    [Theory]
    [InlineData(true, 0)]
    [InlineData(false, 1)]
    public void CanBeUncleared_ShouldReturnExpected(bool expected, int available)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement) {Available = available};

        Assert.Equal(expected, sut.CanBeUncleared());
    }

    [Fact]
    public void CreateUncollectSectionAction_ShouldReturnNewAction()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        var action = sut.CreateUncollectSectionAction();
            
        Assert.NotNull(action as UncollectSection);
    }

    [Fact]
    public void Reset_ShouldSetUserManipulatedToFalse()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement) {UserManipulated = true};
        sut.Reset();
            
        Assert.False(sut.UserManipulated);
    }

    [Fact]
    public void Reset_ShouldSetAvailableTo1()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement) {Available = 0};
        sut.Reset();
            
        Assert.Equal(1, sut.Available);
    }

    [Fact]
    public void Reset_ShouldSetMarkingToUnknown()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        sut.Marking!.Mark = MarkType.Aga;
        sut.Reset();
            
        Assert.Equal(MarkType.Unknown, sut.Marking!.Mark);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Save_ShouldSetSaveDataUserManipulatedToExpected(bool expected, bool userManipulated)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement) {UserManipulated = userManipulated};
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.UserManipulated);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void Save_ShouldSetSaveDataAvailableToExpected(int expected, int available)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement) {Available = available};
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.Available);
    }

    [Theory]
    [InlineData(MarkType.Aga, MarkType.Aga)]
    [InlineData(MarkType.Blacksmith, MarkType.Blacksmith)]
    public void Save_ShouldSetSaveDataMarkingToExpected(MarkType expected, MarkType marking)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        sut.Marking!.Mark = marking;
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.Marking);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        sut.Load(null);
            
        Assert.Equal(1, sut.Available);
    }
        
    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Load_ShouldSetUserManipulatedToExpected(bool expected, bool userManipulated)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        var saveData = new SectionSaveData {UserManipulated = userManipulated};
        sut.Load(saveData);
            
        Assert.Equal(expected, sut.UserManipulated);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void Load_ShouldSetAvailableToExpected(int expected, int available)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        var saveData = new SectionSaveData {Available = available};
        sut.Load(saveData);
            
        Assert.Equal(expected, sut.Available);
    }

    [Theory]
    [InlineData(MarkType.Aga, MarkType.Aga)]
    [InlineData(MarkType.Blacksmith, MarkType.Blacksmith)]
    public void Load_ShouldSetMarkingToExpected(MarkType expected, MarkType marking)
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        var saveData = new SectionSaveData {Marking = marking};
        sut.Load(saveData);
            
        Assert.Equal(expected, sut.Marking!.Mark);
    }

    [Fact]
    public void NodeChanged_ShouldUpdateAccessibility()
    {
        var sut = new ItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, "Test", _node, 1,
            _autoTrackValue, _marking, _requirement);
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
        Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IItemSection.Factory>();
        var sut = factory(
            "Test", _node, 1, _autoTrackValue, _marking, _requirement, _visibleNode);
            
        Assert.NotNull(sut as ItemSection);
    }
}