using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations.Map.Connections;

[ExcludeFromCodeCoverage]
public sealed class ConnectionCollectionTests
{
    private readonly ILocationDictionary _locations = Substitute.For<ILocationDictionary>();
        
    private readonly IMapConnection.Factory _connectionFactory = (_, _) => Substitute.For<IMapConnection>();
    private readonly IAddMapConnection.Factory _addConnectionFactory = _ => Substitute.For<IAddMapConnection>();

    private readonly IMapConnection _connection = Substitute.For<IMapConnection>();
    private readonly ConnectionSaveData _connectionSaveData = new();

    private readonly MapConnectionCollection _sut;

    public ConnectionCollectionTests()
    {
        var mapLocation = Substitute.For<IMapLocation>();
        var mapLocations = new List<IMapLocation>
        {
            mapLocation
        };
            
        var location = Substitute.For<ILocation>();
        location.MapLocations.Returns(mapLocations);
            
        _locations[Arg.Any<LocationID>()].Returns(location);
            
        _sut = new MapConnectionCollection(_locations, _connectionFactory, _addConnectionFactory)
        {
            _connection
        };
        _connection.Save().Returns(_connectionSaveData);
    }

    [Fact]
    public void AddConnection_ShouldReturnNewAddConnectionAction()
    {
        var location1 = Substitute.For<IMapLocation>();
        var location2 = Substitute.For<IMapLocation>();
        var addConnection = _sut.AddConnection(location1, location2);
            
        Assert.NotNull(addConnection);
    }

    [Fact]
    public void Save_ShouldCallSaveOnItems()
    {
        _ = _sut.Save();

        _connection.Received().Save();
    }

    [Fact]
    public void Save_ShouldReturnListOfSaveData()
    {
        var saveData = _sut.Save();

        Assert.Contains(_connectionSaveData, saveData);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        _sut.Load(null);

        Assert.Single(_sut);
    }

    [Fact]
    public void Load_ShouldLoadConnectionsFromSaveData_WhenSaveDataIsNotNull()
    {
        var saveData = new List<ConnectionSaveData>
        {
            _connectionSaveData
        };
        _sut.Load(saveData);
            
        Assert.DoesNotContain(_connection, _sut);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IMapConnectionCollection.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as MapConnectionCollection);
    }
}