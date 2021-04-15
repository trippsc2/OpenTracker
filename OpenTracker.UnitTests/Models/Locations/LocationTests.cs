using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
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
        private readonly List<ISection> _sections = new();
        private readonly ILocationNoteCollection _notes = new LocationNoteCollection();

        private readonly ILocationFactory _factory = Substitute.For<ILocationFactory>();
        private readonly IMapLocationFactory _mapLocationFactory = Substitute.For<IMapLocationFactory>();
        private readonly ISectionFactory _sectionFactory = Substitute.For<ISectionFactory>();

        public LocationTests()
        {
            _factory.GetLocationName(Arg.Any<LocationID>()).Returns(Name);
            _mapLocationFactory.GetMapLocations(Arg.Any<ILocation>()).Returns(_mapLocations);
            _sectionFactory.GetSections(Arg.Any<LocationID>()).Returns(_sections);
        }

        [Theory]
        [InlineData(LocationID.LinksHouse, LocationID.LinksHouse)]
        [InlineData(LocationID.Pedestal, LocationID.Pedestal)]
        public void Ctor_ShouldSetIDToParameter(LocationID expected, LocationID id)
        {
            var sut = new Location(
                _factory, _mapLocationFactory, _sectionFactory, () => Substitute.For<IMarking>(),
                (_, _) => Substitute.For<IClearLocation>(),
                _ => Substitute.For<IPinLocation>(),
                _ => Substitute.For<IUnpinLocation>(), _ => Substitute.For<IAddNote>(),
                (_, _) => Substitute.For<IRemoveNote>(), _notes, id);
            
            Assert.Equal(expected, sut.ID);
        }
    }
}