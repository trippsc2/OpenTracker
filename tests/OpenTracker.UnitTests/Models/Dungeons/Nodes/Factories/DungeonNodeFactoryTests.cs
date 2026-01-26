using System;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories;

public class DungeonNodeFactoryTests
{
    private readonly IHCDungeonNodeFactory _hcFactory = Substitute.For<IHCDungeonNodeFactory>();
    private readonly IATDungeonNodeFactory _atFactory = Substitute.For<IATDungeonNodeFactory>();
    private readonly IEPDungeonNodeFactory _epFactory = Substitute.For<IEPDungeonNodeFactory>();
    private readonly IDPDungeonNodeFactory _dpFactory = Substitute.For<IDPDungeonNodeFactory>();
    private readonly IToHDungeonNodeFactory _tohFactory = Substitute.For<IToHDungeonNodeFactory>();
    private readonly IPoDDungeonNodeFactory _podFactory = Substitute.For<IPoDDungeonNodeFactory>();
    private readonly ISPDungeonNodeFactory _spFactory = Substitute.For<ISPDungeonNodeFactory>();
    private readonly ISWDungeonNodeFactory _swFactory = Substitute.For<ISWDungeonNodeFactory>();
    private readonly ITTDungeonNodeFactory _ttFactory = Substitute.For<ITTDungeonNodeFactory>();
    private readonly IIPDungeonNodeFactory _ipFactory = Substitute.For<IIPDungeonNodeFactory>();
    private readonly IMMDungeonNodeFactory _mmFactory = Substitute.For<IMMDungeonNodeFactory>();
    private readonly ITRDungeonNodeFactory _trFactory = Substitute.For<ITRDungeonNodeFactory>();
    private readonly IGTDungeonNodeFactory _gtFactory = Substitute.For<IGTDungeonNodeFactory>();

    private readonly DungeonNodeFactory _sut;

    public DungeonNodeFactoryTests()
    {
        _sut = new DungeonNodeFactory(
            _hcFactory, _atFactory, _epFactory, _dpFactory, _tohFactory, _podFactory, _spFactory, _swFactory,
            _ttFactory, _ipFactory, _mmFactory, _trFactory, _gtFactory);
    }

    [Fact]
    public void PopulateNodeConnections_ShouldThrowException_WhenDungeonIDIsUnexpected()
    {
        var dungeonData = Substitute.For<IMutableDungeon>();
        dungeonData.ID.Returns((DungeonID) int.MaxValue);
        var id = DungeonNodeID.AT;
        var node = Substitute.For<INode>();
        var connections = new List<INodeConnection>();

        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PopulateNodeConnections(
            dungeonData, id, node, connections));
    }

    [Theory]
    [InlineData(DungeonID.HyruleCastle)]
    [InlineData(DungeonID.AgahnimTower)]
    [InlineData(DungeonID.EasternPalace)]
    [InlineData(DungeonID.DesertPalace)]
    [InlineData(DungeonID.TowerOfHera)]
    [InlineData(DungeonID.PalaceOfDarkness)]
    [InlineData(DungeonID.SwampPalace)]
    [InlineData(DungeonID.SkullWoods)]
    [InlineData(DungeonID.ThievesTown)]
    [InlineData(DungeonID.IcePalace)]
    [InlineData(DungeonID.MiseryMire)]
    [InlineData(DungeonID.TurtleRock)]
    [InlineData(DungeonID.GanonsTower)]
    public void PopulateNodeConnections_ShouldCallPopulateNodeConnectionsOnCorrectFactory(DungeonID id)
    {
        IDungeonNodeFactory expectedFactory = id switch
        {
            DungeonID.HyruleCastle => _hcFactory,
            DungeonID.AgahnimTower => _atFactory,
            DungeonID.EasternPalace => _epFactory,
            DungeonID.DesertPalace => _dpFactory,
            DungeonID.TowerOfHera => _tohFactory,
            DungeonID.PalaceOfDarkness => _podFactory,
            DungeonID.SwampPalace => _spFactory,
            DungeonID.SkullWoods => _swFactory,
            DungeonID.ThievesTown => _ttFactory,
            DungeonID.IcePalace => _ipFactory,
            DungeonID.MiseryMire => _mmFactory,
            DungeonID.TurtleRock => _trFactory,
            DungeonID.GanonsTower => _gtFactory,
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };

        var dungeonData = Substitute.For<IMutableDungeon>();
        dungeonData.ID.Returns(id);
        const DungeonNodeID nodeID = DungeonNodeID.AT;
        var node = Substitute.For<INode>();
        var connections = new List<INodeConnection>();
        _sut.PopulateNodeConnections(dungeonData, nodeID, node, connections);
            
        expectedFactory.Received().PopulateNodeConnections(dungeonData, nodeID, node, connections);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IDungeonNodeFactory>();
            
        Assert.NotNull(sut as DungeonNodeFactory);
    }
}