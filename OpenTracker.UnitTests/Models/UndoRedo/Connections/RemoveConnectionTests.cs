using Autofac;
using NSubstitute;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Connections
{
    public class RemoveConnectionTests
    {
        private readonly IConnection _connection = Substitute.For<IConnection>();
        private readonly IConnectionCollection _connections = new ConnectionCollection(
            Substitute.For<ILocationDictionary>(),
            (location1, location2) => new Connection(location1, location2));

        private readonly RemoveConnection _sut;

        public RemoveConnectionTests()
        {
            _sut = new RemoveConnection(_connections, _connection);
        }
        
        [Fact]
        public void CanExecute_ShouldReturnTrue()
        {
            Assert.True(_sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldRemoveConnectionFromConnectionCollection()
        {
            _connections.Add(_connection);
            _sut.ExecuteDo();

            Assert.DoesNotContain(_connection, _connections);
        }

        [Fact]
        public void ExecuteUndo_ShouldAddConnectionFromConnectionCollection()
        {
            _connections.Add(_connection);
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            Assert.Contains(_connection, _connections);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();

            var factory = scope.Resolve<RemoveConnection.Factory>();
            var sut = factory(_connection);
            
            Assert.NotNull(sut);
        }
    }
}