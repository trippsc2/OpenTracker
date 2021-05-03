using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations
{
    public class PinnedLocationCollectionTests
    {
        private readonly ILocationDictionary _locations = Substitute.For<ILocationDictionary>();

        private readonly PinnedLocationCollection _sut;

        public PinnedLocationCollectionTests()
        {
            _sut = new PinnedLocationCollection(_locations);
        }
        
        [Fact]
        public void Save_ShouldReturnListExpectedListOfLocationIDs()
        {
            const LocationID id1 = LocationID.Blacksmith;
            const LocationID id2 = LocationID.Catfish;

            var location1 = Substitute.For<ILocation>();
            var location2 = Substitute.For<ILocation>();
            location1.ID.Returns(id1);
            location2.ID.Returns(id2);

            _locations[id1].Returns(location1);
            _locations[id2].Returns(location2);
            
            _sut.Add(location1);
            _sut.Add(location2);

            var saveData = _sut.Save();
            
            Assert.Collection(
                saveData, id => Assert.Equal(id1, id),
                id => Assert.Equal(id2, id));
        }
        
        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            const LocationID id1 = LocationID.Blacksmith;
            const LocationID id2 = LocationID.Catfish;

            var location1 = Substitute.For<ILocation>();
            var location2 = Substitute.For<ILocation>();
            location1.ID.Returns(id1);
            location2.ID.Returns(id2);

            _locations[id1].Returns(location1);
            _locations[id2].Returns(location2);
            
            _sut.Add(location1);
            _sut.Add(location2);
            
            _sut.Load(null);
            
            Assert.Collection(
                _sut, location => Assert.Equal(location1, location),
                location => Assert.Equal(location2, location));
        }
        
        [Fact]
        public void Load_ShouldAddLocationsToCollection()
        {
            const LocationID id1 = LocationID.Blacksmith;
            const LocationID id2 = LocationID.Catfish;

            var saveData = new List<LocationID> {id1, id2};

            var location1 = Substitute.For<ILocation>();
            var location2 = Substitute.For<ILocation>();
            location1.ID.Returns(id1);
            location2.ID.Returns(id2);

            _locations[id1].Returns(location1);
            _locations[id2].Returns(location2);
            
            _sut.Load(saveData);
            
            Assert.Collection(
                _sut, location => Assert.Equal(location1, location),
                location => Assert.Equal(location2, location));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IPinnedLocationCollection>();
            
            Assert.NotNull(sut as PinnedLocationCollection);
        }
    }
}