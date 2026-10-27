using System.Collections.ObjectModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.UndoRedo.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Connections;

public class AddConnectionTests
{
    private readonly IMapConnection _connection = Substitute.For<IMapConnection>();
    private readonly IMapConnectionCollection _connections = Substitute.For<IMapConnectionCollection>();
    private readonly ObservableCollection<IMapConnection> _collection = new();

    private readonly AddMapConnection _sut;

    public AddConnectionTests()
    {
        _connections.Connections.Returns(_collection);
        _sut = new AddMapConnection(_connections, _connection);
    }
        
    [Fact]
    public void CanExecute_ShouldReturnTrue_WhenConnectionCollectionContainsReturnsFalse()
    {
        
            
        Assert.True(_sut.CanExecute());
    }

    [Fact]
    public void CanExecute_ShouldReturnFalse_WhenConnectionCollectionContainsConnection()
    {
        _collection.Add(_connection);
            
        Assert.False(_sut.CanExecute());
    }

    [Fact]
    public void ExecuteDo_ShouldCallAddOnConnectionCollection()
    {
        _sut.ExecuteDo();

        Assert.Contains(_connection, _collection);
    }

    [Fact]
    public void ExecuteUndo_ShouldRemoveConnectionFromConnectionCollection()
    {
        _sut.ExecuteDo();
        _sut.ExecuteUndo();

        Assert.DoesNotContain(_connection, _collection);
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