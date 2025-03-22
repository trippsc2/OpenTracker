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
using OpenTracker.Models.UndoRedo.Markings;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Entrance
{
    public class EntranceSectionTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

        private readonly ICollectSection.Factory _collectSectionFactory = (section, force) =>
            new CollectSection(section, force);
        private readonly IUncollectSection.Factory _uncollectSectionFactory = section =>
            new UncollectSection(section);
        private readonly IMarking.Factory _markingFactory = () => new Marking((marking, newMarking) => 
            new ChangeMarking(marking, newMarking));

        private readonly IRequirement _requirement = Substitute.For<IRequirement>();
        private readonly INode _node = Substitute.For<INode>();
        private readonly IOverworldNode _exitProvided = Substitute.For<IOverworldNode>();
        
        [Theory]
        [InlineData("Cave", "Cave")]
        [InlineData("House", "House")]
        public void Ctor_ShouldSetNameToExpected(string expected, string name)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, name,
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            
            Assert.Equal(expected, sut.Name);
        }

        [Fact]
        public void Ctor_ShouldAlwaysSetMarking()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);

            Assert.NotNull(sut.Marking);
        }

        [Fact]
        public void Ctor_ShouldSetAvailableTo1()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);

            Assert.Equal(1, sut.Available);
        }

        [Fact]
        public void Ctor_ShouldSetTotalTo1()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);

            Assert.Equal(1, sut.Total);
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);

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
        public void Accessibility_ShouldEqualExpected(AccessibilityLevel expected, AccessibilityLevel nodeAccessibility)
        {
            _node.Accessibility.Returns(nodeAccessibility);
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            
            Assert.Equal(expected, sut.Accessibility);
        }

        [Fact]
        public void Available_ShouldRaisePropertyChanged()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            Assert.PropertyChanged(sut, nameof(ISection.Available), () => sut.Available = 0);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void Available_ShouldManipulateDungeonExitsAccessible_WhenEntranceShuffleLevelIsDungeon(
            int expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {Available = 0};
            sut.Available = 1;
            sut.Available = available;
            
            Assert.Equal(expected, _exitProvided.DungeonExitsAccessible);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void Available_ShouldManipulateAllExitsAccessible_WhenEntranceShuffleLevelIsAll(
            int expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.All, _requirement, _node, _exitProvided) {Available = 0};
            sut.Available = 1;
            sut.Available = available;
            
            Assert.Equal(expected, _exitProvided.AllExitsAccessible);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void Available_ShouldManipulateInsanityExitsAccessible_WhenEntranceShuffleLevelIsInsanity(
            int expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Insanity, _requirement, _node, _exitProvided) {Available = 0};
            sut.Available = 1;
            sut.Available = available;
            
            Assert.Equal(expected, _exitProvided.InsanityExitsAccessible);
        }

        [Fact]
        public void IsActive_ShouldRaisePropertyChanged()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            _requirement.Met.Returns(true);
            
            Assert.PropertyChanged(sut, nameof(ISection.IsActive), () =>
                _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void IsActive_ShouldReturnExpected(bool expected, bool requirementMet)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            _requirement.Met.Returns(requirementMet);
            _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
            Assert.Equal(expected, sut.IsActive);
        }

        [Fact]
        public void ShouldBeDisplayed_ShouldRaisePropertyChanged()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            
            Assert.PropertyChanged(sut, nameof(ISection.ShouldBeDisplayed), () => sut.Available = 0);
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void ShouldBeDisplayed_ShouldEqualExpected(bool expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {Available = available};

            Assert.Equal(expected, sut.ShouldBeDisplayed);
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void IsAvailable_ShouldReturnExpected(bool expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {Available = available};

            Assert.Equal(expected, sut.IsAvailable());
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanBeCleared_ShouldReturnExpected(bool expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {Available = available};

            Assert.Equal(expected, sut.CanBeCleared());
        }

        [Fact]
        public void Clear_ShouldSetAvailableTo0()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            sut.Clear(false);
            
            Assert.Equal(0, sut.Available);
        }

        [Fact]
        public void CreateCollectSectionAction_ShouldReturnNewAction()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            var action = sut.CreateCollectSectionAction(false);
            
            Assert.NotNull(action as CollectSection);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        public void CanBeUncleared_ShouldReturnExpected(bool expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {Available = available};

            Assert.Equal(expected, sut.CanBeUncleared());
        }

        [Fact]
        public void CreateUncollectSectionAction_ShouldReturnNewAction()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            var action = sut.CreateUncollectSectionAction();
            
            Assert.NotNull(action as UncollectSection);
        }

        [Fact]
        public void Reset_ShouldSetUserManipulatedToFalse()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {UserManipulated = true};
            sut.Reset();
            
            Assert.False(sut.UserManipulated);
        }

        [Fact]
        public void Reset_ShouldSetAvailableTo1()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {Available = 0};
            sut.Reset();
            
            Assert.Equal(1, sut.Available);
        }

        [Fact]
        public void Reset_ShouldSetMarkingToUnknown()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            sut.Marking!.Mark = MarkType.Aga;
            sut.Reset();
            
            Assert.Equal(MarkType.Unknown, sut.Marking!.Mark);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldSetSaveDataUserManipulatedToExpected(bool expected, bool userManipulated)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {UserManipulated = userManipulated};
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.UserManipulated);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        public void Save_ShouldSetSaveDataAvailableToExpected(int expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided) {Available = available};
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.Available);
        }

        [Theory]
        [InlineData(MarkType.Aga, MarkType.Aga)]
        [InlineData(MarkType.Blacksmith, MarkType.Blacksmith)]
        public void Save_ShouldSetSaveDataMarkingToExpected(MarkType expected, MarkType marking)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            sut.Marking!.Mark = marking;
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.Marking);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            sut.Load(null);
            
            Assert.Equal(1, sut.Available);
        }
        
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetUserManipulatedToExpected(bool expected, bool userManipulated)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            var saveData = new SectionSaveData {UserManipulated = userManipulated};
            sut.Load(saveData);
            
            Assert.Equal(expected, sut.UserManipulated);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        public void Load_ShouldSetAvailableToExpected(int expected, int available)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            var saveData = new SectionSaveData {Available = available};
            sut.Load(saveData);
            
            Assert.Equal(expected, sut.Available);
        }

        [Theory]
        [InlineData(MarkType.Aga, MarkType.Aga)]
        [InlineData(MarkType.Blacksmith, MarkType.Blacksmith)]
        public void Load_ShouldSetMarkingToExpected(MarkType expected, MarkType marking)
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            var saveData = new SectionSaveData {Marking = marking};
            sut.Load(saveData);
            
            Assert.Equal(expected, sut.Marking!.Mark);
        }

        [Fact]
        public void RequirementChanged_ShouldUpdateIsActive()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            _requirement.Met.Returns(true);
            _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
            Assert.True(sut.IsActive);
        }

        [Fact]
        public void NodeChanged_ShouldUpdateAccessibility()
        {
            var sut = new EntranceSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _markingFactory, "Test",
                EntranceShuffle.Dungeon, _requirement, _node, _exitProvided);
            _node.Accessibility.Returns(AccessibilityLevel.Normal);
            _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(INode.Accessibility)));
            
            Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IEntranceSection.Factory>();
            var sut = factory(
                "Test", EntranceShuffle.Dungeon, Substitute.For<IRequirement>(),
                Substitute.For<INode>(), Substitute.For<IOverworldNode>());
            
            Assert.NotNull(sut as EntranceSection);
        }
    }
}