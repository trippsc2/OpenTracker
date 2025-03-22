using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories
{
    public class LightWorldConnectionFactoryTests
    {
        private static readonly IComplexRequirementDictionary ComplexRequirements =
            Substitute.For<IComplexRequirementDictionary>();
        private static readonly IItemRequirementDictionary ItemRequirements =
            Substitute.For<IItemRequirementDictionary>();
        private static readonly IPrizeRequirementDictionary PrizeRequirements =
            Substitute.For<IPrizeRequirementDictionary>();
        private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
            Substitute.For<ISequenceBreakRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();
        
        private static readonly IOverworldNodeDictionary OverworldNodes = Substitute.For<IOverworldNodeDictionary>();

        private static readonly INodeConnection.Factory ConnectionFactory = (fromNode, toNode, requirement) =>
            new NodeConnection(fromNode, toNode, requirement);

        private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

        private readonly LightWorldConnectionFactory _sut = new(
            ComplexRequirements, ItemRequirements, PrizeRequirements, SequenceBreakRequirements,
            WorldStateRequirements, OverworldNodes, ConnectionFactory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
            {
                var node = OverworldNodes[id];

                var connections = id switch
                {
                    OverworldNodeID.LightWorld => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.Start], node,
                            WorldStateRequirements[WorldState.StandardOpen]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DeathMountainEntry], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DeathMountainExit], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWKakarikoPortalNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWKakarikoPortalNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 2)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DesertLedge], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.GrassHouse], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BombHut], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.RaceGameLedge], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.CastleSecretExitArea], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.SouthOfGroveLedge], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.CheckerboardLedge], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWMirePortal], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWSouthPortalNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWWitchAreaNotBunny], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWEastPortalNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthInverted], node,
                            PrizeRequirements[(PrizeType.Aga1, 1)]),
                    },
                    OverworldNodeID.LightWorldInverted => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.LightWorldInvertedNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldInverted], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.LightWorldStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.LightWorldMirror => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            ComplexRequirements[ComplexRequirementType.LWMirror]),
                    },
                    OverworldNodeID.LightWorldNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.LightWorldNotBunnyOrDungeonRevive => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            SequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                    },
                    OverworldNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            SequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                    },
                    OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            ComplexRequirements[ComplexRequirementType.SuperBunnyMirror])
                    },
                    OverworldNodeID.LightWorldDash => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ItemRequirements[(ItemType.Boots, 1)])
                    },
                    OverworldNodeID.LightWorldHammer => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)])
                    },
                    OverworldNodeID.LightWorldLift1 => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)])
                    },
                    OverworldNodeID.LightWorldFlute => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ItemRequirements[(ItemType.Flute, 1)])
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
            var sut = scope.Resolve<ILightWorldConnectionFactory>();
            
            Assert.NotNull(sut as LightWorldConnectionFactory);
        }
    }
}