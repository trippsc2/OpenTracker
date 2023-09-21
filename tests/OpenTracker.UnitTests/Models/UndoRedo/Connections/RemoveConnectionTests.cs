using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.UndoRedo.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Connections;

[ExcludeFromCodeCoverage]
public sealed class RemoveConnectionTests
{
    private readonly IMapConnection _connection = Substitute.For<IMapConnection>();
    private readonly IMapConnectionCollection _connections = Substitute.For<IMapConnectionCollection>();

    private readonly RemoveMapConnection _sut;

    public RemoveConnectionTests()
    {
        _sut = new RemoveMapConnection(_connections, _connection);
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

        var factory = scope.Resolve<IRemoveMapConnection.Factory>();
        var sut = factory(_connection);
            
        Assert.NotNull(sut as RemoveMapConnection);
    }
}