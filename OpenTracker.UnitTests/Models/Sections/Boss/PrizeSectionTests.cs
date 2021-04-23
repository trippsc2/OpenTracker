using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Boss
{
    public class PrizeSectionTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

        private readonly ICollectSection.Factory _collectSectionFactory = (section, force) =>
            new CollectSection(section, force);
        private readonly IUncollectSection.Factory _uncollectSectionFactory = section =>
            new UncollectSection(section);
        private readonly ITogglePrizeSection.Factory _togglePrizeSectionFactory = (section, force) =>
            new TogglePrizeSection(section, force);

        private readonly IBossAccessibilityProvider _accessibilityProvider =
            Substitute.For<IBossAccessibilityProvider>();
        private readonly IBossPlacement _bossPlacement = Substitute.For<IBossPlacement>();
        private readonly IPrizePlacement _prizePlacement = Substitute.For<IPrizePlacement>();
        private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();

        [Theory]
        [InlineData("Boss", "Boss")]
        [InlineData("Boss 1", "Boss 1")]
        [InlineData("Boss 2", "Boss 2")]
        [InlineData("Boss 3", "Boss 3")]
        [InlineData("Final Boss", "Final Boss")]
        public void Ctor_ShouldSetNameToExpected(string expected, string name)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, name, _bossPlacement, _prizePlacement, _autoTrackValue);
            
            Assert.Equal(expected, sut.Name);
        }

        [Fact]
        public void Ctor_ShouldSetBossPlacementToExpected()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            
            Assert.Equal(_bossPlacement, sut.BossPlacement);
        }

        [Fact]
        public void Ctor_ShouldSetPrizePlacementToExpected()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            
            Assert.Equal(_prizePlacement, sut.PrizePlacement);
        }

        [Fact]
        public void Ctor_ShouldSetAvailableTo1()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            
            Assert.Equal(1, sut.Available);
        }

        [Fact]
        public void Ctor_ShouldSetTotalTo1()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            
            Assert.Equal(1, sut.Total);
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
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
            
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);

            Assert.Equal(expected, sut.Accessibility);
        }

        [Fact]
        public void IsActive_ShouldAlwaysReturnTrue()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            
            Assert.True(sut.IsActive);
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
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue)
            {
                Available = available
            };
            _accessibilityProvider.Accessibility.Returns(nodeAccessibility);
            _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _accessibilityProvider,
                new PropertyChangedEventArgs(nameof(IBossAccessibilityProvider.Accessibility)));
            
            Assert.Equal(expected, sut.ShouldBeDisplayed);
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void IsAvailable_ShouldReturnExpected(bool expected, int available)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue)
            {
                Available = available
            };

            Assert.Equal(expected, sut.IsAvailable());
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
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue)
            {
                Available = available
            };
            _accessibilityProvider.Accessibility.Returns(accessibility);

            _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _accessibilityProvider,
                new PropertyChangedEventArgs(nameof(IBossAccessibilityProvider.Accessibility)));
            
            Assert.Equal(expected, sut.CanBeCleared(force));
        }

        [Fact]
        public void Clear_ShouldSetAvailableTo0()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            sut.Clear(false);
            
            Assert.Equal(0, sut.Available);
        }

        [Fact]
        public void CreateCollectSectionAction_ShouldReturnNewAction()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            var action = sut.CreateCollectSectionAction(false);
            
            Assert.NotNull(action as CollectSection);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        public void CanBeUncleared_ShouldReturnExpected(bool expected, int available)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue)
            {
                Available = available
            };

            Assert.Equal(expected, sut.CanBeUncleared());
        }

        [Fact]
        public void CreateUncollectSectionAction_ShouldReturnNewAction()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            var action = sut.CreateUncollectSectionAction();
            
            Assert.NotNull(action as UncollectSection);
        }

        [Fact]
        public void CreateTogglePrizeSectionAction_ShouldReturnNewAction()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            var action = sut.CreateTogglePrizeSectionAction(false);
            
            Assert.NotNull(action as TogglePrizeSection);
        }

        [Fact]
        public void Reset_ShouldSetUserManipulatedToFalse()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue)
            {
                UserManipulated = true
            };
            sut.Reset();
            
            Assert.False(sut.UserManipulated);
        }

        [Fact]
        public void Reset_ShouldSetAvailableTo1()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue) {Available = 0};
            sut.Reset();
            
            Assert.Equal(1, sut.Available);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldSetSaveDataUserManipulatedToExpected(bool expected, bool userManipulated)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue)
            {
                UserManipulated = userManipulated
            };
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.UserManipulated);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        public void Save_ShouldSetSaveDataAvailableToExpected(int expected, int available)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue)
            {
                Available = available
            };
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.Available);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            sut.Load(null);
            
            Assert.Equal(1, sut.Available);
        }
        
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetUserManipulatedToExpected(bool expected, bool userManipulated)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            var saveData = new SectionSaveData {UserManipulated = userManipulated};
            sut.Load(saveData);
            
            Assert.Equal(expected, sut.UserManipulated);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        public void Load_ShouldSetAvailableToExpected(int expected, int available)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            var saveData = new SectionSaveData {Available = available};
            sut.Load(saveData);
            
            Assert.Equal(expected, sut.Available);
        }

        [Fact]
        public void AccessibilityProviderChanged_ShouldUpdateAccessibility()
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            _accessibilityProvider.Accessibility.Returns(AccessibilityLevel.Normal);
            _accessibilityProvider.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _accessibilityProvider,
                new PropertyChangedEventArgs(nameof(IBossAccessibilityProvider.Accessibility)));

            Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void AutoTrackValueChanged_ShouldSetAvailableToExpected(int expected, int? autoTrackValue)
        {
            var sut = new PrizeSection(
                _saveLoadManager, _collectSectionFactory, _uncollectSectionFactory, _togglePrizeSectionFactory,
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            _autoTrackValue.CurrentValue.Returns(autoTrackValue);
            _autoTrackValue.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _autoTrackValue, new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue)));
            
            Assert.Equal(expected, sut.Available);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IPrizeSection.Factory>();
            var sut = factory(
                _accessibilityProvider, "Test", _bossPlacement, _prizePlacement, _autoTrackValue);
            
            Assert.NotNull(sut as PrizeSection);
        }
    }
}