using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Boss
{
    public class BossSectionTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

        private readonly ICollectSection.Factory _collectSectionFactory = (section, force) =>
            new CollectSection(section, force);
        private readonly IUncollectSection.Factory _uncollectSectionFactory = section =>
            new UncollectSection(section);

        private readonly IBossAccessibilityProvider _accessibilityProvider =
            Substitute.For<IBossAccessibilityProvider>();
        private readonly IBossPlacement _bossPlacement = Substitute.For<IBossPlacement>();
        private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
        private readonly IRequirement _requirement = Substitute.For<IRequirement>();

        [Theory]
        [InlineData("Boss", "Boss")]
        [InlineData("Boss 1", "Boss 1")]
        [InlineData("Boss 2", "Boss 2")]
        [InlineData("Boss 3", "Boss 3")]
        [InlineData("Final Boss", "Final Boss")]
        public void Ctor_ShouldSetNameToExpected(string expected, string name)
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, name,
                _bossPlacement, _autoTrackValue, _requirement);
            
            Assert.Equal(expected, sut.Name);
        }

        [Fact]
        public void Ctor_ShouldAlwaysSetMarkingToNull()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);

            Assert.Null(sut.Marking);
        }

        [Fact]
        public void Ctor_ShouldSetBossPlacementToExpected()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            
            Assert.Equal(_bossPlacement, sut.BossPlacement);
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            _accessibilityProvider.Accessibility.Returns(AccessibilityLevel.Normal);
            
            Assert.PropertyChanged(sut, nameof(ISection.Accessibility), () =>
                _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _accessibilityProvider,
                    new PropertyChangedEventArgs(nameof(IBossAccessibilityProvider.Accessibility))));
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void Accessibility_ShouldEqualExpected(
            AccessibilityLevel expected, AccessibilityLevel providerAccessibility)
        {
            _accessibilityProvider.Accessibility.Returns(providerAccessibility);
            
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);

            Assert.Equal(expected, sut.Accessibility);
        }

        [Fact]
        public void IsActive_ShouldRaisePropertyChanged()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
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
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            _requirement.Met.Returns(requirementMet);
            _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met)));
            
            Assert.Equal(expected, sut.IsActive);
        }

        [Fact]
        public void IsAvailable_ShouldAlwaysReturnFalse()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            
            Assert.False(sut.IsAvailable());
        }

        [Fact]
        public void ShouldBeDisplayed_ShouldAlwaysReturnFalse()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            
            Assert.False(sut.ShouldBeDisplayed);
        }

        [Fact]
        public void CanBeCleared_ShouldAlwaysReturnFalse()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);

            Assert.False(sut.CanBeCleared());
        }

        [Fact]
        public void CreateCollectSectionAction_ShouldReturnNewAction()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            var action = sut.CreateCollectSectionAction(false);
            
            Assert.NotNull(action as CollectSection);
        }

        [Fact]
        public void CanBeUncleared_ShouldAlwaysReturnFalse()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);

            Assert.False(sut.CanBeUncleared());
        }

        [Fact]
        public void CreateUncollectSectionAction_ShouldReturnNewAction()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            var action = sut.CreateUncollectSectionAction();
            
            Assert.NotNull(action as UncollectSection);
        }

        [Fact]
        public void Reset_ShouldSetUserManipulatedToFalse()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement) {UserManipulated = true};
            sut.Reset();
            
            Assert.False(sut.UserManipulated);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldSetSaveDataUserManipulatedToExpected(bool expected, bool userManipulated)
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement) {UserManipulated = true};
            sut.UserManipulated = userManipulated;
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.UserManipulated);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement) {UserManipulated = true};
            sut.Load(null);
            
            Assert.True(sut.UserManipulated);
        }
        
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetUserManipulatedToExpected(bool expected, bool userManipulated)
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement) {UserManipulated = true};
            var saveData = new SectionSaveData {UserManipulated = userManipulated};
            sut.Load(saveData);
            
            Assert.Equal(expected, sut.UserManipulated);
        }

        [Fact]
        public void AccessibilityProviderChanged_ShouldUpdateAccessibility()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);
            _accessibilityProvider.Accessibility.Returns(AccessibilityLevel.Normal);
            _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _accessibilityProvider,
                new PropertyChangedEventArgs(nameof(IBossAccessibilityProvider.Accessibility)));

            Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IBossSection.Factory>();
            var sut = factory(_accessibilityProvider, "Test", _bossPlacement, _requirement);
            
            Assert.NotNull(sut as BossSection);
        }
    }
}