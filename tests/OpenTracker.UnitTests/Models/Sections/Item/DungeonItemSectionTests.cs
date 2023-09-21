using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Item;

[ExcludeFromCodeCoverage]
public sealed class DungeonItemSectionTests
{
    private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

    private readonly ICollectSection.Factory _collectSectionFactory = (section, force) =>
        new CollectSection(section, force);
    private readonly IUncollectSection.Factory _uncollectSectionFactory = section =>
        new UncollectSection(section);

    private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
    private readonly IDungeonAccessibilityProvider _accessibilityProvider =
        Substitute.For<IDungeonAccessibilityProvider>();
    private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
    private readonly IMarking _marking = Substitute.For<IMarking>();
    private readonly IRequirement _requirement = Substitute.For<IRequirement>();
        
    [Fact]
    public void Ctor_ShouldAlwaysSetNameToDungeon()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
            
        Assert.Equal("Dungeon", sut.Name);
    }

    [Fact]
    public void Ctor_ShouldSetMarkingToExpected_WhenMarkingIsNotNull()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);

        Assert.NotNull(sut.Marking);
    }

    [Fact]
    public void Ctor_ShouldSetMarkingToNull_WhenMarkingIsNull()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, null, _requirement);

        Assert.Null(sut.Marking);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void Ctor_ShouldSetAvailableToExpected(int expected, int total)
    {
        _dungeon.Total.Returns(total);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);

        Assert.Equal(expected, sut.Available);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _dungeon.Total.Returns(1);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);

        _accessibilityProvider.Accessible.Returns(1);
            
        Assert.PropertyChanged(sut, nameof(ISection.Accessibility), () =>
            _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _accessibilityProvider,
                new PropertyChangedEventArgs(nameof(IDungeonAccessibilityProvider.Accessible))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, 0, false, false, 0)]
    [InlineData(AccessibilityLevel.None, 0, false, false, 1)]
    [InlineData(AccessibilityLevel.None, 0, false, false, 2)]
    [InlineData(AccessibilityLevel.None, 0, false, true, 0)]
    [InlineData(AccessibilityLevel.None, 0, false, true, 1)]
    [InlineData(AccessibilityLevel.Inspect, 0, false, true, 2)]
    [InlineData(AccessibilityLevel.None, 0, true, false, 0)]
    [InlineData(AccessibilityLevel.None, 0, true, false, 1)]
    [InlineData(AccessibilityLevel.None, 0, true, false, 2)]
    [InlineData(AccessibilityLevel.None, 0, true, true, 0)]
    [InlineData(AccessibilityLevel.None, 0, true, true, 1)]
    [InlineData(AccessibilityLevel.Inspect, 0, true, true, 2)]
    [InlineData(AccessibilityLevel.None, 1, false, false, 0)]
    [InlineData(AccessibilityLevel.None, 1, false, false, 1)]
    [InlineData(AccessibilityLevel.Partial, 1, false, false, 2)]
    [InlineData(AccessibilityLevel.None, 1, false, true, 0)]
    [InlineData(AccessibilityLevel.Inspect, 1, false, true, 1)]
    [InlineData(AccessibilityLevel.Partial, 1, false, true, 2)]
    [InlineData(AccessibilityLevel.None, 1, true, false, 0)]
    [InlineData(AccessibilityLevel.None, 1, true, false, 1)]
    [InlineData(AccessibilityLevel.Partial, 1, true, false, 2)]
    [InlineData(AccessibilityLevel.None, 1, true, true, 0)]
    [InlineData(AccessibilityLevel.Inspect, 1, true, true, 1)]
    [InlineData(AccessibilityLevel.Partial, 1, true, true, 2)]
    [InlineData(AccessibilityLevel.Normal, 2, false, false, 0)]
    [InlineData(AccessibilityLevel.Normal, 2, false, false, 1)]
    [InlineData(AccessibilityLevel.Normal, 2, false, false, 2)]
    [InlineData(AccessibilityLevel.Normal, 2, false, true, 0)]
    [InlineData(AccessibilityLevel.Normal, 2, false, true, 1)]
    [InlineData(AccessibilityLevel.Normal, 2, false, true, 2)]
    [InlineData(AccessibilityLevel.SequenceBreak, 2, true, false, 0)]
    [InlineData(AccessibilityLevel.SequenceBreak, 2, true, false, 1)]
    [InlineData(AccessibilityLevel.SequenceBreak, 2, true, false, 2)]
    [InlineData(AccessibilityLevel.SequenceBreak, 2, true, true, 0)]
    [InlineData(AccessibilityLevel.SequenceBreak, 2, true, true, 1)]
    [InlineData(AccessibilityLevel.SequenceBreak, 2, true, true, 2)]
    public void Accessibility_ShouldEqualExpected(
        AccessibilityLevel expected, int accessible, bool sequenceBreak, bool visible, int available)
    {
        _dungeon.Total.Returns(2);
        _accessibilityProvider.Accessible.Returns(accessible);
        _accessibilityProvider.SequenceBreak.Returns(sequenceBreak);
        _accessibilityProvider.Visible.Returns(visible);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = available};

        Assert.Equal(expected, sut.Accessibility);
    }

    [Fact]
    public void Accessible_ShouldRaisePropertyChanged()
    {
        _dungeon.Total.Returns(1);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);

        _accessibilityProvider.Accessible.Returns(1);
            
        Assert.PropertyChanged(sut, nameof(IItemSection.Accessible), () =>
            _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _accessibilityProvider,
                new PropertyChangedEventArgs(nameof(IDungeonAccessibilityProvider.Accessible))));
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 0, 1)]
    [InlineData(0, 0, 2)]
    [InlineData(0, 1, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(1, 1, 2)]
    [InlineData(0, 2, 0)]
    [InlineData(1, 2, 1)]
    [InlineData(2, 2, 2)]
    public void Accessible_ShouldEqualExpected(int expected, int accessible, int available)
    {
        _dungeon.Total.Returns(2);
        _accessibilityProvider.Accessible.Returns(accessible);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = available};
            
        Assert.Equal(expected, sut.Accessible);
    }

    [Fact]
    public void Available_ShouldRaisePropertyChanged()
    {
        _dungeon.Total.Returns(1);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        Assert.PropertyChanged(sut, nameof(ISection.Available), () => sut.Available = 0);
    }

    [Fact]
    public void Total_ShouldRaisePropertyChanged()
    {
        _dungeon.Total.Returns(1);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _dungeon.Total.Returns(2);
            
        Assert.PropertyChanged(sut, nameof(IItemSection.Total), () =>
            _dungeon.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _dungeon, new PropertyChangedEventArgs(nameof(IDungeon.Total))));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void Total_ShouldEqualExpected(int expected, int total)
    {
        _dungeon.Total.Returns(total);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
            
        Assert.Equal(expected, sut.Total);
    }

    [Theory]
    [InlineData(1, 1, 2, 0)]
    [InlineData(2, 1, 2, 1)]
    [InlineData(2, 1, 3, 0)]
    [InlineData(3, 1, 3, 1)]
    [InlineData(0, 2, 1, 0)]
    [InlineData(0, 2, 1, 1)]
    [InlineData(1, 2, 1, 2)]
    [InlineData(1, 2, 3, 0)]
    [InlineData(2, 2, 3, 1)]
    [InlineData(3, 2, 3, 2)]
    [InlineData(0, 3, 1, 0)]
    [InlineData(0, 3, 1, 1)]
    [InlineData(0, 3, 1, 2)]
    [InlineData(1, 3, 1, 3)]
    [InlineData(0, 3, 2, 0)]
    [InlineData(0, 3, 2, 1)]
    [InlineData(1, 3, 2, 2)]
    [InlineData(2, 3, 2, 3)]
    public void Total_ChangeShouldAdjustAvailableToExpected(
        int expected, int startingTotal, int changedTotal, int available)
    {
        _dungeon.Total.Returns(startingTotal);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = available};
        _dungeon.Total.Returns(changedTotal);
        _dungeon.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _dungeon, new PropertyChangedEventArgs(nameof(IDungeon.Total)));
            
        Assert.Equal(expected, sut.Available);
    }

    [Fact]
    public void IsActive_ShouldRaisePropertyChanged()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _requirement.Met.Returns(true);
            
        Assert.PropertyChanged(sut, nameof(ISection.IsActive), () =>
            _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
    }

    [Fact]
    public void IsActive_ShouldAlwaysReturnTrue_WhenRequirementIsNull()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking);
            
        Assert.True(sut.IsActive);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void IsActive_ShouldReturnExpected(bool expected, bool requirementMet)
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _requirement.Met.Returns(requirementMet);
        _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
        Assert.Equal(expected, sut.IsActive);
    }

    [Fact]
    public void ShouldBeDisplayed_ShouldRaisePropertyChanged()
    {
        _dungeon.Total.Returns(1);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _accessibilityProvider.Accessible.Returns(1);
            
        Assert.PropertyChanged(sut, nameof(ISection.ShouldBeDisplayed), () =>
            _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _accessibilityProvider,
                new PropertyChangedEventArgs(nameof(IDungeonAccessibilityProvider.Accessible))));
    }

    [Theory]
    [InlineData(false, null, 0, false, false, 0)]
    [InlineData(false, null, 0, false, false, 1)]
    [InlineData(false, null, 0, false, false, 2)]
    [InlineData(false, null, 0, true, false, 0)]
    [InlineData(false, null, 0, true, false, 1)]
    [InlineData(false, null, 0, true, false, 2)]
    [InlineData(false, null, 1, false, false, 0)]
    [InlineData(false, null, 1, false, false, 1)]
    [InlineData(true, null, 1, false, false, 2)]
    [InlineData(false, null, 1, true, false, 0)]
    [InlineData(false, null, 1, true, false, 1)]
    [InlineData(true, null, 1, true, false, 2)]
    [InlineData(false, null, 2, false, false, 0)]
    [InlineData(true, null, 2, false, false, 1)]
    [InlineData(true, null, 2, false, false, 2)]
    [InlineData(false, null, 2, true, false, 0)]
    [InlineData(true, null, 2, true, false, 1)]
    [InlineData(true, null, 2, true, false, 2)]
    [InlineData(false, MarkType.Unknown, 0, false, false, 0)]
    [InlineData(false, MarkType.Unknown, 0, false, false, 1)]
    [InlineData(false, MarkType.Unknown, 0, false, false, 2)]
    [InlineData(false, MarkType.Unknown, 0, false, true, 0)]
    [InlineData(false, MarkType.Unknown, 0, false, true, 1)]
    [InlineData(true, MarkType.Unknown, 0, false, true, 2)]
    [InlineData(false, MarkType.Unknown, 0, true, false, 0)]
    [InlineData(false, MarkType.Unknown, 0, true, false, 1)]
    [InlineData(false, MarkType.Unknown, 0, true, false, 2)]
    [InlineData(false, MarkType.Unknown, 0, true, true, 0)]
    [InlineData(false, MarkType.Unknown, 0, true, true, 1)]
    [InlineData(true, MarkType.Unknown, 0, true, true, 2)]
    [InlineData(false, MarkType.Unknown, 1, false, false, 0)]
    [InlineData(false, MarkType.Unknown, 1, false, false, 1)]
    [InlineData(true, MarkType.Unknown, 1, false, false, 2)]
    [InlineData(false, MarkType.Unknown, 1, false, true, 0)]
    [InlineData(true, MarkType.Unknown, 1, false, true, 1)]
    [InlineData(true, MarkType.Unknown, 1, false, true, 2)]
    [InlineData(false, MarkType.Unknown, 1, true, false, 0)]
    [InlineData(false, MarkType.Unknown, 1, true, false, 1)]
    [InlineData(true, MarkType.Unknown, 1, true, false, 2)]
    [InlineData(false, MarkType.Unknown, 1, true, true, 0)]
    [InlineData(true, MarkType.Unknown, 1, true, true, 1)]
    [InlineData(true, MarkType.Unknown, 1, true, true, 2)]
    [InlineData(false, MarkType.Unknown, 2, false, false, 0)]
    [InlineData(true, MarkType.Unknown, 2, false, false, 1)]
    [InlineData(true, MarkType.Unknown, 2, false, false, 2)]
    [InlineData(false, MarkType.Unknown, 2, false, true, 0)]
    [InlineData(true, MarkType.Unknown, 2, false, true, 1)]
    [InlineData(true, MarkType.Unknown, 2, false, true, 2)]
    [InlineData(false, MarkType.Unknown, 2, true, false, 0)]
    [InlineData(true, MarkType.Unknown, 2, true, false, 1)]
    [InlineData(true, MarkType.Unknown, 2, true, false, 2)]
    [InlineData(false, MarkType.Unknown, 2, true, true, 0)]
    [InlineData(true, MarkType.Unknown, 2, true, true, 1)]
    [InlineData(true, MarkType.Unknown, 2, true, true, 2)]
    [InlineData(false, MarkType.Aga, 0, false, false, 0)]
    [InlineData(false, MarkType.Aga, 0, false, false, 1)]
    [InlineData(false, MarkType.Aga, 0, false, false, 2)]
    [InlineData(false, MarkType.Aga, 0, false, true, 0)]
    [InlineData(false, MarkType.Aga, 0, false, true, 1)]
    [InlineData(true, MarkType.Aga, 0, false, true, 2)]
    [InlineData(false, MarkType.Aga, 0, true, false, 0)]
    [InlineData(false, MarkType.Aga, 0, true, false, 1)]
    [InlineData(false, MarkType.Aga, 0, true, false, 2)]
    [InlineData(false, MarkType.Aga, 0, true, true, 0)]
    [InlineData(false, MarkType.Aga, 0, true, true, 1)]
    [InlineData(true, MarkType.Aga, 0, true, true, 2)]
    [InlineData(false, MarkType.Aga, 1, false, false, 0)]
    [InlineData(false, MarkType.Aga, 1, false, false, 1)]
    [InlineData(true, MarkType.Aga, 1, false, false, 2)]
    [InlineData(false, MarkType.Aga, 1, false, true, 0)]
    [InlineData(true, MarkType.Aga, 1, false, true, 1)]
    [InlineData(true, MarkType.Aga, 1, false, true, 2)]
    [InlineData(false, MarkType.Aga, 1, true, false, 0)]
    [InlineData(false, MarkType.Aga, 1, true, false, 1)]
    [InlineData(true, MarkType.Aga, 1, true, false, 2)]
    [InlineData(false, MarkType.Aga, 1, true, true, 0)]
    [InlineData(true, MarkType.Aga, 1, true, true, 1)]
    [InlineData(true, MarkType.Aga, 1, true, true, 2)]
    [InlineData(false, MarkType.Aga, 2, false, false, 0)]
    [InlineData(true, MarkType.Aga, 2, false, false, 1)]
    [InlineData(true, MarkType.Aga, 2, false, false, 2)]
    [InlineData(false, MarkType.Aga, 2, false, true, 0)]
    [InlineData(true, MarkType.Aga, 2, false, true, 1)]
    [InlineData(true, MarkType.Aga, 2, false, true, 2)]
    [InlineData(false, MarkType.Aga, 2, true, false, 0)]
    [InlineData(true, MarkType.Aga, 2, true, false, 1)]
    [InlineData(true, MarkType.Aga, 2, true, false, 2)]
    [InlineData(false, MarkType.Aga, 2, true, true, 0)]
    [InlineData(true, MarkType.Aga, 2, true, true, 1)]
    [InlineData(true, MarkType.Aga, 2, true, true, 2)]
    public void ShouldBeDisplayed_ShouldEqualExpected(
        bool expected, MarkType? mark, int accessible, bool sequenceBreak, bool visible, int available)
    {
        IMarking? marking = null;

        if (mark is not null)
        {
            _marking.Mark.Returns(mark.Value);
            marking = _marking;
        }
            
        _accessibilityProvider.Accessible.Returns(accessible);
        _accessibilityProvider.SequenceBreak.Returns(sequenceBreak);
        _accessibilityProvider.Visible.Returns(visible);
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, marking, _requirement) {Available = available};
            
        Assert.Equal(expected, sut.ShouldBeDisplayed);
    }

    [Theory]
    [InlineData(false, 1, 0)]
    [InlineData(true, 1, 1)]
    [InlineData(false, 2, 0)]
    [InlineData(true, 2, 1)]
    [InlineData(true, 2, 2)]
    [InlineData(false, 3, 0)]
    [InlineData(true, 3, 1)]
    [InlineData(true, 3, 2)]
    [InlineData(true, 3, 3)]
    public void IsAvailable_ShouldReturnExpected(bool expected, int total, int available)
    {
        _dungeon.Total.Returns(total);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = available};

        Assert.Equal(expected, sut.IsAvailable());
    }

    [Theory]
    [InlineData(false, null, 0, false, false, 0, false)]
    [InlineData(false, null, 0, false, false, 1, false)]
    [InlineData(false, null, 0, false, false, 2, false)]
    [InlineData(false, null, 0, true, false, 0, false)]
    [InlineData(false, null, 0, true, false, 1, false)]
    [InlineData(false, null, 0, true, false, 2, false)]
    [InlineData(false, null, 1, false, false, 0, false)]
    [InlineData(false, null, 1, false, false, 1, false)]
    [InlineData(true, null, 1, false, false, 2, false)]
    [InlineData(false, null, 1, true, false, 0, false)]
    [InlineData(false, null, 1, true, false, 1, false)]
    [InlineData(true, null, 1, true, false, 2, false)]
    [InlineData(false, null, 2, false, false, 0, false)]
    [InlineData(true, null, 2, false, false, 1, false)]
    [InlineData(true, null, 2, false, false, 2, false)]
    [InlineData(false, null, 2, true, false, 0, false)]
    [InlineData(true, null, 2, true, false, 1, false)]
    [InlineData(true, null, 2, true, false, 2, false)]
    [InlineData(false, MarkType.Unknown, 0, false, false, 0, false)]
    [InlineData(false, MarkType.Unknown, 0, false, false, 1, false)]
    [InlineData(false, MarkType.Unknown, 0, false, false, 2, false)]
    [InlineData(false, MarkType.Unknown, 0, false, true, 0, false)]
    [InlineData(false, MarkType.Unknown, 0, false, true, 1, false)]
    [InlineData(true, MarkType.Unknown, 0, false, true, 2, false)]
    [InlineData(false, MarkType.Unknown, 0, true, false, 0, false)]
    [InlineData(false, MarkType.Unknown, 0, true, false, 1, false)]
    [InlineData(false, MarkType.Unknown, 0, true, false, 2, false)]
    [InlineData(false, MarkType.Unknown, 0, true, true, 0, false)]
    [InlineData(false, MarkType.Unknown, 0, true, true, 1, false)]
    [InlineData(true, MarkType.Unknown, 0, true, true, 2, false)]
    [InlineData(false, MarkType.Unknown, 1, false, false, 0, false)]
    [InlineData(false, MarkType.Unknown, 1, false, false, 1, false)]
    [InlineData(true, MarkType.Unknown, 1, false, false, 2, false)]
    [InlineData(false, MarkType.Unknown, 1, false, true, 0, false)]
    [InlineData(true, MarkType.Unknown, 1, false, true, 1, false)]
    [InlineData(true, MarkType.Unknown, 1, false, true, 2, false)]
    [InlineData(false, MarkType.Unknown, 1, true, false, 0, false)]
    [InlineData(false, MarkType.Unknown, 1, true, false, 1, false)]
    [InlineData(true, MarkType.Unknown, 1, true, false, 2, false)]
    [InlineData(false, MarkType.Unknown, 1, true, true, 0, false)]
    [InlineData(true, MarkType.Unknown, 1, true, true, 1, false)]
    [InlineData(true, MarkType.Unknown, 1, true, true, 2, false)]
    [InlineData(false, MarkType.Unknown, 2, false, false, 0, false)]
    [InlineData(true, MarkType.Unknown, 2, false, false, 1, false)]
    [InlineData(true, MarkType.Unknown, 2, false, false, 2, false)]
    [InlineData(false, MarkType.Unknown, 2, false, true, 0, false)]
    [InlineData(true, MarkType.Unknown, 2, false, true, 1, false)]
    [InlineData(true, MarkType.Unknown, 2, false, true, 2, false)]
    [InlineData(false, MarkType.Unknown, 2, true, false, 0, false)]
    [InlineData(true, MarkType.Unknown, 2, true, false, 1, false)]
    [InlineData(true, MarkType.Unknown, 2, true, false, 2, false)]
    [InlineData(false, MarkType.Unknown, 2, true, true, 0, false)]
    [InlineData(true, MarkType.Unknown, 2, true, true, 1, false)]
    [InlineData(true, MarkType.Unknown, 2, true, true, 2, false)]
    [InlineData(false, MarkType.Aga, 0, false, false, 0, false)]
    [InlineData(false, MarkType.Aga, 0, false, false, 1, false)]
    [InlineData(false, MarkType.Aga, 0, false, false, 2, false)]
    [InlineData(false, MarkType.Aga, 0, false, true, 0, false)]
    [InlineData(false, MarkType.Aga, 0, false, true, 1, false)]
    [InlineData(false, MarkType.Aga, 0, false, true, 2, false)]
    [InlineData(false, MarkType.Aga, 0, true, false, 0, false)]
    [InlineData(false, MarkType.Aga, 0, true, false, 1, false)]
    [InlineData(false, MarkType.Aga, 0, true, false, 2, false)]
    [InlineData(false, MarkType.Aga, 0, true, true, 0, false)]
    [InlineData(false, MarkType.Aga, 0, true, true, 1, false)]
    [InlineData(false, MarkType.Aga, 0, true, true, 2, false)]
    [InlineData(false, MarkType.Aga, 1, false, false, 0, false)]
    [InlineData(false, MarkType.Aga, 1, false, false, 1, false)]
    [InlineData(true, MarkType.Aga, 1, false, false, 2, false)]
    [InlineData(false, MarkType.Aga, 1, false, true, 0, false)]
    [InlineData(false, MarkType.Aga, 1, false, true, 1, false)]
    [InlineData(true, MarkType.Aga, 1, false, true, 2, false)]
    [InlineData(false, MarkType.Aga, 1, true, false, 0, false)]
    [InlineData(false, MarkType.Aga, 1, true, false, 1, false)]
    [InlineData(true, MarkType.Aga, 1, true, false, 2, false)]
    [InlineData(false, MarkType.Aga, 1, true, true, 0, false)]
    [InlineData(false, MarkType.Aga, 1, true, true, 1, false)]
    [InlineData(true, MarkType.Aga, 1, true, true, 2, false)]
    [InlineData(false, MarkType.Aga, 2, false, false, 0, false)]
    [InlineData(true, MarkType.Aga, 2, false, false, 1, false)]
    [InlineData(true, MarkType.Aga, 2, false, false, 2, false)]
    [InlineData(false, MarkType.Aga, 2, false, true, 0, false)]
    [InlineData(true, MarkType.Aga, 2, false, true, 1, false)]
    [InlineData(true, MarkType.Aga, 2, false, true, 2, false)]
    [InlineData(false, MarkType.Aga, 2, true, false, 0, false)]
    [InlineData(true, MarkType.Aga, 2, true, false, 1, false)]
    [InlineData(true, MarkType.Aga, 2, true, false, 2, false)]
    [InlineData(false, MarkType.Aga, 2, true, true, 0, false)]
    [InlineData(true, MarkType.Aga, 2, true, true, 1, false)]
    [InlineData(true, MarkType.Aga, 2, true, true, 2, false)]
    [InlineData(false, null, 0, false, false, 0, true)]
    [InlineData(true, null, 0, false, false, 1, true)]
    [InlineData(true, null, 0, false, false, 2, true)]
    [InlineData(false, null, 0, true, false, 0, true)]
    [InlineData(true, null, 0, true, false, 1, true)]
    [InlineData(true, null, 0, true, false, 2, true)]
    [InlineData(false, null, 1, false, false, 0, true)]
    [InlineData(true, null, 1, false, false, 1, true)]
    [InlineData(true, null, 1, false, false, 2, true)]
    [InlineData(false, null, 1, true, false, 0, true)]
    [InlineData(true, null, 1, true, false, 1, true)]
    [InlineData(true, null, 1, true, false, 2, true)]
    [InlineData(false, null, 2, false, false, 0, true)]
    [InlineData(true, null, 2, false, false, 1, true)]
    [InlineData(true, null, 2, false, false, 2, true)]
    [InlineData(false, null, 2, true, false, 0, true)]
    [InlineData(true, null, 2, true, false, 1, true)]
    [InlineData(true, null, 2, true, false, 2, true)]
    [InlineData(false, MarkType.Unknown, 0, false, false, 0, true)]
    [InlineData(true, MarkType.Unknown, 0, false, false, 1, true)]
    [InlineData(true, MarkType.Unknown, 0, false, false, 2, true)]
    [InlineData(false, MarkType.Unknown, 0, false, true, 0, true)]
    [InlineData(true, MarkType.Unknown, 0, false, true, 1, true)]
    [InlineData(true, MarkType.Unknown, 0, false, true, 2, true)]
    [InlineData(false, MarkType.Unknown, 0, true, false, 0, true)]
    [InlineData(true, MarkType.Unknown, 0, true, false, 1, true)]
    [InlineData(true, MarkType.Unknown, 0, true, false, 2, true)]
    [InlineData(false, MarkType.Unknown, 0, true, true, 0, true)]
    [InlineData(true, MarkType.Unknown, 0, true, true, 1, true)]
    [InlineData(true, MarkType.Unknown, 0, true, true, 2, true)]
    [InlineData(false, MarkType.Unknown, 1, false, false, 0, true)]
    [InlineData(true, MarkType.Unknown, 1, false, false, 1, true)]
    [InlineData(true, MarkType.Unknown, 1, false, false, 2, true)]
    [InlineData(false, MarkType.Unknown, 1, false, true, 0, true)]
    [InlineData(true, MarkType.Unknown, 1, false, true, 1, true)]
    [InlineData(true, MarkType.Unknown, 1, false, true, 2, true)]
    [InlineData(false, MarkType.Unknown, 1, true, false, 0, true)]
    [InlineData(true, MarkType.Unknown, 1, true, false, 1, true)]
    [InlineData(true, MarkType.Unknown, 1, true, false, 2, true)]
    [InlineData(false, MarkType.Unknown, 1, true, true, 0, true)]
    [InlineData(true, MarkType.Unknown, 1, true, true, 1, true)]
    [InlineData(true, MarkType.Unknown, 1, true, true, 2, true)]
    [InlineData(false, MarkType.Unknown, 2, false, false, 0, true)]
    [InlineData(true, MarkType.Unknown, 2, false, false, 1, true)]
    [InlineData(true, MarkType.Unknown, 2, false, false, 2, true)]
    [InlineData(false, MarkType.Unknown, 2, false, true, 0, true)]
    [InlineData(true, MarkType.Unknown, 2, false, true, 1, true)]
    [InlineData(true, MarkType.Unknown, 2, false, true, 2, true)]
    [InlineData(false, MarkType.Unknown, 2, true, false, 0, true)]
    [InlineData(true, MarkType.Unknown, 2, true, false, 1, true)]
    [InlineData(true, MarkType.Unknown, 2, true, false, 2, true)]
    [InlineData(false, MarkType.Unknown, 2, true, true, 0, true)]
    [InlineData(true, MarkType.Unknown, 2, true, true, 1, true)]
    [InlineData(true, MarkType.Unknown, 2, true, true, 2, true)]
    [InlineData(false, MarkType.Aga, 0, false, false, 0, true)]
    [InlineData(true, MarkType.Aga, 0, false, false, 1, true)]
    [InlineData(true, MarkType.Aga, 0, false, false, 2, true)]
    [InlineData(false, MarkType.Aga, 0, false, true, 0, true)]
    [InlineData(true, MarkType.Aga, 0, false, true, 1, true)]
    [InlineData(true, MarkType.Aga, 0, false, true, 2, true)]
    [InlineData(false, MarkType.Aga, 0, true, false, 0, true)]
    [InlineData(true, MarkType.Aga, 0, true, false, 1, true)]
    [InlineData(true, MarkType.Aga, 0, true, false, 2, true)]
    [InlineData(false, MarkType.Aga, 0, true, true, 0, true)]
    [InlineData(true, MarkType.Aga, 0, true, true, 1, true)]
    [InlineData(true, MarkType.Aga, 0, true, true, 2, true)]
    [InlineData(false, MarkType.Aga, 1, false, false, 0, true)]
    [InlineData(true, MarkType.Aga, 1, false, false, 1, true)]
    [InlineData(true, MarkType.Aga, 1, false, false, 2, true)]
    [InlineData(false, MarkType.Aga, 1, false, true, 0, true)]
    [InlineData(true, MarkType.Aga, 1, false, true, 1, true)]
    [InlineData(true, MarkType.Aga, 1, false, true, 2, true)]
    [InlineData(false, MarkType.Aga, 1, true, false, 0, true)]
    [InlineData(true, MarkType.Aga, 1, true, false, 1, true)]
    [InlineData(true, MarkType.Aga, 1, true, false, 2, true)]
    [InlineData(false, MarkType.Aga, 1, true, true, 0, true)]
    [InlineData(true, MarkType.Aga, 1, true, true, 1, true)]
    [InlineData(true, MarkType.Aga, 1, true, true, 2, true)]
    [InlineData(false, MarkType.Aga, 2, false, false, 0, true)]
    [InlineData(true, MarkType.Aga, 2, false, false, 1, true)]
    [InlineData(true, MarkType.Aga, 2, false, false, 2, true)]
    [InlineData(false, MarkType.Aga, 2, false, true, 0, true)]
    [InlineData(true, MarkType.Aga, 2, false, true, 1, true)]
    [InlineData(true, MarkType.Aga, 2, false, true, 2, true)]
    [InlineData(false, MarkType.Aga, 2, true, false, 0, true)]
    [InlineData(true, MarkType.Aga, 2, true, false, 1, true)]
    [InlineData(true, MarkType.Aga, 2, true, false, 2, true)]
    [InlineData(false, MarkType.Aga, 2, true, true, 0, true)]
    [InlineData(true, MarkType.Aga, 2, true, true, 1, true)]
    [InlineData(true, MarkType.Aga, 2, true, true, 2, true)]
    public void CanBeCleared_ShouldEqualExpected(
        bool expected, MarkType? mark, int accessible, bool sequenceBreak, bool visible, int available, bool force)
    {
        IMarking? marking = null;

        if (mark is not null)
        {
            _marking.Mark.Returns(mark.Value);
            marking = _marking;
        }
            
        _accessibilityProvider.Accessible.Returns(accessible);
        _accessibilityProvider.SequenceBreak.Returns(sequenceBreak);
        _accessibilityProvider.Visible.Returns(visible);
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, marking, _requirement) {Available = available};
            
        Assert.Equal(expected, sut.CanBeCleared(force));
    }

    [Theory]
    [InlineData(0, 0, 0, false)]
    [InlineData(0, 0, 0, true)]
    [InlineData(1, 0, 1, false)]
    [InlineData(0, 0, 1, true)]
    [InlineData(2, 0, 2, false)]
    [InlineData(0, 0, 2, true)]
    [InlineData(0, 1, 0, false)]
    [InlineData(0, 1, 0, true)]
    [InlineData(1, 1, 1, false)]
    [InlineData(0, 1, 1, true)]
    [InlineData(1, 1, 2, false)]
    [InlineData(0, 1, 2, true)]
    [InlineData(0, 2, 0, false)]
    [InlineData(0, 2, 0, true)]
    [InlineData(0, 2, 1, false)]
    [InlineData(0, 2, 1, true)]
    [InlineData(0, 2, 2, false)]
    [InlineData(0, 2, 2, true)]
    public void Clear_ShouldSetAvailableToExpected(int expected, int accessible, int available, bool force)
    {
        _dungeon.Total.Returns(2);
        _accessibilityProvider.Accessible.Returns(accessible);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = available};
        sut.Clear(force);
            
        Assert.Equal(expected, sut.Available);
    }

    [Fact]
    public void CreateCollectSectionAction_ShouldReturnNewAction()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        var action = sut.CreateCollectSectionAction(false);
            
        Assert.NotNull(action as CollectSection);
    }

    [Theory]
    [InlineData(true, 0)]
    [InlineData(true, 1)]
    [InlineData(false, 2)]
    public void CanBeUncleared_ShouldReturnExpected(bool expected, int available)
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = available};

        Assert.Equal(expected, sut.CanBeUncleared());
    }

    [Fact]
    public void CreateUncollectSectionAction_ShouldReturnNewAction()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        var action = sut.CreateUncollectSectionAction();
            
        Assert.NotNull(action as UncollectSection);
    }

    [Fact]
    public void Reset_ShouldSetUserManipulatedToFalse()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {UserManipulated = true};
        sut.Reset();
            
        Assert.False(sut.UserManipulated);
    }

    [Fact]
    public void Reset_ShouldSetAvailableToTotal()
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = 0};
        sut.Reset();
            
        Assert.Equal(2, sut.Available);
    }

    [Fact]
    public void Reset_ShouldSetMarkingToUnknown()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
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
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {UserManipulated = userManipulated};
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.UserManipulated);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void Save_ShouldSetSaveDataAvailableToExpected(int expected, int available)
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement) {Available = available};
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.Available);
    }

    [Theory]
    [InlineData(MarkType.Aga, MarkType.Aga)]
    [InlineData(MarkType.Blacksmith, MarkType.Blacksmith)]
    public void Save_ShouldSetSaveDataMarkingToExpected(MarkType expected, MarkType marking)
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        sut.Marking!.Mark = marking;
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.Marking);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        sut.Load(null);
            
        Assert.Equal(2, sut.Available);
    }
        
    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Load_ShouldSetUserManipulatedToExpected(bool expected, bool userManipulated)
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        var saveData = new SectionSaveData {UserManipulated = userManipulated};
        sut.Load(saveData);
            
        Assert.Equal(expected, sut.UserManipulated);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Load_ShouldSetAvailableToExpected(int expected, int available)
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
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
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        var saveData = new SectionSaveData {Marking = marking};
        sut.Load(saveData);
            
        Assert.Equal(expected, sut.Marking!.Mark);
    }

    [Fact]
    public void RequirementChanged_ShouldUpdateIsActive()
    {
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _requirement.Met.Returns(true);
        _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
        Assert.True(sut.IsActive);
    }

    [Fact]
    public void DungeonChanged_ShouldUpdateTotal()
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _dungeon.Total.Returns(1);
        _dungeon.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _dungeon, new PropertyChangedEventArgs(nameof(IDungeon.Total)));
            
        Assert.Equal(1, sut.Total);
    }

    [Fact]
    public void AccessibilityProviderChanged_ShouldUpdateAccessibility_WhenAccessibleChanges()
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _accessibilityProvider.Accessible.Returns(2);
        _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _accessibilityProvider,
            new PropertyChangedEventArgs(nameof(IDungeonAccessibilityProvider.Accessible)));
            
        Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
    }

    [Fact]
    public void AccessibilityProviderChanged_ShouldUpdateAccessibility_WhenSequenceBreakChanges()
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _accessibilityProvider.Accessible.Returns(2);
        _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _accessibilityProvider,
            new PropertyChangedEventArgs(nameof(IDungeonAccessibilityProvider.SequenceBreak)));
            
        Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
    }

    [Fact]
    public void AccessibilityProviderChanged_ShouldUpdateAccessibility_WhenVisibleChanges()
    {
        _dungeon.Total.Returns(2);
        var sut = new DungeonItemSection(
            _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _dungeon, _accessibilityProvider,
            _autoTrackValue, _marking, _requirement);
        _accessibilityProvider.Accessible.Returns(2);
        _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _accessibilityProvider,
            new PropertyChangedEventArgs(nameof(IDungeonAccessibilityProvider.Visible)));
            
        Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDungeonItemSection.Factory>();
        var sut = factory(
            _dungeon, _accessibilityProvider, _autoTrackValue, _marking, _requirement);
            
        Assert.NotNull(sut as DungeonItemSection);
    }
}