using Autofac;
using NSubstitute;
using OpenTracker.Models.Connections;
using OpenTracker.Models.UndoRedo.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Connections
{
    public class RemoveConnectionTests
    {
        private readonly IConnection _connection = Substitute.For<IConnection>();
        private readonly IConnectionCollection _connections = Substitute.For<IConnectionCollection>();

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
        public void ExecuteDo_ShouldCallRemoveOnConnectionCollection()
        {
            _sut.ExecuteDo();

            _connections.Received().Remove(_connection);
        }

        [Fact]
        public void ExecuteUndo_ShouldAddConnectionFromConnectionCollection()
        {
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            _connections.Received().Add(_connection);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();

            var factory = scope.Resolve<IRemoveConnection.Factory>();
            var sut = factory(_connection);
            
            Assert.NotNull(sut as RemoveConnection);
        }
    }
}