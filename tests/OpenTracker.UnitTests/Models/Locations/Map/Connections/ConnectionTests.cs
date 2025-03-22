using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.UndoRedo.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations.Map.Connections
{
    public class ConnectionTests
    {
        private readonly IRemoveMapConnection.Factory _removeConnectionFactory = _ => Substitute.For<IRemoveMapConnection>();
        
        private readonly List<IMapLocation> _mapLocations;

        public ConnectionTests()
        {
            _mapLocations = new List<IMapLocation>
            {
                Substitute.For<IMapLocation>(),
                Substitute.For<IMapLocation>(),
                Substitute.For<IMapLocation>()
            };

            for (var i = 0; i < _mapLocations.Count; i++)
            {
                var id = (LocationID)i;
                var index = i;
                

                var mapLocation = _mapLocations[i];

                var mapLocations = new List<IMapLocation>
                {
                    Substitute.For<IMapLocation>(),
                    Substitute.For<IMapLocation>()
                };
                mapLocations.Insert(index, mapLocation);

                var location = Substitute.For<ILocation>();
                location.MapLocations.Returns(mapLocations);
                location.ID.Returns(id);
                
                mapLocation.Location.Returns(location);
            }
        }
        
        [Theory]
        [InlineData(true, 0, 1, 0, 1)]
        [InlineData(true, 0, 1, 1, 0)]
        [InlineData(false, 0, 1, 0, 2)]
        [InlineData(false, 0, 1, 2, 0)]
        [InlineData(false, 0, 1, 1, 2)]
        [InlineData(false, 0, 1, 2, 1)]
        [InlineData(true, 1, 0, 0, 1)]
        [InlineData(true, 1, 0, 1, 0)]
        [InlineData(false, 1, 0, 0, 2)]
        [InlineData(false, 1, 0, 2, 0)]
        [InlineData(false, 1, 0, 1, 2)]
        [InlineData(false, 1, 0, 2, 1)]
        [InlineData(false, 0, 2, 0, 1)]
        [InlineData(false, 0, 2, 1, 0)]
        [InlineData(true, 0, 2, 0, 2)]
        [InlineData(true, 0, 2, 2, 0)]
        [InlineData(false, 0, 2, 1, 2)]
        [InlineData(false, 0, 2, 2, 1)]
        [InlineData(false, 2, 0, 0, 1)]
        [InlineData(false, 2, 0, 1, 0)]
        [InlineData(true, 2, 0, 0, 2)]
        [InlineData(true, 2, 0, 2, 0)]
        [InlineData(false, 2, 0, 1, 2)]
        [InlineData(false, 2, 0, 2, 1)]
        [InlineData(false, 1, 2, 0, 1)]
        [InlineData(false, 1, 2, 1, 0)]
        [InlineData(false, 1, 2, 0, 2)]
        [InlineData(false, 1, 2, 2, 0)]
        [InlineData(true, 1, 2, 1, 2)]
        [InlineData(true, 1, 2, 2, 1)]
        [InlineData(false, 2, 1, 0, 1)]
        [InlineData(false, 2, 1, 1, 0)]
        [InlineData(false, 2, 1, 0, 2)]
        [InlineData(false, 2, 1, 2, 0)]
        [InlineData(true, 2, 1, 1, 2)]
        [InlineData(true, 2, 1, 2, 1)]
        public void Equals_ShouldReturnTrue_WhenLocationPairIsTheSameInAnyOrder(
            bool expected, int connection1Location1Index, int connection1Location2Index, int connection2Location1Index,
            int connection2Location2Index)
        {
            var connection1 = new MapConnection(_removeConnectionFactory,
                _mapLocations[connection1Location1Index], _mapLocations[connection1Location2Index]);
            var connection2 = new MapConnection(_removeConnectionFactory,
                _mapLocations[connection2Location1Index], _mapLocations[connection2Location2Index]);
            
            Assert.Equal(expected, connection1.Equals(connection2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenParameterIsNull()
        {
            var connection = new MapConnection(
                _removeConnectionFactory, _mapLocations[0], _mapLocations[1]);
            
            Assert.False(connection.Equals(null));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenParameterIsNotConnection()
        {
            var connection = new MapConnection(
                _removeConnectionFactory, _mapLocations[0], _mapLocations[1]);
            
            Assert.False(connection.Equals(new object()));
        }
        
        [Theory]
        [InlineData(true, 0, 1, 0, 1)]
        [InlineData(true, 0, 1, 1, 0)]
        [InlineData(false, 0, 1, 0, 2)]
        [InlineData(false, 0, 1, 2, 0)]
        [InlineData(false, 0, 1, 1, 2)]
        [InlineData(false, 0, 1, 2, 1)]
        [InlineData(true, 1, 0, 0, 1)]
        [InlineData(true, 1, 0, 1, 0)]
        [InlineData(false, 1, 0, 0, 2)]
        [InlineData(false, 1, 0, 2, 0)]
        [InlineData(false, 1, 0, 1, 2)]
        [InlineData(false, 1, 0, 2, 1)]
        [InlineData(false, 0, 2, 0, 1)]
        [InlineData(false, 0, 2, 1, 0)]
        [InlineData(true, 0, 2, 0, 2)]
        [InlineData(true, 0, 2, 2, 0)]
        [InlineData(false, 0, 2, 1, 2)]
        [InlineData(false, 0, 2, 2, 1)]
        [InlineData(false, 2, 0, 0, 1)]
        [InlineData(false, 2, 0, 1, 0)]
        [InlineData(true, 2, 0, 0, 2)]
        [InlineData(true, 2, 0, 2, 0)]
        [InlineData(false, 2, 0, 1, 2)]
        [InlineData(false, 2, 0, 2, 1)]
        [InlineData(false, 1, 2, 0, 1)]
        [InlineData(false, 1, 2, 1, 0)]
        [InlineData(false, 1, 2, 0, 2)]
        [InlineData(false, 1, 2, 2, 0)]
        [InlineData(true, 1, 2, 1, 2)]
        [InlineData(true, 1, 2, 2, 1)]
        [InlineData(false, 2, 1, 0, 1)]
        [InlineData(false, 2, 1, 1, 0)]
        [InlineData(false, 2, 1, 0, 2)]
        [InlineData(false, 2, 1, 2, 0)]
        [InlineData(true, 2, 1, 1, 2)]
        [InlineData(true, 2, 1, 2, 1)]
        public void GetHashCode_ShouldReturnSameValue_WhenLocationPairIsTheSameInAnyOrder(
            bool expected, int connection1Location1Index, int connection1Location2Index, int connection2Location1Index,
            int connection2Location2Index)
        {
            var connection1 = new MapConnection(_removeConnectionFactory,
                _mapLocations[connection1Location1Index], _mapLocations[connection1Location2Index]);
            var connection2 = new MapConnection(_removeConnectionFactory,
                _mapLocations[connection2Location1Index], _mapLocations[connection2Location2Index]);
            
            Assert.Equal(expected, connection1.GetHashCode() == connection2.GetHashCode());
        }

        [Fact]
        public void RemoveConnection_ShouldReturnNewRemoveConnection()
        {
            var sut = new MapConnection(_removeConnectionFactory, _mapLocations[0],
                _mapLocations[1]);
            var removeConnection = sut.CreateRemoveConnectionAction();
            
            Assert.NotNull(removeConnection);
        }

        [Theory]
        [InlineData(LocationID.LinksHouse, 0, 1)]
        [InlineData(LocationID.LinksHouse, 0, 2)]
        [InlineData(LocationID.Pedestal, 1, 0)]
        [InlineData(LocationID.Pedestal, 1, 2)]
        [InlineData(LocationID.LumberjackCave, 2, 0)]
        [InlineData(LocationID.LumberjackCave, 2, 1)]
        public void Save_ShouldReturnSaveDataLocation1EqualsIDOfLocation1Property(
            LocationID expected, int location1Index, int location2Index)
        {
            var sut = new MapConnection(_removeConnectionFactory, _mapLocations[location1Index],
                _mapLocations[location2Index]);
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.Location1);
        }
        
        [Theory]
        [InlineData(LocationID.Pedestal, 0, 1)]
        [InlineData(LocationID.LumberjackCave, 0, 2)]
        [InlineData(LocationID.LinksHouse, 1, 0)]
        [InlineData(LocationID.LumberjackCave, 1, 2)]
        [InlineData(LocationID.LinksHouse, 2, 0)]
        [InlineData(LocationID.Pedestal, 2, 1)]
        public void Save_ShouldReturnSaveDataLocation2EqualsIDOfLocation2Property(
            LocationID expected, int location1Index, int location2Index)
        {
            var sut = new MapConnection(_removeConnectionFactory, _mapLocations[location1Index],
                _mapLocations[location2Index]);
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.Location2);
        }

        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 0)]
        [InlineData(2, 2, 1)]
        public void Save_ShouldReturnSaveDataIndex1EqualsIndexOfLocation1Property(
            int expected, int location1Index, int location2Index)
        {
            var sut = new MapConnection(_removeConnectionFactory, _mapLocations[location1Index],
                _mapLocations[location2Index]);
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.Index1);
        }
        
        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(0, 1, 0)]
        [InlineData(2, 1, 2)]
        [InlineData(0, 2, 0)]
        [InlineData(1, 2, 1)]
        public void Save_ShouldReturnSaveDataIndex2EqualsIndexOfLocation2Property(
            int expected, int location1Index, int location2Index)
        {
            var sut = new MapConnection(_removeConnectionFactory, _mapLocations[location1Index],
                _mapLocations[location2Index]);
            var saveData = sut.Save();
            
            Assert.Equal(expected, saveData.Index2);
        }
        
        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 0)]
        [InlineData(2, 2, 1)]
        public void Save_SaveDataShouldAllowLookupOfLocation1(int expected, int location1Index, int location2Index)
        {
            var sut = new MapConnection(_removeConnectionFactory, _mapLocations[location1Index],
                _mapLocations[location2Index]);
            var saveData = sut.Save();
            
            Assert.Equal(
                _mapLocations[expected], _mapLocations[expected].Location.MapLocations[saveData.Index1]);
        }
        
        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(0, 1, 0)]
        [InlineData(2, 1, 2)]
        [InlineData(0, 2, 0)]
        [InlineData(1, 2, 1)]
        public void Save_SaveDataShouldAllowLookupOfLocation2(int expected, int location1Index, int location2Index)
        {
            var sut = new MapConnection(_removeConnectionFactory, _mapLocations[location1Index],
                _mapLocations[location2Index]);
            var saveData = sut.Save();
            
            Assert.Equal(
                _mapLocations[expected], _mapLocations[expected].Location.MapLocations[saveData.Index2]);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IMapConnection.Factory>();
            var sut = factory(_mapLocations[0], _mapLocations[1]);
            
            Assert.NotNull(sut as MapConnection);
        }
    }
}