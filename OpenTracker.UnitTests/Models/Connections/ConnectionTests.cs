using System;
using Autofac;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Locations;
using Xunit;

namespace OpenTracker.UnitTests.Models.Connections
{
    public class ConnectionTests
    {
        [Fact]
        public void Equal_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var locations = scope.Resolve<ILocationDictionary>();
            var random = new Random();

            var firstLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);
            var secondLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);

            while (firstLocation == secondLocation)
            {
                secondLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);
            }

            var firstMapLocation = (locations[(LocationID)firstLocation]).MapLocations[0];
            var secondMapLocation = (locations[(LocationID)secondLocation]).MapLocations[0];

            var factory = scope.Resolve<IConnection.Factory>();

            var firstConnection = factory(firstMapLocation, secondMapLocation);
            var secondConnection = factory(secondMapLocation, firstMapLocation);
            var thirdConnection = factory(firstMapLocation, secondMapLocation);

            Assert.True(firstConnection.Equals(secondConnection));
            Assert.True(secondConnection.Equals(firstConnection));
            Assert.True(thirdConnection.Equals(firstConnection));
        }

        [Fact]
        public void NotEqual_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var locations = scope.Resolve<ILocationDictionary>();
            var random = new Random();

            var firstLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);
            var secondLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);

            while (firstLocation == secondLocation)
            {
                secondLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);
            }

            var thirdLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);

            while (firstLocation == thirdLocation || secondLocation == thirdLocation)
            {
                thirdLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);
            }

            var firstMapLocation = (locations[(LocationID)firstLocation]).MapLocations[0];
            var secondMapLocation = (locations[(LocationID)secondLocation]).MapLocations[0];
            var thirdMapLocation = (locations[(LocationID)thirdLocation]).MapLocations[0];

            var factory = scope.Resolve<IConnection.Factory>();

            var firstConnection = factory(firstMapLocation, secondMapLocation);
            var secondConnection = factory(firstMapLocation, thirdMapLocation);
            var thirdConnection = factory(secondMapLocation, thirdMapLocation);

            Assert.False(firstConnection.Equals(secondConnection));
            Assert.False(secondConnection.Equals(firstConnection));
            Assert.False(firstConnection.Equals(thirdConnection));
            Assert.False(thirdConnection.Equals(firstConnection));
            Assert.False(secondConnection.Equals(thirdConnection));
            Assert.False(thirdConnection.Equals(secondConnection));
        }

        [Fact]
        public void Save_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var locations = scope.Resolve<ILocationDictionary>();
            var random = new Random();

            var firstLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);
            var secondLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);

            while (firstLocation == secondLocation)
            {
                secondLocation = random.Next(0, Enum.GetValues(typeof(LocationID)).Length - 1);
            }

            var firstMapLocation = (locations[(LocationID)firstLocation]).MapLocations[0];
            var secondMapLocation = (locations[(LocationID)secondLocation]).MapLocations[0];

            var factory = scope.Resolve<IConnection.Factory>();

            var connection = factory(firstMapLocation, secondMapLocation);
            var saveData = connection.Save();
            
            Assert.Equal(firstMapLocation, locations[saveData.Location1].MapLocations[saveData.Index1]);
            Assert.Equal(secondMapLocation, locations[saveData.Location2].MapLocations[saveData.Index2]);
        }
    }
}