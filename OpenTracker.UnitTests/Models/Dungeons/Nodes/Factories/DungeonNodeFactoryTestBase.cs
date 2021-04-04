using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public abstract class DungeonNodeFactoryTestBase
    {
        private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
        private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
        
        protected readonly IRequirementDictionary _requirements;
        protected readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IEntryNodeConnection.Factory _entryFactory;
        private readonly INodeConnection.Factory _connectionFactory;

        protected readonly List<INode> _entryFactoryCalls = new();
        protected readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls =
            new();

        protected readonly IDungeonNodeFactory _sut;

        protected DungeonNodeFactoryTestBase()
        {
            _requirements = new RequirementDictionary(() => _requirementFactory);
            _overworldNodes = new OverworldNodeDictionary(() => _overworldNodeFactory);

            _entryFactory = fromNode =>
            {
                _entryFactoryCalls.Add(fromNode);
                return Substitute.For<IEntryNodeConnection>();
            };

            _connectionFactory = (fromNode, toNode, requirement) =>
            {
                _connectionFactoryCalls.Add((fromNode, toNode, requirement));
                return Substitute.For<INodeConnection>();
            };
        }
    }
}