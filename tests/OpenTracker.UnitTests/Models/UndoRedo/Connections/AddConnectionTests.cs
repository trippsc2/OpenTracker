using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.UndoRedo.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Connections;

[ExcludeFromCodeCoverage]
public sealed class AddConnectionTests
{
    private readonly IMapConnection _connection = Substitute.For<IMapConnection>();
    private readonly IMapConnectionCollection _connections = Substitute.For<IMapConnectionCollection>();

    private readonly AddMapConnection _sut;

    public AddConnectionTests()
    {
        _sut = new AddMapConnection(_connections, _connection);
    }
        
    [Fact]
    public void CanExecute_ShouldReturnTrue_WhenConnectionCollectionContainsReturnsFalse()
    {
        _connections.Contains(_connection).Returns(false);
            
        Assert.True(_sut.CanExecute());
    }

    [Fact]
    public void CanExecute_ShouldReturnFalse_WhenConnectionCollectionContainsConnection()
    {
        _connections.Contains(_connection).Returns(true);
            
        Assert.False(_sut.CanExecute());
    }

    [Fact]
    public void ExecuteDo_ShouldCallAddOnConnectionCollection()
    {
        _sut.ExecuteDo();

        _connections.Received().Add(_connection);
    }

    [Fact]
    public void ExecuteUndo_ShouldRemoveConnectionFromConnectionCollection()
    {
        _sut.ExecuteDo();
        _sut.ExecuteUndo();

        _connections.Received().Remove(_connection);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();

        var factory = scope.Resolve<IAddMapConnection.Factory>();
        var sut = factory(_connection);
            
        Assert.NotNull(sut as AddMapConnection);
    }
}