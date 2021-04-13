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
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prizes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories
{
    public class NEDarkWorldConnectionFactoryTests
    {
        private static readonly IComplexRequirementDictionary ComplexRequirements =
            Substitute.For<IComplexRequirementDictionary>();
        private static readonly IItemRequirementDictionary ItemRequirements =
            Substitute.For<IItemRequirementDictionary>();
        private static readonly IPrizeRequirementDictionary PrizeRequirements =
            Substitute.For<IPrizeRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();
        
        private static readonly IOverworldNodeDictionary OverworldNodes = Substitute.For<IOverworldNodeDictionary>();

        private static readonly INodeConnection.Factory ConnectionFactory =
            (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

        private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

        private readonly NEDarkWorldConnectionFactory _sut = new(
            ComplexRequirements, ItemRequirements, PrizeRequirements, WorldStateRequirements, OverworldNodes,
            ConnectionFactory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
            {
                var node = OverworldNodes[id];

                var connections = id switch
                {
                    OverworldNodeID.DWWitchArea => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWWitchArea], node,
                            ComplexRequirements[ComplexRequirementType.LWMirror]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.FluteInverted], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node)
                    },
                    OverworldNodeID.DWWitchAreaNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWWitchArea], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                    },
                    OverworldNodeID.CatfishArea => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.ZoraArea], node,
                            ComplexRequirements[ComplexRequirementType.LWMirror]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)])
                    },
                    OverworldNodeID.DarkWorldEast => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldStandardOpen], node,
                            PrizeRequirements[(PrizeType.Aga1, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.FluteInverted], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldMirror], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldSouthHammer], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWEastPortalNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node)
                    },
                    OverworldNodeID.DarkWorldEastStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEast], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.DarkWorldEastNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEast], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                    },
                    OverworldNodeID.DarkWorldEastHammer => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)])
                    },
                    OverworldNodeID.FatFairyEntrance => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            PrizeRequirements[(PrizeType.RedCrystal, 2)])
                    },
                    OverworldNodeID.DWEastPortal => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWEastPortalStandardOpen], node,
                            ItemRequirements[(ItemType.Gloves, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldEastHammer], node)
                    },
                    OverworldNodeID.DWEastPortalInverted => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWEastPortal], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.DWEastPortalNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWEastPortal], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyDW])
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
            var sut = scope.Resolve<INEDarkWorldConnectionFactory>();
            
            Assert.NotNull(sut as NEDarkWorldConnectionFactory);
        }
    }
}