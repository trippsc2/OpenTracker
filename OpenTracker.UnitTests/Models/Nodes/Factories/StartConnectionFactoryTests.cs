using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories
{
    public class StartConnectionFactoryTests
    {
        private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
            new AlternativeRequirementDictionary(
                requirements => new AlternativeRequirement(requirements));
        private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
            Substitute.For<IEntranceShuffleRequirementDictionary>();
        private static readonly IItemRequirementDictionary ItemRequirements =
            Substitute.For<IItemRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();

        private static readonly IOverworldNodeDictionary OverworldNodes = Substitute.For<IOverworldNodeDictionary>();

        private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

        private readonly StartConnectionFactory _sut;

        public StartConnectionFactoryTests()
        {
            _sut = new StartConnectionFactory(
                AlternativeRequirements, EntranceShuffleRequirements, ItemRequirements, WorldStateRequirements,
                OverworldNodes,
                (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement));
        }

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
            {
                var connections = id switch
                {
                    OverworldNodeID.EntranceDungeonAllInsanity => new List<INodeConnection>
                    {
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.Start], OverworldNodes[id],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                                EntranceShuffleRequirements[EntranceShuffle.All],
                                EntranceShuffleRequirements[EntranceShuffle.Insanity]
                            }])
                    },
                    OverworldNodeID.EntranceNone => new List<INodeConnection>
                    {
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.Start], OverworldNodes[id],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }])
                    },
                    OverworldNodeID.EntranceNoneInverted => new List<INodeConnection>
                    {
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.EntranceNone], OverworldNodes[id],
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.Flute => new List<INodeConnection>
                    {
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.Start], OverworldNodes[id],
                            ItemRequirements[(ItemType.Flute, 1)])
                    },
                    OverworldNodeID.FluteActivated => new List<INodeConnection>
                    {
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.Flute], OverworldNodes[id],
                            ItemRequirements[(ItemType.FluteActivated, 1)]),
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.LightWorldFlute], OverworldNodes[id])
                    },
                    OverworldNodeID.FluteInverted => new List<INodeConnection>
                    {
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.FluteActivated], OverworldNodes[id],
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.FluteStandardOpen => new List<INodeConnection>
                    {
                        new NodeConnection(
                            OverworldNodes[OverworldNodeID.FluteActivated], OverworldNodes[id],
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    _ => null
                };

                if (connections is null)
                {
                    continue;
                }
                
                ExpectedValues.Add(id, connections.ToExpectedObject());
            }
        }

        [Fact]
        public void GetNodeConnections_ShouldThrowException_WhenIDIsUnexpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _sut.GetNodeConnections((OverworldNodeID)int.MaxValue, Substitute.For<INode>()));
        }

        [Theory]
        [MemberData(nameof(GetNodeConnections_ShouldReturnExpectedValueData))]
        public void GetNodeConnections_ShouldReturnExpectedValue(ExpectedObject expected, OverworldNodeID id)
        {
            expected.ShouldEqual(_sut.GetNodeConnections(id, OverworldNodes[id]));
        }

        public static IEnumerable<object[]> GetNodeConnections_ShouldReturnExpectedValueData()
        {
            PopulateExpectedValues();

            return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IStartConnectionFactory>();
            
            Assert.NotNull(sut as StartConnectionFactory);
        }
    }
}