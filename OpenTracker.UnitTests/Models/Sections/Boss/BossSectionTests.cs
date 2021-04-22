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
        public void CanBeCleared_ShouldAlwaysReturnFalse()
        {
            var sut = new BossSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _accessibilityProvider, "Test",
                _bossPlacement, _autoTrackValue, _requirement);

            Assert.False(sut.CanBeCleared());
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