using System;
using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Markings;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo.Locations;
using OpenTracker.Models.UndoRedo.Notes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations
{
    public class LocationTests
    {
        private const string Name = "Test";
        
        private readonly List<IMapLocation> _mapLocations = new();

        private readonly List<ISection> _sections = new()
        {
            Substitute.For<IEntranceSection>(),
            Substitute.For<IItemSection>()
        };
        
        private readonly ILocationNoteCollection _notes = new LocationNoteCollection();

        private readonly IMapLocationFactory _mapLocationFactory = Substitute.For<IMapLocationFactory>();
        private readonly ISectionFactory _sectionFactory = Substitute.For<ISectionFactory>();

        public LocationTests()
        {
            _mapLocationFactory.GetMapLocations(Arg.Any<ILocation>()).Returns(_mapLocations);
            _sectionFactory.GetSections(Arg.Any<LocationID>()).Returns(_sections);
        }

        [Theory]
        [InlineData(LocationID.LinksHouse, LocationID.LinksHouse)]
        [InlineData(LocationID.Pedestal, LocationID.Pedestal)]
        public void Ctor_ShouldSetIDToExpected(LocationID expected, LocationID id)
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(),
                _ => Substitute.For<IAddNote>(), (_, _) => Substitute.For<IRemoveNote>(),
                _notes, id, Name);
            
            Assert.Equal(expected, sut.ID);
        }

        [Fact]
        public void Ctor_ShouldSetNameToExpected()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(Name, sut.Name);
        }

        [Fact]
        public void Ctor_ShouldSetMapLocationsToExpected()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(_mapLocations, sut.MapLocations);
        }

        [Fact]
        public void Ctor_ShouldSetSectionsToExpected()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(_sections, sut.Sections);
        }

        [Fact]
        public void Ctor_ShouldSetNotesToExpected()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(_notes, sut.Notes);
        }
        
        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _sections[0].Accessibility.Returns(AccessibilityLevel.Normal);

            Assert.PropertyChanged(sut, nameof(ILocation.Accessibility), () => 
                _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _sections[0], new PropertyChangedEventArgs(nameof(ISection.Accessibility))));
        }

        [Theory]
        [InlineData(AccessibilityLevel.Cleared, false, false, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Cleared, false, false, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.None, false, true, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, false, true, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Partial, false, true, AccessibilityLevel.None, AccessibilityLevel.Partial)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, false, true, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, false, true, AccessibilityLevel.None, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.None, false, true, AccessibilityLevel.Normal, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, false, true, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Partial, false, true, AccessibilityLevel.Normal, AccessibilityLevel.Partial)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, false, true, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, false, true, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.None, true, false, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, true, false, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Partial, true, false, AccessibilityLevel.Partial, AccessibilityLevel.None)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, true, false, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Normal, true, false, AccessibilityLevel.Normal, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.None, true, false, AccessibilityLevel.None, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Inspect, true, false, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Partial, true, false, AccessibilityLevel.Partial, AccessibilityLevel.Normal)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, true, false, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Normal, true, false, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.None, true, true, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, true, true, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.None, AccessibilityLevel.Partial)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.None, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Inspect, true, true, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, true, true, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Inspect, AccessibilityLevel.Partial)]
        [InlineData(
            AccessibilityLevel.Partial, true, true, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Partial, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Partial, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Partial, AccessibilityLevel.Partial)]
        [InlineData(
            AccessibilityLevel.Partial, true, true, AccessibilityLevel.Partial, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Partial, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
        [InlineData(
            AccessibilityLevel.Partial, true, true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
        [InlineData(
            AccessibilityLevel.Partial, true, true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Partial)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, true, true, AccessibilityLevel.SequenceBreak,
            AccessibilityLevel.SequenceBreak)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, true, true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Normal, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Partial, true, true, AccessibilityLevel.Normal, AccessibilityLevel.Partial)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, true, true, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, true, true, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void Accessibility_ShouldMatchExpected(
            AccessibilityLevel expected, bool section1Available, bool section2Available, AccessibilityLevel section1,
            AccessibilityLevel section2)
        {
            _sections[0].Available.Returns(1);
            _sections[1].Available.Returns(1);

            if (section1Available)
            {
                _sections[0].IsAvailable().Returns(true);
                _sections[0].Requirement!.Met.Returns(true);
            }
            
            if (section2Available)
            {
                _sections[1].IsAvailable().Returns(true);
                _sections[1].Requirement!.Met.Returns(true);
            }

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            _sections[0].Accessibility.Returns(section1);
            _sections[1].Accessibility.Returns(section2);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(expected, sut.Accessibility);
        }

        [Fact]
        public void Accessibility_ShouldThrowException_WhenSectionAccessibilityIsUnexpected()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);

            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            _sections[0].Accessibility.Returns(AccessibilityLevel.Cleared);
            _sections[1].Accessibility.Returns(AccessibilityLevel.Cleared);
            
            Assert.Throws<Exception>(() => _ = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name));
        }

        [Fact]
        public void Accessible_ShouldRaisePropertyChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            ((IItemSection) _sections[1]).Accessible.Returns(1);

            Assert.PropertyChanged(sut, nameof(ILocation.Accessible), () => 
                _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _sections[0], new PropertyChangedEventArgs(nameof(IItemSection.Accessible))));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void Accessible_ShouldMatchExpected(int expected, int accessible)
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Total.Returns(1);
            ((IItemSection) _sections[1]).Accessible.Returns(accessible);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(expected, sut.Accessible);
        }

        [Fact]
        public void Total_ShouldRaisePropertyChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            ((IItemSection) _sections[1]).Total.Returns(1);
            
            Assert.PropertyChanged(sut, nameof(IItemSection.Total),
                () => _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _sections[0], new PropertyChangedEventArgs(nameof(IItemSection.Total))));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void Total_ShouldMatchExpected(int expected, int total)
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Available.Returns(total);
            ((IItemSection) _sections[1]).Total.Returns(total);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(expected, sut.Total);
        }
        
        [Fact]
        public void Visible_ShouldRaisePropertyChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _sections[0].Accessibility.Returns(AccessibilityLevel.Normal);

            Assert.PropertyChanged(sut, nameof(ILocation.Accessibility), () => 
                _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _sections[0], new PropertyChangedEventArgs(nameof(ISection.Accessibility))));
        }
        
        [Theory]
        [InlineData(false, false, false, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(true, true, true, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(true, true, true, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void Visible_ShouldMatchExpected(
            bool expected, bool section1Available, bool section2Available, AccessibilityLevel section1,
            AccessibilityLevel section2)
        {
            _sections[0].Available.Returns(1);
            _sections[1].Available.Returns(1);

            if (section1Available)
            {
                _sections[0].IsAvailable().Returns(true);
                _sections[0].Requirement!.Met.Returns(true);
            }
            
            if (section2Available)
            {
                _sections[1].IsAvailable().Returns(true);
                _sections[1].Requirement!.Met.Returns(true);
            }

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            _sections[0].Accessibility.Returns(section1);
            _sections[1].Accessibility.Returns(section2);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(expected, sut.Visible);
        }

        [Fact]
        public void SectionChanged_ShouldUpdateAccessibility_WhenSectionAccessibilityChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _sections[0].Accessibility.Returns(AccessibilityLevel.Normal);

            _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sections[0], new PropertyChangedEventArgs(nameof(ISection.Accessibility)));
            
            Assert.Equal(AccessibilityLevel.Partial, sut.Accessibility);
        }

        [Fact]
        public void SectionChanged_ShouldUpdateAccessibility_WhenSectionAvailableChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _sections[0].Accessibility.Returns(AccessibilityLevel.Normal);

            _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sections[0], new PropertyChangedEventArgs(nameof(ISection.Available)));
            
            Assert.Equal(AccessibilityLevel.Partial, sut.Accessibility);
        }

        [Fact]
        public void SectionChanged_ShouldUpdateAccessible_WhenSectionAccessibleChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            ((IItemSection) _sections[1]).Accessible.Returns(1);

            _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sections[0], new PropertyChangedEventArgs(nameof(IItemSection.Accessible)));
            
            Assert.Equal(1, sut.Accessible);
        }

        [Fact]
        public void SectionChanged_ShouldUpdateAvailable_WhenSectionAvailableChanged()
        {
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _sections[1].Available.Returns(1);

            _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sections[0], new PropertyChangedEventArgs(nameof(ISection.Available)));
            
            Assert.Equal(1, sut.Available);
        }

        [Fact]
        public void SectionChanged_ShouldUpdateTotal_WhenSectionTotalChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            ((IItemSection) _sections[1]).Total.Returns(1);
            
            _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sections[0], new PropertyChangedEventArgs(nameof(IItemSection.Total)));

            Assert.Equal(1, sut.Total);
        }
        
        [Fact]
        public void SectionChanged_ShouldUpdateVisible_WhenSectionAccessibilityChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _sections[0].Accessibility.Returns(AccessibilityLevel.Normal);

            _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sections[0], new PropertyChangedEventArgs(nameof(ISection.Accessibility)));
            
            Assert.True(sut.Visible);
        }

        [Fact]
        public void SectionChanged_ShouldUpdateVisible_WhenSectionAvailableChanged()
        {
            _sections[0].Available.Returns(1);
            _sections[0].IsAvailable().Returns(true);
            _sections[0].Requirement!.Met.Returns(true);
            _sections[1].Available.Returns(1);
            _sections[1].IsAvailable().Returns(true);
            _sections[1].Requirement!.Met.Returns(true);

            ((IItemSection) _sections[1]).Accessible.Returns(1);
            ((IItemSection) _sections[1]).Total.Returns(1);
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _sections[0].Accessibility.Returns(AccessibilityLevel.Normal);

            _sections[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sections[0], new PropertyChangedEventArgs(nameof(ISection.Available)));
            
            Assert.True(sut.Visible);
        }

        [Fact]
        public void CanBeCleared_ShouldCallCanBeClearedOnSections()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            _ = sut.CanBeCleared(false);

            _sections[0].Received().CanBeCleared(false);
            _sections[1].Received().CanBeCleared(false);
        }

        [Theory]
        [InlineData(false, false, false, false)]
        [InlineData(true, false, false, true)]
        [InlineData(true, false, true, false)]
        [InlineData(true, false, true, true)]
        [InlineData(false, true, false, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, true, true, false)]
        [InlineData(true, true, true, true)]
        public void CanBeCleared_ShouldReturnExpected(bool expected, bool force, bool section1, bool section2)
        {
            _sections[0].CanBeCleared(force).Returns(section1);
            _sections[1].CanBeCleared(force).Returns(section2);

            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            Assert.Equal(expected, sut.CanBeCleared(force));
        }

        [Fact]
        public void CreateAddNoteAction_ShouldReturnAction()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            Assert.NotNull(sut.CreateAddNoteAction());
        }

        [Fact]
        public void CreateRemoveNoteAction_ShouldReturnAction()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            Assert.NotNull(sut.CreateRemoveNoteAction(Substitute.For<IMarking>()));
        }

        [Fact]
        public void CreateClearLocationAction_ShouldReturnAction()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            Assert.NotNull(sut.CreateClearLocationAction());
        }

        [Fact]
        public void CreatePinLocationAction_ShouldReturnAction()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            Assert.NotNull(sut.CreatePinLocationAction());
        }

        [Fact]
        public void CreateUnpinLocationAction_ShouldReturnAction()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            Assert.NotNull(sut.CreateUnpinLocationAction());
        }

        [Fact]
        public void Reset_ShouldCallResetOnSections()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            sut.Reset();

            _sections[0].Received().Reset();
            _sections[1].Received().Reset();
        }

        [Fact]
        public void Save_ShouldReturnExpectedSectionSaveData()
        {
            var section1 = Substitute.For<SectionSaveData>();
            var section2 = Substitute.For<SectionSaveData>();

            _sections[0].Save().Returns(section1);
            _sections[1].Save().Returns(section2);

            var sections = new List<SectionSaveData>()
            {
                section1,
                section2
            }.ToExpectedObject();
            
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            var saveData = sut.Save();
            
            sections.ShouldEqual(saveData.Sections);
        }

        [Fact]
        public void Save_ShouldReturnExpectedMarkingSaveData()
        {
            const MarkType marking1 = MarkType.Aga;
            const MarkType marking2 = MarkType.Blacksmith;
            
            _notes.Add(Substitute.For<IMarking>());
            _notes.Add(Substitute.For<IMarking>());

            _notes[0].Mark.Returns(marking1);
            _notes[1].Mark.Returns(marking2);

            var markings = new List<MarkType?>
            {
                marking1,
                marking2
            }.ToExpectedObject();

            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);
            
            markings.ShouldEqual(sut.Save().Markings);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            sut.Load(null);
            
            _sections[0].DidNotReceive().Load(Arg.Any<SectionSaveData>());
            _sections[1].DidNotReceive().Load(Arg.Any<SectionSaveData>());
        }

        [Fact]
        public void Load_ShouldCallLoadOnSections()
        {
            var section1SaveData = new SectionSaveData();
            var section2SaveData = new SectionSaveData();

            var sections = new List<SectionSaveData>
            {
                section1SaveData,
                section2SaveData
            };

            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            var saveData = new LocationSaveData
            {
                Sections = sections
            };
            
            sut.Load(saveData);
            
            _sections[0].Received().Load(section1SaveData);
            _sections[1].Received().Load(section2SaveData);
        }

        [Fact]
        public void Load_ShouldEnsureNotesMatchSaveData()
        {
            const MarkType marking1 = MarkType.Aga;
            const MarkType marking2 = MarkType.Blacksmith;

            var markings = new List<MarkType?>
            {
                marking1,
                marking2
            };

            var sut = new Location(
                _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, LocationID.Pedestal, Name);

            var saveData = new LocationSaveData
            {
                Markings = markings
            };
            
            sut.Load(saveData);
            
            Assert.Collection(
                sut.Notes, marking => Assert.Equal(marking1, marking.Mark),
                marking => Assert.Equal(marking2, marking.Mark));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ILocation.Factory>();
            var sut = factory(LocationID.Pedestal, Name);
            
            Assert.NotNull(sut as Location);
        }
    }
}