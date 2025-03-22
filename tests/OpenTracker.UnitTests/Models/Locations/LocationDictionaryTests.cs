using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations
{
    public class LocationDictionaryTests
    {
        private readonly ILocationFactory _factory = Substitute.For<ILocationFactory>();

        private readonly LocationDictionary _sut;

        public LocationDictionaryTests()
        {
            _sut = new LocationDictionary(() => _factory);
        }

        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var location1 = _sut[LocationID.Pedestal];
            var location2 = _sut[LocationID.Pedestal];
            
            Assert.Equal(location1, location2);
        }

        [Fact]
        public void Indexer_ShouldReturnTheDifferentInstances()
        {
            var location1 = _sut[LocationID.Pedestal];
            var location2 = _sut[LocationID.LinksHouse];
            
            Assert.NotEqual(location1, location2);
        }

        [Fact]
        public void Reset_ShouldCallResetOnBossPlacements()
        {
            var location = _sut[LocationID.Pedestal];
            _sut.Reset();
            
            location.Received().Reset();
        }

        [Fact]
        public void Save_ShouldCallSaveOnBossPlacements()
        {
            var location = _sut[LocationID.Pedestal];
            _ = _sut.Save();
            
            location.Received().Save();
        }

        [Fact]
        public void Save_ShouldReturnDictionaryOfSaveData()
        {
            var location = _sut[LocationID.Pedestal];
            var locationSaveData = new LocationSaveData();
            location.Save().Returns(locationSaveData);
            var saveData = _sut.Save();

            Assert.Equal(locationSaveData, saveData[LocationID.Pedestal]);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var location = _sut[LocationID.Pedestal];
            _sut.Load(null);
            
            location.DidNotReceive().Load(Arg.Any<LocationSaveData>());
        }

        [Fact]
        public void Load_ShouldCallLoadOnBossPlacements()
        {
            var location = _sut[LocationID.Pedestal];
            var saveData = new Dictionary<LocationID, LocationSaveData>
            {
                { LocationID.Pedestal, new LocationSaveData() }
            };
            _sut.Load(saveData);
            
            location.Received().Load(Arg.Any<LocationSaveData>());
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ILocationDictionary>();
            
            Assert.NotNull(sut as LocationDictionary);
        }
    }
}