using System;
using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.NodeConnections;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes
{
    public class DungeonNodeTests
    {
        private readonly IDungeonNodeFactory _factory = Substitute.For<IDungeonNodeFactory>();
        private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

        private readonly List<INodeConnection> _connections = new()
        {
            Substitute.For<INodeConnection>(),
            Substitute.For<INodeConnection>()
        };

        private readonly DungeonNode _sut;

        public DungeonNodeTests()
        {
            _sut = new DungeonNode(_factory, _dungeonData);
            _factory.When(x => x.PopulateNodeConnections(
                Arg.Any<IMutableDungeon>(), Arg.Any<DungeonNodeID>(), _sut,
                Arg.Any<IList<INodeConnection>>())).Do(x =>
                {
                    var connections = (IList<INodeConnection>)x[3];
                    foreach (var connection in _connections)
                    {
                        connections.Add(connection);
                    }
                });
            _dungeonData.Nodes.ItemCreated += Raise.Event<EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>>>(
                _dungeonData.Nodes, new KeyValuePair<DungeonNodeID, IDungeonNode>(
                    DungeonNodeID.AT, _sut));
        }
        
        
    }
}